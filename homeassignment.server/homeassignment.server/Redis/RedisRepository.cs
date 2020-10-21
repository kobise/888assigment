using homeassignment.server.Models;
using Newtonsoft.Json;
using Serilog;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace homeassignment.server.Redis
{
    public class RedisRepository : ICacheContinentRepository
    {
        IDatabase _conection;
        ILogger _logger;
        public RedisRepository(ILogger logger, IDatabase databaseConection)
        {
            _conection = databaseConection;
            _logger = logger;

        }

        /// <summary>
        /// Try to get specific object from cache
        /// </summary>
        /// <typeparam name="T">object type that stored in cache</typeparam>
        /// <param name="key">key of the data in cache</param>
        /// <returns> return required T, return null if their is no data in cache</returns>
        public async Task<T> TryGetAsync<T>(string key)
        {
            var result = await _conection.StringGetAsync(key);
            if (result.IsNull)
            {
                return default(T);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(result);
            }
        }


        /// <summary>
        /// Save collection of T in cache 
        /// </summary>
        /// <typeparam name="T">object type that stored in cache</typeparam>
        /// <param name="key">key of the data in cache</param>
        /// <param name="data">Collection of T</param>
        /// <returns>Throw Exception if failed</returns>
        public async Task SaveAsync<T>(string key, T data)
        {
            var wasSet = await _conection.StringSetAsync(key, JsonConvert.SerializeObject(data));
            if (!wasSet)
            {
                throw new Exception("Failed to add data to cache");
            }
        }

        /// <summary>
        /// Save collection of Continent in cache 
        /// </summary>
        /// <param name="continents">Collection of continent</param>
        /// <returns>Throw Exception if failed</returns>
        public Task SaveAllContinentsAsync(ICollection<Continent> continents)
        {
            return SaveAsync("continents", continents);
        }


        /// <summary>
        /// Save continent T in cache 
        /// </summary>
        /// <param name="continent">continent object</param>
        /// <returns>Throw Exception if failed</returns>
        public Task SaveContinentByCodeAsync(string code, Continent continent)
        {
            return SaveAsync($"continents{code}", continent);
        }

        public Task<ICollection<Continent>> GetAllContinentsAsync()
        {
            return TryGetAsync<ICollection<Continent>>("continents");
        }

        public Task<Continent> GetContinentByCodeAsync(string code)
        {
            return TryGetAsync<Continent>($"continents{code}");
        }
    }
}
