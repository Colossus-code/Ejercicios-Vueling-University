using Domain;
using Dto;
using Newtonsoft.Json;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureData
{
    public class PokemonRepositoryUrls : IPokemonRepositoryUrls
    {
        private readonly string _pathFile;
        private readonly IPokemonFinderAndTransform _pokemonFinder;

        public PokemonRepositoryUrls(IPokemonFinderAndTransform pokeFinder)
        {
            _pathFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocalFiles", "PokeBase.txt");
            _pokemonFinder = pokeFinder;
        }
        public async Task<List<PokemonDomainEntity>> GetAllIntroduced(List<string> urls)
        {
            List<PokemonDomainEntity> pokemonsDomainEntity = new List<PokemonDomainEntity>();

            List<PokemonDto> pokeDtos = new List<PokemonDto>();

            string allPokemons = File.ReadAllText(_pathFile);

            if (allPokemons != "")
            {
                pokeDtos = JsonConvert.DeserializeObject<List<PokemonDto>>(allPokemons);
            }

            foreach (var url in urls)
            {

                pokeDtos.Add(await _pokemonFinder.GetPokemonsFromApiUrlAsString(url));
            }

            foreach (PokemonDto pokeDto in pokeDtos)
            {
                pokemonsDomainEntity.Add(_pokemonFinder.TrasnfromDtoToEntity(pokeDto));
            }

            return pokemonsDomainEntity;

        }
        public bool PersistPokemonAsJson(List<PokemonDomainEntity> pokemon)
        {
            List<PokemonDto> dtosToPersist = new List<PokemonDto>();

            foreach (PokemonDomainEntity pokemonDomainEntity in pokemon)
            {
                dtosToPersist.Add(_pokemonFinder.TrasnfromEntityToDto(pokemonDomainEntity));
            }

            File.WriteAllText(_pathFile, JsonConvert.SerializeObject(dtosToPersist));

            return true;

        }


    }
}
