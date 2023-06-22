using Contracts.RequestService;
using DomainEntity;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IPokemonMovementsRepository
    {
        List<MovementsDomainEntity> GetActualMovementsDomain(List<MovementsDto> movementsDtos);
        List<MovementsDto> GetActualMovementsDto(RequestPokeApiModel requestPokeApi);
        Task<List<MovementsDto>> GetApiMovements(RequestPokeApiModel requestPokeApi);
        void PersistMovements(List<MovementsDto> movementsDto);
    }
}
