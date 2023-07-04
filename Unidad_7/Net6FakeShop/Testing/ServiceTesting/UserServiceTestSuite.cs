using DomainEntities;
using Dto;
using Implementations;
using Implementations.CustomExceptions;
using Moq;
using RepositoryContracts;

namespace ServiceTesting
{
    public class UserServiceTestSuite
    {
        private readonly UserService _userService;
        private readonly Mock<ICustomersRepository> _repostoryMock;
        public UserServiceTestSuite()
        {

            _repostoryMock = new Mock<ICustomersRepository>();

            _userService = new UserService(_repostoryMock.Object);
        }
        [Fact]
        public void Assert_Throw_NotAllowLocationException_When_LocationIsWrong()
        {
            //Arrange 
            List<CustomerDto> usersDto = GetCustomersDto(1, "RU");

            //Act && Assert

            Assert.Throws<NotAllowLocationException>(() => _userService.AgregateUsers(usersDto));
        }
        [Fact]
        public void Assert_Throw_NotAllowIdException_When_IdIsWrong()
        {
            //Arrange 
            List<CustomerDto> usersDto = GetCustomersDto(-2, "EUR");

            //Act && Assert

            Assert.Throws<NotAllowUserId>(() => _userService.AgregateUsers(usersDto));
        }
        [Fact]
        public void Assert_NotEmpty_When_UserItsOkay()
        {
            //Arrange 
            List<CustomerDto> usersDto = GetCustomersDto(999, "EUR");

            List<Customer> customers = GetCustomersDomain();

            //Act  

            _repostoryMock.Setup(e => e.ComprobeNotExist(It.IsAny<List<Customer>>())).Returns(true);    
            
            _repostoryMock.Setup(e => e.GenerateUsers(customers)).Returns(true);

            var response = _userService.AgregateUsers(usersDto);

            //Assert

            Assert.NotEmpty(response);
        }

        private List<CustomerDto> GetCustomersDto(int id, string countryName)
        {
            return new List<CustomerDto>()
            {
                new CustomerDto
                {

                Id = id,
                CountryShortName = new CountryDto
                {
                    ShortName = countryName
                }
                }
            };
        }
        private List<Customer> GetCustomersDomain()
        {
            return new List<Customer>
            {
                new Customer {
                Id = 5,
                CustomerName = "Samuel",
                CustomerSurname = "L Jackson",
                CountryShortName = "AMR",
                OrdersId = new List<int>
                    {
                        7,9,10
                    }
                }
            };
        }
    }
}