using homeassignment.server.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homeassignment.server
{
    /// <summary>
    /// Adapter between api controller to the repository.
    /// All data logics should be written here.
    /// </summary>
    public class ContinentService : IContinentService
    {
        IContinentRepository _repo;
        ILogger _logger;

        public ContinentService(ILogger logger, IContinentRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        /// <summary>
        /// Get all continents from graphQL or redis
        /// </summary>
        /// <returns>Collection of Continent</returns>
        public async Task<ICollection<Continent>> GetContinentsAsync()
        {
            _logger.Verbose("Get all continents");
            return await _repo.GetAllContinentsAsync();
        }

        /// <summary>
        /// Get Continent by continent's code
        /// </summary>
        /// <param name="code">continent's code</param>
        /// <returns>Requested Continent, if not exist return null</returns>
        public async Task<Continent> GetContinentByCodeAsync(string code)
        {
            _logger.Verbose("Get continent by code:{continentCode}", code.ToUpper());
            return await _repo.GetContinentByCodeAsync(code);
        }

    }
}
