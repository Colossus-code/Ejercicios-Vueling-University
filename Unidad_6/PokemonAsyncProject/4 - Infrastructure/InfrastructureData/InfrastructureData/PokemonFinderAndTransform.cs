using Domain;
using Dto;
using Newtonsoft.Json;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureData
{
    public class PokemonFinderAndTransform : IPokemonFinderAndTransform
    {
        private readonly string _pathFileFirstGen;
        private readonly string _pathFileSecondGen;
        private readonly string _pathFileThirdGen;

        public PokemonFinderAndTransform()
        {
            _pathFileFirstGen = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocalFiles", "PokemonFirstGen.txt");
            _pathFileSecondGen = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocalFiles", "PokemonSecondGen.txt");
            _pathFileThirdGen = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocalFiles", "PokemonThirdGen.txt");

        }
        public string GetAllList(int gen)
        {

            switch (gen)
            {

                case 1:

                    return File.ReadAllText(_pathFileFirstGen);

                case 2:

                    return File.ReadAllText(_pathFileSecondGen);

                case 3:

                    return File.ReadAllText(_pathFileThirdGen);
            }

            return null;


        }
        public async Task<PokemonDto> GetPokemonsFromApiUrlAsString(string url)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage firePokemonFromApiGen = await client.GetAsync(url);

            string resutAsString = await firePokemonFromApiGen.Content.ReadAsStringAsync();

            return TransformStringToDto(resutAsString);

        }

        private PokemonDto TransformStringToDto(string listPropsAsStringsOfPokeapi) 
        {

            PokemonDto pokemonJsonAsDto = JsonConvert.DeserializeObject<PokemonDto>(listPropsAsStringsOfPokeapi);

            PokemonDto listOfPropsToSerialize = new PokemonDto
            {
                name = pokemonJsonAsDto.name,
                types = pokemonJsonAsDto.types,
                abilities = pokemonJsonAsDto.abilities,
                sprites = pokemonJsonAsDto.sprites,
                game_indices = pokemonJsonAsDto.game_indices,
                IntroducedAt = pokemonJsonAsDto.IntroducedAt,
                id = pokemonJsonAsDto.id,
            };



            return listOfPropsToSerialize;
        }

        public PokemonDomainEntity TrasnfromDtoToEntity(PokemonDto pokemonDto)
        {

            PokemonDomainEntity pokemonDomainEntity = new PokemonDomainEntity
            {
                Id = pokemonDto.id,
                Name = pokemonDto.name,
                Types = new List<string>(),
                Abilities = new List<string>()

            };

            foreach (var type in pokemonDto.types)
            {
                pokemonDomainEntity.Types.Add(type.type.name);
            }

            foreach (var ability in pokemonDto.abilities)
            {
                pokemonDomainEntity.Abilities.Add(ability.ability.name);
            }

            switch (pokemonDto.id)
            {
                case int n when (n >= 0 && n <= 151):

                    pokemonDomainEntity.Generation = "first Generation";
                    break;

                case int n when (n >= 152 && n <= 251):

                    pokemonDomainEntity.Generation = "second Generation";
                    break;

                case int n when (n >= 252 && n <= 386):

                    pokemonDomainEntity.Generation = "third Generation";
                    break;

                default:

                    pokemonDomainEntity.Generation = null;
                    break;
            }

            return pokemonDomainEntity;
        }

        public PokemonDto TrasnfromEntityToDto(PokemonDomainEntity pokemonDomain)
        {
            PokemonDto pokemonDto = new PokemonDto
            {
                id = pokemonDomain.Id,
                name = pokemonDomain.Name,
                types = new Dto.Type[2],
                abilities = new Ability[4]

            };

            int iterator = 0;
            foreach (var types in pokemonDomain.Types)
            {

                pokemonDto.types[iterator] = new Dto.Type
                {
                    type = new Type1 
                    {
                        name = types
                    }
                };

                iterator++;
            }

            iterator = 0;

            foreach (var ability in pokemonDomain.Abilities)
            {

                pokemonDto.abilities[iterator] = new Ability
                {

                    ability = new Ability1
                    {
                        name = ability
                    }
                };

                iterator++;
            }

            return pokemonDto;
        }
    }
}
