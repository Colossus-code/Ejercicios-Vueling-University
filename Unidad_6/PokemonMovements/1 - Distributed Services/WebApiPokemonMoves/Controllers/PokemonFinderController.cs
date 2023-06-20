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



        public PokemonFinderController()
        {

        }

        [HttpPost]
        [Route("PokemonMovesForTypeAndLanguage")]
        public async Task<IHttpActionResult> PokeListIntroduced(PokemonFinderModel pokemonFinder)
        {

            return Ok("Okay");
        }
    }
}