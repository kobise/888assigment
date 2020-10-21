using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homeassignment.server
{
    public interface ICache
    {
        /// <summary>
        /// Try to get specific object from cache
        /// </summary>
        /// <typeparam name="T">object type that stored in cache</typeparam>
        /// <param name="key">key of the data in cache</param>
        /// <returns> return required T, return null if their is no data in cache</returns>
        Task<T> TryGetAsync<T>(string key);


        /// <summary>
        /// Save collection of T in cache 
        /// </summary>
        /// <typeparam name="T">object type that stored in cache</typeparam>
        /// <param name="key">key of the data in cache</param>
        /// <param name="data">Collection of T</param>
        /// <returns>Throw Exception if failed</returns>
        Task SaveAsync<T>(string key, T data);
    }
}
