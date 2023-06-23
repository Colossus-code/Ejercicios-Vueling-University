using Contracts.CustomExceptions;
using Contracts.RequestService;
using DomainEntity;
using Dto;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.Validators
{
    public class PokemonFinderValiator
    {
        private readonly ILogger _pokeLogger;

        private readonly List<string> _allowLang;

        public PokemonFinderValiator(ILogger logger)
        {
            _pokeLogger = logger;

            _allowLang = new List<string>
            {
                "es",
                "it",
                "en",
                "ja",
                "zh-Hans",
                "fr",
                "de",

            };
        }

        public bool ComprobeData(RequestPokeApiModel requesApiModel)
        {
            if (!_allowLang.Contains(requesApiModel.Language))
            {

                throw new NotAllowLenguageException("Not allow lenguage, select: es, it, en, ja or zh-Hans");
            }

            return true;
        }

        public bool ComprobeQuantity(List<MovementsDto> movementsDto, RequestPokeApiModel requesApiModel, LenguageMovementsDomainEntity lenguageMovementsDomainEntity)
        {
            if (movementsDto.Count() < requesApiModel.Quantity)
            {
                _pokeLogger.Warning($"Selected {requesApiModel.Quantity}, but only found {movementsDto.Count}. Writing new {movementsDto.Count} of type {lenguageMovementsDomainEntity.MovementsFound.FirstOrDefault().MoveType} in {lenguageMovementsDomainEntity.MovementsFound.FirstOrDefault().MoveLenguage}");

                throw new NotEnougthMovementsException($"Selected more moves than actual exist. Adding: {movementsDto.Count}");
            }

            return true;
        }

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
