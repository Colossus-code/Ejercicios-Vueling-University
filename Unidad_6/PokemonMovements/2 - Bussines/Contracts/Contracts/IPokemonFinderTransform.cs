using DomainEntity;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPokemonFinderTransform
    {

        LenguageMovementsDomainEntity TransformToEntity(List<MovementsDto> movementsDtoCache, string language);
    }
}
