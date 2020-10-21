using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using homeassignment.server.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace homeassignment.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContinentsController : ControllerBase
    {
        IContinentService _continentService;
        public ContinentsController(IContinentService continentService)
        {
            _continentService = continentService;
        }

        // GET api/continents
        [HttpGet]
        public async Task<ActionResult<ICollection<Continent>>> GetAsync()
        {
            return Ok(await _continentService.GetContinentsAsync());
        }

        // GET api/continents/5
        [HttpGet("{code}")]
        public async Task<ActionResult<Continent>> Get(string code)
        {
            return Ok(await _continentService.GetContinentByCodeAsync(code));
        }

    }
}
