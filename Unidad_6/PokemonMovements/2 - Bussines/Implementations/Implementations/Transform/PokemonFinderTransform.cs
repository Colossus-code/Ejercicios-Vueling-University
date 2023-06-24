using Contracts;
using DomainEntity;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.Transform
{
    public class PokemonFinderTransform : IPokemonFinderTransform
    {
        public LenguageMovementsDomainEntity TransformToEntity(List<MovementsDto> movementsDtoCache, string language)
        {
            LenguageMovementsDomainEntity lenguageMovementsDomainEntity = new LenguageMovementsDomainEntity();

            List<MovementsDomainEntity> movementsDomainEntities = new List<MovementsDomainEntity>();

            foreach (MovementsDto moveDto in movementsDtoCache)
            {
                movementsDomainEntities.Add(new MovementsDomainEntity
                {
                    MoveId = moveDto.id.Value,
                    MoveType = moveDto.type.name,
                    MoveLenguage = new LenguagesDomainEntity
                    {
                        Lenguage = language,
                        MoveId = moveDto.id.Value,
                        MovementNameByLanguage = moveDto.names.Where(e => e.language.name.Equals(language)).Select(e => e.name).FirstOrDefault(),
                        MovementDescByLanguage = moveDto.flavor_text_entries.Where(e => e.language.name.Equals(language)).Select(e => e.flavor_text).FirstOrDefault(),
                    }
                });
            }

            lenguageMovementsDomainEntity.MovementsFound = movementsDomainEntities;

            return lenguageMovementsDomainEntity;
        }
    }
}

