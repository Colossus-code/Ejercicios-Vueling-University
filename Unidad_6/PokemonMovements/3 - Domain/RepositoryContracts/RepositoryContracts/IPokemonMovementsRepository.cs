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
        Task<List<MovementsDto>> GetApiMovements(RequestPokeApiModel requestPokeApi);
        void PersistMovements(List<MovementsDto> movementsDto);
        List<MovementsDto> GetActualMovementsDto();
        Task<List<MovementsDto>> GetRestMovements(int lastId, int toTake, RequestPokeApiModel requestPokeApi);
    }
}
