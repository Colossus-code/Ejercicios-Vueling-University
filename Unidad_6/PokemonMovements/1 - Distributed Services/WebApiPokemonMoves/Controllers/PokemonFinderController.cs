using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApiPokemonMoves.Models;

namespace WebApiPokemonMoves.Controllers
{
    public class PokemonFinderController : ApiController
    {

        private readonly ILogger _pokeLogger; 

        public PokemonFinderController(ILogger logger)
        {
            _pokeLogger = logger;
        }

        [HttpPost]
        [Route("PokemonMovesForTypeAndLanguage")]
        public async Task<IHttpActionResult> PokeListIntroduced(PokemonFinderModel pokemonFinder)
        {
            _pokeLogger.Information($"Introducing {pokemonFinder.Quantity} first moves of type {pokemonFinder.Type}");

            return Ok("Okay");
        }
    }
}