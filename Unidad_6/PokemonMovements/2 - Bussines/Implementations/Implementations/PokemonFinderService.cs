using Contracts;
using Contracts.CustomExceptions;
using Contracts.RequestService;
using DomainEntity;
using Dto;
using Implementations.Persist;
using Implementations.Validators;
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
        private readonly ILogger _pokeLogger;

        private readonly IPokemonFinderPersist _pokeFinderPersist; 
        private readonly IPokemonFinderValidator _pokeFinderValidator;
        private readonly IPokemonFinderTransform _pokeFinderTransform;

        public PokemonFinderService(IPokemonMovementsRepository repoMovements, ILogger logger, IPokemonFinderPersist pokemonFinderPersist, IPokemonFinderValidator pokemonFinderValidator, IPokemonFinderTransform pokemonFinderTransform)
        {

            _pokemonMovementsRepository = repoMovements;
            _pokeLogger = logger;

            _pokeFinderPersist = pokemonFinderPersist;
            _pokeFinderValidator = pokemonFinderValidator;
            _pokeFinderTransform = pokemonFinderTransform;

        }

        public async Task<string> IntroduceMovesByTypeAndLng(RequestPokeApiModel requesApiModel)
        {

            _pokeFinderValidator.ComprobeData(requesApiModel);

            List<MovementsDto> movementsDtoCache = _pokemonMovementsRepository.GetActualMovementsDto();

            LenguageMovementsDomainEntity lenguageMovementsDomainEntity = new LenguageMovementsDomainEntity();
            
            if (movementsDtoCache == null)
            {
               return await GetWithoutCache(movementsDtoCache, requesApiModel, lenguageMovementsDomainEntity);

            }
            else
            {
                movementsDtoCache = movementsDtoCache.Where(type => type.type.name.Equals(requesApiModel.Type)).ToList();

                if (movementsDtoCache.Count >= requesApiModel.Quantity && movementsDtoCache.Count != 0)
                {

                    movementsDtoCache = movementsDtoCache.Take(requesApiModel.Quantity).ToList();


                    return _pokeFinderPersist.PersistAndTransform(movementsDtoCache, lenguageMovementsDomainEntity, requesApiModel);
                     

                }
                else if (movementsDtoCache.Count < requesApiModel.Quantity && movementsDtoCache.Count != 0)
                {

                    return await GetWithoutEnoughtCache(movementsDtoCache,requesApiModel,lenguageMovementsDomainEntity);

                }
                else
                {
                    return await GetWitoutTypeCache(movementsDtoCache,requesApiModel,lenguageMovementsDomainEntity);
                }

            }

        } 
        
        private async Task<string> GetWithoutCache(List<MovementsDto> movementsDtoCache, RequestPokeApiModel requestApiModel, LenguageMovementsDomainEntity lenguageMovementsDomainEntity)
        {
            _pokeLogger.Information("Not found cache data");

            movementsDtoCache = await _pokemonMovementsRepository.GetApiMovements(requestApiModel);

            if (movementsDtoCache.Count < requestApiModel.Quantity)
            {
                _pokemonMovementsRepository.PersistMovements(movementsDtoCache);

                lenguageMovementsDomainEntity = _pokeFinderTransform.TransformToEntity(movementsDtoCache, requestApiModel.Language);

                _pokeLogger.Warning($"Selected {requestApiModel.Quantity}, but only found {movementsDtoCache.Count}. Writing new {movementsDtoCache.Count} of type {lenguageMovementsDomainEntity.MovementsFound.FirstOrDefault().MoveType} in {lenguageMovementsDomainEntity.MovementsFound.FirstOrDefault().MoveLenguage}");

                _pokeFinderPersist.PerisistEntity(lenguageMovementsDomainEntity);

                throw new NotEnougthMovementsException($"Selected more moves than actual exist. Adding: {movementsDtoCache.Count}");
            }
            else
            {
                movementsDtoCache = movementsDtoCache.Take(requestApiModel.Quantity).ToList();

                _pokemonMovementsRepository.PersistMovements(movementsDtoCache);

                lenguageMovementsDomainEntity = _pokeFinderTransform.TransformToEntity(movementsDtoCache, requestApiModel.Language);

                _pokeLogger.Information($"Writing new {movementsDtoCache.Count} of type {lenguageMovementsDomainEntity.MovementsFound.FirstOrDefault().MoveType} in {lenguageMovementsDomainEntity.MovementsFound.FirstOrDefault().MoveLenguage.Lenguage}");

                _pokeFinderPersist.PerisistEntity(lenguageMovementsDomainEntity);

                return lenguageMovementsDomainEntity.ToString();
            }
        }

        private async Task<string> GetWithoutEnoughtCache(List<MovementsDto> movementsDtoCache, RequestPokeApiModel requestApiModel, LenguageMovementsDomainEntity lenguageMovementsDomainEntity)
        {
            int toTake = requestApiModel.Quantity - movementsDtoCache.Count;

            int lastId = movementsDtoCache.Select(e => e.id.Value).Last();

            List<MovementsDto> restMovements = await _pokemonMovementsRepository.GetRestMovements(lastId, toTake, requestApiModel);

            _pokemonMovementsRepository.PersistMovements(restMovements);

            _pokeFinderValidator.ComprobeQuantity(movementsDtoCache, requestApiModel, lenguageMovementsDomainEntity);

            _pokeLogger.Information($"Found {requestApiModel.Quantity - toTake} moves, adding {toTake} new {movementsDtoCache.Count} moves DTO.");

            return _pokeFinderPersist.PersistAndTransform(restMovements, lenguageMovementsDomainEntity, requestApiModel).ToString();
        }

        private async Task<string> GetWitoutTypeCache(List<MovementsDto> movementsDtoCache, RequestPokeApiModel requestApiModel, LenguageMovementsDomainEntity lenguageMovementsDomainEntity)
        {
            movementsDtoCache = await _pokemonMovementsRepository.GetApiMovements(requestApiModel);

            if (movementsDtoCache == null)
            {
                throw new NotRealTypeException($"The following type: {requestApiModel.Type} it's not defined like a pokemon type.");
            }

            movementsDtoCache = movementsDtoCache.Take(requestApiModel.Quantity).Where(type => type.type.name.Equals(requestApiModel.Type)).ToList();

            _pokemonMovementsRepository.PersistMovements(movementsDtoCache);

            _pokeFinderValidator.ComprobeQuantity(movementsDtoCache, requestApiModel, lenguageMovementsDomainEntity);

            return _pokeFinderPersist.PersistAndTransform(movementsDtoCache, lenguageMovementsDomainEntity, requestApiModel).ToString();

        }
    }
}
