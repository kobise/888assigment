using homeassignment.server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace homeassignment.server
{
    public interface IContinentService
    {
        Task<Continent> GetContinentByCodeAsync(string code);
        Task<ICollection<Continent>> GetContinentsAsync();
    }
}