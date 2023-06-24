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
    public interface IPokemonFinderPersist
    {
        string PerisistEntity(LenguageMovementsDomainEntity movementDomain);

        string PersistAndTransform(List<MovementsDto> movementsDtoCache, LenguageMovementsDomainEntity lenguageMovementsDomainEntity, RequestPokeApiModel requesApiModel);  
    }
}
