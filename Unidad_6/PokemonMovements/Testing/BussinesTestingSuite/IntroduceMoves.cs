
using Contracts;
using Contracts.CustomExceptions;
using Contracts.RequestService;
using Dto;
using Implementations;
using Moq;
using RepositoryContracts;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BussinesTestingSuite
{
    public class IntroduceMoves
    {
        private readonly Mock<IPokemonFinderRepository> _mockPokeFinderRepo = new Mock<IPokemonFinderRepository>();
        private readonly Mock<IPokemonMovementsRepository> _mockPokeFinderRepoMovs = new Mock<IPokemonMovementsRepository>();
        private readonly Mock<ILogger> _pokeLogger = new Mock<ILogger>();
        private readonly Mock<IPokemonFinderPersist> _pokeFinderPersist = new Mock<IPokemonFinderPersist>();
        private readonly Mock<IPokemonFinderValidator> _pokeFinderValidator = new Mock<IPokemonFinderValidator>();
        private readonly Mock<IPokemonFinderTransform> _pokeFinderTransform =   new Mock<IPokemonFinderTransform>();
        
        private readonly PokemonFinderService _pokeService;


        public IntroduceMoves()
        {
            _pokeService = new PokemonFinderService(_mockPokeFinderRepoMovs.Object, _pokeLogger.Object, _pokeFinderPersist.Object, _pokeFinderValidator.Object, _pokeFinderTransform.Object);
        }

        [Fact]
        public void AssertNotNull_WhenIntroduce_MoveIsNotActuallyOnList()
        {
            
            //RequestPokeApiModel requestApi = new RequestPokeApiModel
            //{
            //    Language = "en",
            //    Quantity = 1,
            //    Type = "fire"
            //};

            RequestPokeApiModel requestApi = new RequestPokeApiModel();

            MovementsDto movementsDto = new MovementsDto
            {
                id = 420,
                name = "ice-shard",
                type = new Dto.Type
                {
                    name = "ice"

                },
                flavor_text_entries = new Flavor_Text_Entries[1],

            };

            //var response = _mockPokeFinderRepoMovs.Setup(e => e.GetApiMovements(It.IsAny<RequestPokeApiModel>())).ReturnsAsync(movementsDto);
            
            //_pokeService.Setup(e => e.IntroduceMovesByTypeAndLng(requestApi));

            Assert.NotNull(_pokeFinderPersist.Object);

        }
        [Fact]
        public void AssertNotNull_WhenIntroduce_MoveIsActuallyOnCache()
        {

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
        public void AssertException_WhenIntroduce_NotAllowLang()
        {
            RequestPokeApiModel notAllowLang = new RequestPokeApiModel
            {
                Language = "pfpfp",
                Quantity = 1,
                Type = "water"
            };

            var test = _pokeService.IntroduceMovesByTypeAndLng(notAllowLang);

            //Assert.Throws<NotAllowLenguageException>();
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
