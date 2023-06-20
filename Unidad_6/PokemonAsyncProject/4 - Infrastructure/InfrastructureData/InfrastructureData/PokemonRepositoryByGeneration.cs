using Domain;
using Dto;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureData
{
    public class PokemonRepositoryByGeneration : IPokemonRepositoryByGeneration
    {

        private readonly string _pathFileFirstGen;
        private readonly string _pathFileSecondGen;
        private readonly string _pathFileThirdGen;

        private readonly IPokemonFinderAndTransform _pokemonFinder;

        private const string StartOfUrlPokemon = "https://pokeapi.co/api/v2/pokemon/";

        public PokemonRepositoryByGeneration(IPokemonFinderAndTransform pokeFinder)
        {
            _pathFileFirstGen = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocalFiles", "PokemonFirstGen.txt");
            _pathFileSecondGen = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocalFiles", "PokemonSecondGen.txt");
            _pathFileThirdGen = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocalFiles", "PokemonThirdGen.txt");

            _pokemonFinder = pokeFinder;
        }

        public async Task<PokemonDomainEntity> GetPokemonFromApi(string name)
        {
            PokemonDto pokemonDto = await _pokemonFinder.GetPokemonsFromApiUrlAsString(StartOfUrlPokemon + name);

            return _pokemonFinder.TrasnfromDtoToEntity(pokemonDto);
        }

        public bool PersistPokemon(PokemonDomainEntity pokemon, int generation)
        {
            string actualOnes = _pokemonFinder.GetAllList(generation);

            switch (generation)
            {

                case 1:

                    File.WriteAllText(_pathFileFirstGen, actualOnes += $"\n{pokemon}");
                    break;

                case 2:

                    File.WriteAllText(_pathFileSecondGen, actualOnes += $"\n{pokemon}");
                    break;

                case 3:

                    File.WriteAllText(_pathFileThirdGen, actualOnes += $"\n{pokemon}");
                    break;
            }

            return true;
        }


    }
}
