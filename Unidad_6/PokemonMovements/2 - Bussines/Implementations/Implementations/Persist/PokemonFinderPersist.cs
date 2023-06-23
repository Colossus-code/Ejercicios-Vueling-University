using Contracts.RequestService;
using DomainEntity;
using Dto;
using Implementations.Validators;
using RepositoryContracts;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.Persist
{
    public class PokemonFinderPersist
    {
        private readonly ILogger _pokeLogger;
        private readonly IPokemonMovementsRepository _pokemonMovementsRepository;
        private readonly IPokemonFinderRepository _pokemonFinderRepository;

        private readonly PokemonFinderValiator _pokeValidations;
        public PokemonFinderPersist(IPokemonMovementsRepository repoMovements, IPokemonFinderRepository repoFinder, ILogger logger)
        {

            _pokemonMovementsRepository = repoMovements;
            _pokemonFinderRepository = repoFinder;
            _pokeLogger = logger;

            _pokeValidations = new PokemonFinderValiator(logger);

        }
        public string PerisistEntity(LenguageMovementsDomainEntity movementDomain)
        {
            _pokemonFinderRepository.PersistEntity(movementDomain.ToString());

            return movementDomain.ToString();
        }
       
   
        public string PersistAndTransform(List<MovementsDto> movementsDtoCache, LenguageMovementsDomainEntity lenguageMovementsDomainEntity, RequestPokeApiModel requesApiModel)
        {

            lenguageMovementsDomainEntity = _pokeValidations.TransformToEntity(movementsDtoCache, requesApiModel.Language);

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
