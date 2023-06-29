using Contracts;
using Contracts.CustomExceptions;
using Contracts.Dto;
using Contracts.RepositoryContracts;
using DomainEntity;
using Implementations;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Text;

namespace TestRegistUserService
{
    public class LoginUserTest
    {

        private readonly Mock<IRepositoryUserLogin> _loginUserRepository = new Mock<IRepositoryUserLogin>();
        private readonly Mock<IConfiguration> _config = new Mock<IConfiguration>();

        private readonly LoginUserService _loginUserService;
        public LoginUserTest()
        {
            _loginUserService = new LoginUserService(_loginUserRepository.Object, _config.Object);
        }

        [Fact]
        public void Assert_Exception_WhenUserItsNotValid()
        {
            UserDto userDto = new UserDto
            {
                Username = "Not Valid",
                Password = "password"

            };

            var user = _loginUserRepository.Setup(e => e.GetUser(userDto)).Returns((UserDomainEntity) null);

            Assert.Throws<DataIntroducedErrorException>(() => _loginUserService.LoggingUser(userDto));

        }

        [Fact]
        public void Assert_True_WhenUserItsValid()
        {
            UserDto userDto = new UserDto
            {
                Username = "Valid User",
                Password = "thisPasswordIsH4rD!"

            };

            UserDomainEntity userDom = new UserDomainEntity
            {
                Username = userDto.Username,
                Password = new PasswordEncryptDomainEntity
                {
                    HashPassword = new byte[1],
                    SaltPassword = new byte[2]

                },
                Orders = new List<OrdersDomainEntity>
                {
                    new OrdersDomainEntity
                    {
                        OrderName = "Guerreros del caos",
                        OrderDescription = "Tipejoz duroz",
                        DeliverTime = DateTime.UtcNow.AddDays(1)
                    },
                    new OrdersDomainEntity 
                    {
                        OrderName = "Orkoz",
                        OrderDescription = "20 orkoz maz duroz",
                        DeliverTime = DateTime.UtcNow.AddYears(100)
                    
                    }
                }
            };

            var user = _loginUserRepository.Setup(e => e.GetUser(userDto)).Returns(userDom);

            var result = _loginUserService.LoggingUser(userDto);

            Assert.True(result);
        }
    }


}
