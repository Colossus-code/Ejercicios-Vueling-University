using Contracts;
using Contracts.CustomExceptions;
using Contracts.RequestService;
using DomainEntity;
using Dto;
using RepositoryContracts;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations
{
    public class PokemonFinderService : IPokemonFinderService
    {
        private readonly IPokemonLenguajesRepository _pokemonLenguajesRepository;
        private readonly IPokemonMovementsRepository _pokemonMovementsRepository;
        private readonly IPokemonFinderRepository _pokemonFinderRepository;
        private readonly ILogger _pokeLogger;

        private readonly List<string> _allowLang;

        public PokemonFinderService(IPokemonLenguajesRepository repoLenguages, IPokemonMovementsRepository repoMovements, IPokemonFinderRepository repoFinder, ILogger logger)
        {
            _pokemonLenguajesRepository = repoLenguages;
            _pokemonMovementsRepository = repoMovements;
            _pokemonFinderRepository = repoFinder;
            _pokeLogger = logger;

            _allowLang = new List<string>
            {
                "es",
                "it",
                "en",
                "ja",
                "zh-Hans"

            };

        }

        public async Task<string> IntroduceMovesByTypeAndLng(RequestPokeApiModel requesApiModel)
        {

            ComprobeData(requesApiModel);

            List<MovementsDto> movementsDtoCache = _pokemonMovementsRepository.GetActualMovementsDto(requesApiModel);


            if (movementsDtoCache == null)
            {
                _pokeLogger.Information("Not found cache data");

                movementsDtoCache = await _pokemonMovementsRepository.GetApiMovements(requesApiModel);

                LenguageMovementsDomainEntity lenguageMovementsDomainEntity = TransformMovementDto(movementsDtoCache);

                return lenguageMovementsDomainEntity.ToString();
            }
            else
            {
                movementsDtoCache = movementsDtoCache.Take(requesApiModel.Quantity).Where(type => type.type.name.Equals(requesApiModel.Type)).ToList();
                LenguageMovementsDomainEntity lenguageMovementsDomainEntity = ComprobeIfItsOnFile(movementsDtoCache, requesApiModel.Language);

                return lenguageMovementsDomainEntity.ToString();
            }

        }

        private LenguageMovementsDomainEntity ComprobeIfItsOnFile(List<MovementsDto> movementsDtoCache, string language)
        {
            LenguageMovementsDomainEntity lenguageMovementsDomainEntity = new LenguageMovementsDomainEntity();

            List<LenguagesDomainEntity> lenguagesDomainEntity = new List<LenguagesDomainEntity>();

            List<MovementsDomainEntity> movementsDomainEntities = new List<MovementsDomainEntity>();

            movementsDomainEntities = _pokemonMovementsRepository.GetActualMovementsDomain(movementsDtoCache);

            lenguagesDomainEntity = _pokemonLenguajesRepository.GetLenguage(movementsDtoCache).
                Where(lenguage => lenguage.MovementNameByLanguage.Keys.Equals(language)).ToList();


            lenguageMovementsDomainEntity.MovementsFound = movementsDomainEntities;

            return lenguageMovementsDomainEntity;
        }

        private LenguageMovementsDomainEntity TransformMovementDto(List<MovementsDto> movementsDto)
        {
            _pokemonMovementsRepository.PersistMovements(movementsDto);

            List<MovementsDomainEntity> movementsDomainEntities = _pokemonFinderRepository.GetMovements(movementsDto);

            List<LenguagesDomainEntity> lenguagesDomainEntities = _pokemonLenguajesRepository.GetLenguage(movementsDto);

            

            //todo aplicar persistencia como dto 
        }
        

        private bool ComprobeData(RequestPokeApiModel requesApiModel)
        {
            if (_allowLang.Contains(requesApiModel.Language)){

                throw new NotAllowLenguageException("Not allow lenguage, select: es, it, en, ja or zh-Hans");
            }

            return true;
        }
    }
}
