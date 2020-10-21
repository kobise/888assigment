using homeassignment.server.GeaphQL;
using homeassignment.server.Models;
using homeassignment.server.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using System.Data;

namespace homeassignment.server
{
    public class ContinentRepository : IContinentRepository
    {
        public const string GetDataFromCacheError = "Failed get data from cache";
        public const string GetDataFromGraphQLError = "Failed get data from GraphQL";
        public const string SaveDataInCacheError = "Failed save data from cache";
        
        ICacheContinentRepository _cacheRepository;
        IGraphQLRepository _graphQLRepository;
        Serilog.ILogger _logger;
        public ContinentRepository(Serilog.ILogger logger, ICacheContinentRepository cacheRepository, IGraphQLRepository graphQLRepository)
        {
            _cacheRepository = cacheRepository;
            _graphQLRepository = graphQLRepository;
            _logger = logger;
        }

        public async Task<ICollection<Continent>> GetAllContinentsAsync()
        {

            ICollection<Continent> result = null;
            try
            {
                result = await _cacheRepository.GetAllContinentsAsync();
            }
            catch (Exception e)
            {
                _logger.Error(e, GetDataFromCacheError);
            }
            if (result == null)
            {
                try
                {
                    result = await _graphQLRepository.GetAllContinentsAsync();

                }
                catch (Exception e)
                {
                    _logger.Error(e, GetDataFromGraphQLError);
                    throw new DataException(GetDataFromGraphQLError);
                }
            }
            if (result != null)
            {
                try
                {
                    await _cacheRepository.SaveAllContinentsAsync(result);
                }
                catch (Exception e)
                {
                    _logger.Error(e, SaveDataInCacheError);
                }

            }
            return result;
        }

        public async Task<Continent> GetContinentByCodeAsync(string code)
        {
            Continent result = null;
            try
            {
                result = await _cacheRepository.GetContinentByCodeAsync(code);
            }
            catch (Exception e)
            {
                _logger.Error(e, GetDataFromCacheError);
            }
            if (result == null)
            {
                try
                {
                    result = await _graphQLRepository.GetContinentByCodeAsync(code);

                }
                catch (Exception e)
                {
                    _logger.Error(e, GetDataFromGraphQLError);
                    throw new DataException(GetDataFromGraphQLError);
                }
            }
            if (result != null)
            {
                try
                {
                    await _cacheRepository.SaveContinentByCodeAsync(code,result);
                }
                catch (Exception e)
                {
                    _logger.Error(e, SaveDataInCacheError);
                }
            }
            return result;
        }
    }
}
