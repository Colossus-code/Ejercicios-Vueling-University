using Dto;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryImplementations
{
    public class PokemonLenguajesRepository : IPokemonLenguajesRepository
    {
        
        public PokemonLenguajesRepository()
        {
            
        }


        //public List<LenguagesDomainEntity> GetLenguage(List<MovementsDto> movementsDtoCache)
        //{
        //    List<LenguagesDomainEntity> lenguagesDomain = new List<LenguagesDomainEntity>();

        //    foreach(MovementsDto movementDto in movementsDtoCache)
        //    {
        //        LenguagesDomainEntity lenguagesDomainEntity = new LenguagesDomainEntity
        //        {
        //            MoveId = movementDto.id
        //        };


        //        foreach (var mov in movementDto.names)
        //        {
        //            lenguagesDomainEntity.MovementNameByLanguage.Add(mov.language.name, mov.name);
        //        }

        //        foreach (var mov in movementDto.effect_entries)
        //        {
        //            lenguagesDomainEntity.MovementDescByLanguage.Add(mov.language.name, mov.short_effect);
        //        }

        //        lenguagesDomain.Add(lenguagesDomainEntity);

        //    }

        //    return lenguagesDomain;
        //}
    }
}
