using Contracts.RequestService;
using DomainEntity;
using Dto;
using Newtonsoft.Json;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryImplementations
{
    public class PokemonFinderRepository : IPokemonFinderRepository
    {
        private readonly string _pathFileDto;
        private readonly string _pathFile;

        public PokemonFinderRepository()
        {
            _pathFileDto = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocalFiles", "PokemonMovementsDto.txt");
            _pathFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LocalFiles", "PokemonMovements.txt");
        }

        public List<MovementsDto> GetListFromFile(RequestPokeApiModel requestMode)
        {
            string atualData = File.ReadAllText(_pathFileDto);

            List<MovementsDto> cacheMovements = JsonConvert.DeserializeObject<List<MovementsDto>>(atualData);

            return cacheMovements; 

        }

        public List<MovementsDomainEntity> GetMovements(List<MovementsDto> movementsDto)
        {

            List<MovementsDomainEntity> movementsDomainEntity = new List<MovementsDomainEntity>();
            
            foreach(MovementsDto movement in movementsDto)
            {
                movementsDomainEntity.Add(new MovementsDomainEntity
                {
                    MoveId = movement.id,
                    MoveType = movement.damage_class.name
               
                });

            }
        
            return movementsDomainEntity;
        }
    }
}
