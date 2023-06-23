using Contracts;
using Contracts.CustomExceptions;
using Contracts.RequestService;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
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
            _pokeLogger.Information($"Finding {pokemonFinder.QuantityOfResults} first moves of type {pokemonFinder.TypeOfPokemon}");

            try
            {
                RequestPokeApiModel requestPokeapiModel = new RequestPokeApiModel
                {
                    Language = pokemonFinder.LanguageToFind.ToLower(),
                    Quantity = Convert.ToInt32(pokemonFinder.QuantityOfResults),
                    Type = pokemonFinder.TypeOfPokemon.ToLower()

                };


                return Ok(await _pokeFinderService.IntroduceMovesByTypeAndLng(requestPokeapiModel));


            }
            catch (FileNotFoundException ex)
            {
                _pokeLogger.Error($"Error at: {DateTime.UtcNow}, exception message: {ex.Message}. Exception stacktrace: {ex.StackTrace}");
                return BadRequest("Can't to persist into a file, comprove that path file it's correctly and the file wasn't deleted.");
            }
            catch (FormatException ex)
            {
                _pokeLogger.Error($"Error at: {DateTime.UtcNow}, exception message: {ex.Message}. Exception stacktrace: {ex.StackTrace}");
                return BadRequest("You must to introduce a number of quantity");

            }
            catch (NotAllowLenguageException ex)
            {

                _pokeLogger.Error($"Error at: {DateTime.UtcNow}, exception message: {ex.Message}. Exception stacktrace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }            
            catch (NotRealTypeException ex)
            {

                _pokeLogger.Error($"Error at: {DateTime.UtcNow}, exception message: {ex.Message}. Exception stacktrace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {

                _pokeLogger.Error($"Error at: {DateTime.UtcNow}, exception message: {ex.Message}. Exception stacktrace: {ex.StackTrace}");
                return BadRequest("An error has been occurred, contact with the administrator.");
            }
        }
    }

}
