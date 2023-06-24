using Contracts;
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
    public class PokemonFinderPersist : IPokemonFinderPersist
    {
        private readonly ILogger _pokeLogger;
        private readonly IPokemonFinderRepository _pokemonFinderRepository;

        private readonly IPokemonFinderTransform _pokeFinderTransform; 
        public PokemonFinderPersist(IPokemonFinderRepository repoFinder, ILogger logger, IPokemonFinderTransform pokemonFinderTransform)
        {

            _pokemonFinderRepository = repoFinder;
            _pokeLogger = logger;

            _pokeFinderTransform = pokemonFinderTransform;

        }
        public string PerisistEntity(LenguageMovementsDomainEntity movementDomain)
        {
            _pokemonFinderRepository.PersistEntity(movementDomain.ToString());

            return movementDomain.ToString();
        }
       
   
        public string PersistAndTransform(List<MovementsDto> movementsDtoCache, LenguageMovementsDomainEntity lenguageMovementsDomainEntity, RequestPokeApiModel requesApiModel)
        {

            lenguageMovementsDomainEntity = _pokeFinderTransform.TransformToEntity(movementsDtoCache, requesApiModel.Language);

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
