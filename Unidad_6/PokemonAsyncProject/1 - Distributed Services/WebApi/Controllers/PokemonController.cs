using Contracts;
using Implementations.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiPokemon.Models;

namespace WebApiPokemon.Controllers
{
    public class PokemonController : ApiController
    {
        private readonly IPokemonService _pokemonsSevice;

        public PokemonController(IPokemonService pokeService)
        {
            _pokemonsSevice = pokeService;
        }

        [HttpPost]
        [Route("IntroducePokemonsByUrl")]
        public async Task<IHttpActionResult> PokeListIntroduced(PokemonUrlModel pokeModel)
        {
            try
            {

                string methodResponse = await _pokemonsSevice.AddPokemonsFromUrl(pokeModel.PokeApiUrls);

                return Ok(methodResponse);



            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message); //TODO sgarciam 1706 gestion de excepciones mejorada.
            }

        }


        [HttpPost]
        [Route("IntroducePokemonByName")]
        public async Task<IHttpActionResult> IntroducePokemon(PokemonNameModel pokeModel)
        {
            try
            {

                string methodResponse = await _pokemonsSevice.AddPokemonFromName(pokeModel.PokemonName.ToLower());

                return Ok(methodResponse);


            }

            //catch (HttpRequestException ex)
            //{
            //    return BadRequest($"The pokemon {pokeModel} weren't found. See the error code: {ex.Message}");
            //}
            catch (NotAllowGenerationException ex)
            {
                return BadRequest(ex.Message);

            }
            catch (NotAllowTypeException ex)
            {
                return BadRequest(ex.Message);

            }
            catch (AllReadyIntroducedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest($"The pokemon name mustn't to be empty. See the error code: {ex.Message}");
            }
            catch (Exception ex)
            {

                return BadRequest($"The pokemon {pokeModel.PokemonName} doesn't exsist. See the error code: {ex.Message}"); //TODO sgarciam 1706 gestion de excepciones mejorada.
            }

        }
    }
}
