using Contracts;
using Contracts.CustomExceptions;
using Contracts.RequestService;
using DomainEntity;
using Dto;
using Implementations.Persist;
using RepositoryContracts;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.Validators
{
    public class PokemonFinderValidator : IPokemonFinderValidator
    {
        private readonly ILogger _pokeLogger;

        private readonly List<string> _allowLang;

        private readonly IPokemonFinderPersist _pokeFinderPersist;
        private readonly IPokemonFinderTransform _pokeFinderTransform;

        public PokemonFinderValidator(ILogger logger, IPokemonFinderPersist pokemonFinderPersist, IPokemonFinderTransform pokemonFinderTransform)
        {
            _pokeLogger = logger;
            
            _pokeFinderPersist = pokemonFinderPersist;
            _pokeFinderTransform = pokemonFinderTransform;

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
                lenguageMovementsDomainEntity = _pokeFinderTransform.TransformToEntity(movementsDto, requesApiModel.Language);

                _pokeLogger.Warning($"Selected {requesApiModel.Quantity}, but only found {movementsDto.Count}. Writing new {movementsDto.Count} of type {lenguageMovementsDomainEntity.MovementsFound.FirstOrDefault().MoveType} in {lenguageMovementsDomainEntity.MovementsFound.FirstOrDefault().MoveLenguage}");

                _pokeFinderPersist.PerisistEntity(lenguageMovementsDomainEntity);

                throw new NotEnougthMovementsException($"Selected more moves than actual exist. Adding: {movementsDto.Count}");
            }

            return true;
        }
    }
       
}
