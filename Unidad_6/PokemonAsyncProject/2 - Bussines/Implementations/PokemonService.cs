using Contracts;
using Domain;
using Implementations.CustomExceptions;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonRepositoryUrls _pokemonRepositoryUrl;
        private readonly IPokemonRepositoryByGeneration _pokemonRepositoryGeneration;
        private readonly IPokemonFinderAndTransform _pokemonFinderAndTransform;

        public PokemonService(IPokemonRepositoryUrls pokeRepoUrl, IPokemonRepositoryByGeneration pokeRepoGeneration, IPokemonFinderAndTransform pokemonFinderAndTransform)
        {
            _pokemonRepositoryUrl = pokeRepoUrl;
            _pokemonRepositoryGeneration = pokeRepoGeneration;
            _pokemonFinderAndTransform = pokemonFinderAndTransform;
        }

        public async Task<string> AddPokemonsFromUrl(List<string> urls)
        {
            //todo sgarciam 2006 agregar logger y test
            List<PokemonDomainEntity> pokemonsDomainEntity = await _pokemonRepositoryUrl.GetAllIntroduced(urls);
            List<PokemonDomainEntity> pokemonsToDelete = new List<PokemonDomainEntity>();
            foreach (PokemonDomainEntity pokemon in pokemonsDomainEntity)
            {
                if (!(pokemon.Types.Contains("fire") && (pokemon.Generation != null)))
                {
                    pokemonsToDelete.Add(pokemon);
                }

            }

            foreach (PokemonDomainEntity pokemon in pokemonsToDelete)
            {
                pokemonsDomainEntity.Remove(pokemon);
            }

            if (pokemonsDomainEntity.Count == 0)
            {

                throw new NotFoundFireOrGeneration("Not found any pokemon witch is type fire and 1st, 2nd or 3th generation");

            }

            _pokemonRepositoryUrl.PersistPokemonAsJson(pokemonsDomainEntity);



            return $"The {pokemonsDomainEntity.Count()} list of pokemons had been added";
        }

        public async Task<string> AddPokemonFromName(string pokemonName)
        {

            PokemonDomainEntity pokemonSelected = await _pokemonRepositoryGeneration.GetPokemonFromApi(pokemonName);

            if (!pokemonSelected.Types.Contains("fire"))
            {

                throw new NotAllowTypeException($"The Pokemon must to be type Fire, {pokemonName} it's {pokemonSelected.Types.FirstOrDefault()}.");

            }
            if (pokemonSelected.Generation == null)
            {

                throw new NotAllowGenerationException("The Pokemon must to be of the 1st, 2nd or 3th Generation.");
            }

            int generationToInsert = (ComprobeIsNotInTheList(pokemonSelected));

            if (generationToInsert == 0)
            {

                throw new AllReadyIntroducedException($"The Pokemon, {pokemonName} it's already introduced at the list.");
            }

            _pokemonRepositoryGeneration.PersistPokemon(pokemonSelected, generationToInsert);

            return $"The Pokemon {pokemonName} has been added to the file";

        }

        private int ComprobeIsNotInTheList(PokemonDomainEntity pokemonDomain)
        {
            int valueOfGeneration = 0;

            string actualList = null;

            switch (pokemonDomain.Generation)
            {

                case "first Generation":

                    actualList = _pokemonFinderAndTransform.GetAllList(1);

                    valueOfGeneration = 1;

                    if (actualList.Contains(pokemonDomain.Name))
                    {
                        valueOfGeneration = 0;
                    }

                    return valueOfGeneration;

                case "second Generation":

                    actualList = _pokemonFinderAndTransform.GetAllList(2);

                    valueOfGeneration = 2;

                    if (actualList.Contains(pokemonDomain.Name))
                    {
                        valueOfGeneration = 0;
                    }

                    return valueOfGeneration;

                case "third Generation":

                    actualList = _pokemonFinderAndTransform.GetAllList(3);

                    valueOfGeneration = 3;

                    if (actualList.Contains(pokemonDomain.Name))
                    {
                        valueOfGeneration = 0;
                    }

                    return valueOfGeneration;

            }

            return valueOfGeneration;

        }
    }
}
