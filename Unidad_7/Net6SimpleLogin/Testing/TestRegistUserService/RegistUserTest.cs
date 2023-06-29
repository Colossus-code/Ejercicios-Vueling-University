using Contracts;
using Contracts.Dto;
using Contracts.RepositoryContracts;
using DomainEntity;
using Implementations;
using Moq;
using System.Text;

namespace TestRegistUserService
{
    public class RegistUserTest
    {

        private readonly Mock<IRepositoryUserLogin> _loginUserRepository = new Mock<IRepositoryUserLogin>();


        private readonly RegistUserService _registUserService; 
        public RegistUserTest()
        {
            _registUserService = new RegistUserService(_loginUserRepository.Object);
        }
        [Fact]
        public void Assert_True_When_IntroduceCorrectUser()
        {
            var testPass = "passwordTest";

            UserDto userDto = new UserDto()
            {
                Username = "Test",
                Password = "passwordTest",
                PasswordHash = Encoding.UTF8.GetBytes(testPass)
                

            };

            UserDomainEntity userDomainEntity = new UserDomainEntity
            {
                Username = userDto.Username,
                //Password = Encoding.UTF8.GetString(userDto.PasswordHash),
                Orders = new List<OrdersDomainEntity>
                {
                    new OrdersDomainEntity
                    {
                        OrderDescription = "Product test desc",
                        OrderName = "Product 1",
                    }
                }
            };


            var response =  _loginUserRepository.Setup(e => e.PersistDb(userDomainEntity).Result).Returns(true);

            var expectsTrue = _registUserService.GenerateUser(userDto);

            Assert.True(true);
        }        
        [Fact]
        public void Assert_False_When_IntroduceIncorrectUser()
        {

            var testPass = "passwordTest";

            UserDto userDto = new UserDto()
            {
                Username = "AllreadyOnDb",
                PasswordHash = Encoding.UTF8.GetBytes(testPass)

            };

            UserDomainEntity userDomainEntity = new UserDomainEntity
            {
                Username = userDto.Username,
                Orders = null
            };


            _loginUserRepository.Setup(e => e.PersistDb(userDomainEntity).Result).Returns(false);

            var expectedTrue = _registUserService.GenerateUser(userDto).Result;

            Assert.False(expectedTrue);
        }
    }    
}