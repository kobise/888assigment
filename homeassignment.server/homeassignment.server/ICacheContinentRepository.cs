using homeassignment.server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace homeassignment.server
{
    public interface ICacheContinentRepository : ICache, IContinentRepository
    {

        /// <summary>
        /// Save collection of Continent in cache 
        /// </summary>
        /// <param name="continents">Collection of continent</param>
        /// <returns>Throw Exception if failed</returns>
        Task SaveAllContinentsAsync(ICollection<Continent> continents);

        /// <summary>
        /// Save continent T in cache 
        /// </summary>
        /// <param name="continent">continent object</param>
        /// <returns>Throw Exception if failed</returns>
        Task SaveContinentByCodeAsync(string code, Continent continent);


    }
}