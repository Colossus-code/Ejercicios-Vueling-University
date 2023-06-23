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
        private readonly IPokemonFinderRepository _pokemonFinderRepo; 
        private const string  _pokeApiRouteType = "https://pokeapi.co/api/v2/type/";

        public PokemonMovementsRepository(IPokemonFinderRepository pokemonFindrRepo)
        {
            _pathFileDto = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocalFiles", "PokemonMovementsDto.txt");
            _pokemonFinderRepo = pokemonFindrRepo;
        }

        public List<MovementsDto> GetActualMovementsDto()
        {
            List<MovementsDto> movementsDto = _pokemonFinderRepo.GetListFromFile();

            return movementsDto; 

        }

        public async Task<List<MovementsDto>> GetApiMovements(RequestPokeApiModel requestPokeApi)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage getTypeFromApi = await client.GetAsync(_pokeApiRouteType+requestPokeApi.Type);
            
            string resutAsString = await getTypeFromApi.Content.ReadAsStringAsync();

            TypesDto typeAtacks = JsonConvert.DeserializeObject<TypesDto>(resutAsString);

            List<MovementsDto> movementsDtos = new List<MovementsDto>();

            foreach (var urlMove in typeAtacks.moves)
            {

                HttpResponseMessage webApiResoult = await client.GetAsync(urlMove.url);

                string result = await webApiResoult.Content.ReadAsStringAsync();

                MovementsDto movementDto = JsonConvert.DeserializeObject<MovementsDto>(result);
                
                movementsDtos.Add(movementDto);
    
            }


            return movementsDtos;

        }   

        public void PersistMovements(List<MovementsDto> movementsDto)
        {
            string readText = File.ReadAllText(_pathFileDto);

            List<MovementsDto> actualMovs = JsonConvert.DeserializeObject<List<MovementsDto>>(readText);

            if(actualMovs != null)
            {
                foreach (var movementDto in actualMovs)
                {
                    movementsDto.Add(movementDto);
                }
            }

            File.WriteAllText(_pathFileDto, JsonConvert.SerializeObject(movementsDto));
        }        

        public async Task<List<MovementsDto>> GetRestMovements(int lastId, int toTake, RequestPokeApiModel requestPokeApi)
        {
            List<MovementsDto> movementsDtos = await GetApiMovements(requestPokeApi);
            List<MovementsDto> movementsDtosToResponse = new List<MovementsDto>();


            foreach (var movement in movementsDtos.Where(e => e.id > lastId && movementsDtosToResponse.Count < toTake))
            {
                movementsDtosToResponse.Add(movement);
            };

            return movementsDtosToResponse;
        }

    }
}
