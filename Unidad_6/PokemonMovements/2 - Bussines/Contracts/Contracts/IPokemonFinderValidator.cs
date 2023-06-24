using Contracts.RequestService;
using DomainEntity;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPokemonFinderValidator
    {
        bool ComprobeQuantity(List<MovementsDto> movementsDto, RequestPokeApiModel requesApiModel, LenguageMovementsDomainEntity lenguageMovementsDomainEntity);

        bool ComprobeData(RequestPokeApiModel requesApiModel);

    }
}
