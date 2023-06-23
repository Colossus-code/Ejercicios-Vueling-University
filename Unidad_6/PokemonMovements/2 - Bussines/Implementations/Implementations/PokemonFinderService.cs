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
        private readonly IPokemonMovementsRepository _pokemonMovementsRepository;
        private readonly IPokemonFinderRepository _pokemonFinderRepository;
        private readonly ILogger _pokeLogger;

        private readonly List<string> _allowLang;

        public PokemonFinderService(IPokemonMovementsRepository repoMovements, IPokemonFinderRepository repoFinder, ILogger logger)
        {

            _pokemonMovementsRepository = repoMovements;
            _pokemonFinderRepository = repoFinder;
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

        public async Task<string> IntroduceMovesByTypeAndLng(RequestPokeApiModel requesApiModel)
        {

            ComprobeData(requesApiModel);

            List<MovementsDto> movementsDtoCache = _pokemonMovementsRepository.GetActualMovementsDto();

            LenguageMovementsDomainEntity lenguageMovementsDomainEntity = new LenguageMovementsDomainEntity();

            if (movementsDtoCache == null)
            {
                _pokeLogger.Information("Not found cache data");

                movementsDtoCache = await _pokemonMovementsRepository.GetApiMovements(requesApiModel);

                movementsDtoCache = movementsDtoCache.Take(requesApiModel.Quantity).ToList();
                
                _pokemonMovementsRepository.PersistMovements(movementsDtoCache);

                lenguageMovementsDomainEntity = TransformToEntity(movementsDtoCache, requesApiModel.Language);

                _pokeLogger.Information($"Writing new {movementsDtoCache.Count} of type {lenguageMovementsDomainEntity.MovementsFound.FirstOrDefault().MoveType} in {lenguageMovementsDomainEntity.MovementsFound.FirstOrDefault().MoveLenguage}");

                return PerisistEntity(lenguageMovementsDomainEntity);

            }
            else
            {

                movementsDtoCache = movementsDtoCache.Where(type => type.type.name.Equals(requesApiModel.Type)).ToList();

                if (movementsDtoCache.Count >= requesApiModel.Quantity && movementsDtoCache.Count != 0)
                {

                    movementsDtoCache = movementsDtoCache.Take(requesApiModel.Quantity).ToList();

                    return PersistAndTransform(movementsDtoCache, lenguageMovementsDomainEntity ,requesApiModel).ToString();
                     

                }
                else if (movementsDtoCache.Count < requesApiModel.Quantity && movementsDtoCache.Count != 0)
                {
                    int toTake = requesApiModel.Quantity - movementsDtoCache.Count;

                    int lastId = movementsDtoCache.Select(e => e.id.Value).Last();

                    List<MovementsDto> restMovements = await _pokemonMovementsRepository.GetRestMovements(lastId, toTake, requesApiModel);

                    _pokemonMovementsRepository.PersistMovements(restMovements);

                    _pokeLogger.Information($"Found {requesApiModel.Quantity-toTake} moves, adding {toTake} new {movementsDtoCache.Count} moves DTO.");

                    return PersistAndTransform(restMovements,lenguageMovementsDomainEntity,requesApiModel).ToString();


                }
                else
                {
                    movementsDtoCache = await _pokemonMovementsRepository.GetApiMovements(requesApiModel);

                    if(movementsDtoCache == null)
                    {
                        throw new NotRealTypeException($"The following type:{requesApiModel.Type} it's not defined like a pokemon type.");
                    }
                    movementsDtoCache = movementsDtoCache.Take(requesApiModel.Quantity).Where(type => type.type.name.Equals(requesApiModel.Type)).ToList();

                    _pokemonMovementsRepository.PersistMovements(movementsDtoCache);
                  
                    return PersistAndTransform(movementsDtoCache, lenguageMovementsDomainEntity, requesApiModel).ToString();

                }

            }

        }

        private LenguageMovementsDomainEntity TransformToEntity(List<MovementsDto> movementsDtoCache, string language)
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
        private string PerisistEntity(LenguageMovementsDomainEntity movementDomain)
        {
            _pokemonFinderRepository.PersistEntity(movementDomain.ToString());

            return movementDomain.ToString();
        }
       
        private bool ComprobeData(RequestPokeApiModel requesApiModel)
        {
            if (!_allowLang.Contains(requesApiModel.Language)){

                throw new NotAllowLenguageException("Not allow lenguage, select: es, it, en, ja or zh-Hans");
            }

            return true;
        }

        private string PersistAndTransform(List<MovementsDto> movementsDtoCache, LenguageMovementsDomainEntity lenguageMovementsDomainEntity, RequestPokeApiModel requesApiModel)
        {

            lenguageMovementsDomainEntity = TransformToEntity(movementsDtoCache, requesApiModel.Language);

            string actualPresentation = _pokemonFinderRepository.GetActualPresentation();

            LenguageMovementsDomainEntity nowToPersist = new LenguageMovementsDomainEntity();

            nowToPersist.MovementsFound = new List<MovementsDomainEntity>();

            foreach(MovementsDomainEntity move in lenguageMovementsDomainEntity.MovementsFound.OrderBy(e => e.MoveId))
            {
                if(!(actualPresentation.Contains(move.MoveLenguage.MovementNameByLanguage) 
                    && actualPresentation.Contains(move.MoveId.ToString())))
                {

                    nowToPersist.MovementsFound.Add(move);
                }
            }

            _pokemonFinderRepository.PersistEntity(nowToPersist.ToString());

            _pokeLogger.Information($"Writing new {nowToPersist.MovementsFound.Count} of type {lenguageMovementsDomainEntity.MovementsFound.FirstOrDefault().MoveType} in {lenguageMovementsDomainEntity.MovementsFound.FirstOrDefault().MoveLenguage.Lenguage}");

            return nowToPersist.ToString();
                   

        }
    }
}
