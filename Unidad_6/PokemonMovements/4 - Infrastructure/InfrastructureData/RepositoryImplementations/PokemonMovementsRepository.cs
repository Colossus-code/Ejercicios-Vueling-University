using Contracts.Dto;
using Contracts.RequestService;
using DomainEntity;
using Dto;
using Newtonsoft.Json;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace RepositoryImplementations
{
    public class PokemonMovementsRepository : IPokemonMovementsRepository
    {
        private readonly string _pathFileDto;
        private readonly string _pathFile;
        private readonly IPokemonFinderRepository _pokemonFinderRepo; 
        private const string  _pokeApiRouteType = "https://pokeapi.co/api/v2/type/";

        public PokemonMovementsRepository(IPokemonFinderRepository pokemonFindrRepo)
        {
            _pathFileDto = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocalFiles", "PokemonMovementsDto.txt");
            _pathFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocalFiles", "PokemonMovements.txt");
        }

        public List<MovementsDto> GetActualMovementsDto(RequestPokeApiModel requestPokeApi)
        {
            List<MovementsDto> movementsDto = _pokemonFinderRepo.GetListFromFile(requestPokeApi);

            return movementsDto; 

        }

        public List<MovementsDomainEntity> GetActualMovementsDomain(List<MovementsDto> movementsDtos)
        {
            List<MovementsDomainEntity> movementsDomain = _pokemonFinderRepo.GetMovements(movementsDtos);

            return movementsDomain;

        }
        public async Task<List<MovementsDto>> GetApiMovements(RequestPokeApiModel requestPokeApi)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage getTypeFromApi = await client.GetAsync(_pokeApiRouteType+requestPokeApi.Type);
            
            string resutAsString = await getTypeFromApi.Content.ReadAsStringAsync();

            List<TypesDto> typeAtacks = JsonConvert.DeserializeObject<List<TypesDto>>(resutAsString).Take(requestPokeApi.Quantity).ToList();

            List<MovementsDto> movementsDtos = new List<MovementsDto>();

            foreach (var type in typeAtacks)
            {

                HttpResponseMessage webApiResoult = await client.GetAsync(type.moves.Select(url => url.url).FirstOrDefault());

                string result = await getTypeFromApi.Content.ReadAsStringAsync();

                MovementsDto movementDto = JsonConvert.DeserializeObject<MovementsDto>(result);

                movementDto.names[0] =movementDto.names.FirstOrDefault(lenguage => lenguage.language.name.Contains(requestPokeApi.Language));
                
                movementsDtos.Add(movementDto);
    
            }


            return movementsDtos;

        }

        public void PersistMovements(List<MovementsDto> movementsDto)
        {
            string serialiceMovements = JsonConvert.SerializeObject(movementsDto);

            File.AppendAllText(_pathFileDto, serialiceMovements);
        }
    }
}
