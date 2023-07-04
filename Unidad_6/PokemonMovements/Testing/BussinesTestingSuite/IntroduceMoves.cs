
using Contracts;
using Contracts.CustomExceptions;
using Contracts.RequestService;
using DomainEntity;
using Dto;
using Implementations;
using Moq;
using RepositoryContracts;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Movements = DomainEntity.MovementsDomainEntity;

namespace BussinesTestingSuite
{
    public class IntroduceMoves
    {
        private readonly Mock<ILogger> _pokeLogger = new Mock<ILogger>();
        private readonly Mock<IPokemonFinderRepository> _mockPokeFinderRepo = new Mock<IPokemonFinderRepository>();
        private readonly Mock<IPokemonMovementsRepository> _mockPokeFinderRepoMovs = new Mock<IPokemonMovementsRepository>();
        private readonly Mock<IPokemonFinderPersist> _pokeFinderPersist = new Mock<IPokemonFinderPersist>();
        private readonly Mock<IPokemonFinderValidator> _pokeFinderValidator = new Mock<IPokemonFinderValidator>();
        private readonly Mock<IPokemonFinderTransform> _pokeFinderTransform = new Mock<IPokemonFinderTransform>();

        private PokemonFinderService _pokeService;


        public IntroduceMoves()
        {
            _pokeService = new PokemonFinderService(_mockPokeFinderRepoMovs.Object, _pokeLogger.Object, _pokeFinderPersist.Object, _pokeFinderValidator.Object, _pokeFinderTransform.Object);
        }

        [Fact]
        public void When_IntroduceMovesByTypeAndLng_movementsDtoCache_IsNull()
        {
            _mockPokeFinderRepoMovs.Setup(e => e.GetActualMovementsDto()).Returns((List<MovementsDto>)null);
            _mockPokeFinderRepoMovs.Setup(e => e.GetApiMovements(It.IsAny<RequestPokeApiModel>())).ReturnsAsync(new List<MovementsDto>() { new MovementsDto() { name = "sergiofuego" } });
            _mockPokeFinderRepoMovs.Setup(e => e.PersistMovements(new List<MovementsDto>()));
            _pokeFinderTransform.Setup(e => e.TransformToEntity(It.IsAny<List<MovementsDto>>(), It.IsAny<string>())).Returns(new LenguageMovementsDomainEntity() { MovementsFound = new List<Movements>() { new Movements() { MoveId = 1, MoveLenguage = new LenguagesDomainEntity() { Lenguage = "Japones" }, MoveType = "fire" } } }).Verifiable();
            _mockPokeFinderRepo.Setup(e => e.PersistEntity(""));

            var result = _pokeService.IntroduceMovesByTypeAndLng(new RequestPokeApiModel() { Language = "ja", Quantity = 1, Type = "fire" });

            Assert.NotNull(result);
            var response = result.Result.Contains(DateTime.UtcNow.GetDateTimeFormats()[0]);
            Assert.True(response);
            _pokeFinderTransform.Verify(e => e.TransformToEntity(It.IsAny<List<MovementsDto>>(), It.IsAny<string>()), Times.Once);

        }

        [Fact]
        public void AssertNotNull_WhenIntroduce_MoveIsActuallyOnCache()
        {
            List<MovementsDto> movements = new List<MovementsDto>
            {
                new MovementsDto()
                {
                    id = 3,
                    name = "SuperIce",
                    type = new Dto.Type
                    {
                        name = "ice"
                    }


                },
                new MovementsDto()
                {
                    id = 4 ,
                    name = "HiperIce",
                    type = new Dto.Type
                    {
                        name = "ice"
                    }
                }
            };

            LenguageMovementsDomainEntity lenguageMovementsDomainEntity = new LenguageMovementsDomainEntity()
            {
                IntroducedAt = DateTime.UtcNow,
                MovementsFound = new List<Movements>
                {
                    new Movements
                    {
                        MoveId = 3,
                        MoveLenguage = new LenguagesDomainEntity
                        {
                            MoveId = 3,
                            Lenguage = "en",
                            MovementDescByLanguage = "Nothing special",
                            MovementNameByLanguage = "SuperIce"
                        },
                        MoveType = "ice"
                    },
                    new Movements
                    {
                        MoveId = 4,
                        MoveLenguage = new LenguagesDomainEntity
                        {
                            MoveId = 4,
                            Lenguage = "en",
                            MovementDescByLanguage = "Nothing special",
                            MovementNameByLanguage = "HiperIce"
                        },
                        MoveType = "ice"
                    }
                }

            };

            _mockPokeFinderRepoMovs.Setup(e => e.GetActualMovementsDto()).Returns(movements);
            
            _pokeFinderPersist.Setup(e => e.PersistAndTransform(It.IsAny<List<MovementsDto>>(), It.IsAny<LenguageMovementsDomainEntity>(), It.IsAny<RequestPokeApiModel>()))
                .Returns(lenguageMovementsDomainEntity.ToString());
           

            var result = _pokeService.IntroduceMovesByTypeAndLng(new RequestPokeApiModel() { Language = "en", Quantity = 2, Type = "ice" });

            Assert.True(result.Result == lenguageMovementsDomainEntity.ToString());
        }
        [Fact]
        public void AssertNotNull_WhenIntroduce_MoveIsActuallyOnCacheNotLang()
        {

        }
        [Fact]
        public void AssertNotNull_WhenIntroduce_MoveIsNotActuallyOnCacheType()
        {

        }

        [Fact]
        public async void AssertException_WhenIntroduce_NotAllowLang()
        {
            RequestPokeApiModel notAllowLang = new RequestPokeApiModel
            {
                Language = "pfpfp",
                Quantity = 1,
                Type = "water"

            };

            _pokeFinderValidator.Setup(e => e.ComprobeData(notAllowLang)).Throws(new NotAllowLenguageException());

            try
            {
                await _pokeService.IntroduceMovesByTypeAndLng(notAllowLang);
               

            }
            catch (NotAllowLenguageException ex)
            {

                Assert.NotNull(ex.Message);

            }

        }


        [Fact]
        public void AssertException_WhenIntroduce_NotAllowType()
        {

        }
        [Fact]
        public void AssertException_WhenIntroduce_MoreThanExistingTypeMoves()
        {

        }
    }
}
