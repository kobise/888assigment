using homeassignment.server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace homeassignment.server
{
    public interface IContinentRepository
    {
        /// <summary>
        /// get all continents from repository
        /// </summary>
        /// <returns> Collection of Continents</returns>
        Task<ICollection<Continent>> GetAllContinentsAsync();

        /// <summary>
        /// get continent by code from repository
        /// </summary>
        /// <param name="code"> continent code</param>
        /// <returns> continent object</returns>
        Task<Continent> GetContinentByCodeAsync(string code);
    }
}