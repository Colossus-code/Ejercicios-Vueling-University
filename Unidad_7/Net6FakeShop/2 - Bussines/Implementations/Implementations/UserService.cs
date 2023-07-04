using Contracts;
using DomainEntities;
using Dto;
using Implementations.CustomExceptions;
using RepositoryContracts;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Implementations
{
    public class UserService : IUserService
    {
        private readonly ICustomersRepository _customerRepository;
        public UserService(ICustomersRepository customerRepo)
        {
            _customerRepository = customerRepo;
        }

        public string AgregateUsers(List<CustomerDto> customersDto)
        {

            List<Customer> customers = new List<Customer>();

            ValidateInformation(customersDto);

            TransformDtoToEntity(customersDto, customers);

            if (_customerRepository.ComprobeNotExist(customers))
            {

                _customerRepository.GenerateUsers(customers);

            }
            else
            {
                throw new NotAllowUserId("The user ID introduced it's allready created.");
            }

            return JsonSerializer.Serialize(customers);
        }

        private void ValidateInformation(List<CustomerDto> customersDto)
        {
            if (!customersDto.Any(e => e.ValidateCustomerId()))
            {
                throw new NotAllowUserId($"The ID's must to be natural ones.");
            }

            if (!customersDto.Any(e => e.CountryShortName.ValidateShortName()))
            {
                throw new NotAllowLocationException($"The selected locations aren't an deliverable zone.");
            }
        }

        private void TransformDtoToEntity(List<CustomerDto> customersDto, List<Customer> customers)
        {
            foreach (CustomerDto customerDto in customersDto)
            {
                customers.Add(new Customer
                {
                    Id = customerDto.Id,
                    CountryShortName = customerDto.CountryShortName.ShortName
                });

            }
        }
    }
}