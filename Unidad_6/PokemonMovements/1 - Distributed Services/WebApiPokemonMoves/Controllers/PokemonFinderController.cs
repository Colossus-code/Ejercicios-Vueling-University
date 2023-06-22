using Contracts;
using Contracts.CustomExceptions;
using Contracts.RequestService;
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
        private readonly IPokemonFinderService _pokeFinderService;

        public PokemonFinderController(ILogger logger, IPokemonFinderService pokeFinde)
        {
            _pokeLogger = logger;
            _pokeFinderService = pokeFinde;
        }

        [HttpPost]
        [Route("PokemonMovesForTypeAndLanguage")]
        public async Task<IHttpActionResult> PokeListIntroduced(PokemonFinderModel pokemonFinder)
        {
            _pokeLogger.Information($"Introducing {pokemonFinder.QuantityOfResults} first moves of type {pokemonFinder.TypeOfPokemon}");

            try
            {
                RequestPokeApiModel requestPokeapiModel = new RequestPokeApiModel
                {
                    Language = pokemonFinder.LanguageToFind,
                    Quantity = Convert.ToInt32(pokemonFinder.QuantityOfResults),
                    Type = pokemonFinder.TypeOfPokemon

                };

                await _pokeFinderService.IntroduceMovesByTypeAndLng(requestPokeapiModel);

                return Ok("Okay");


            }catch(NotAllowLenguageException ex) { 
            
                _pokeLogger.Error($"Error at: {DateTime.UtcNow}, exception message: {ex.Message}. Exception stacktrace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            
            }catch(Exception ex) { 
            
                _pokeLogger.Error($"Error at: {DateTime.UtcNow}, exception message: {ex.Message}. Exception stacktrace: {ex.StackTrace}");
                return BadRequest("An error has been occurred, contact with the administrator.");
            }
        }
    }
}