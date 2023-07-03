using Dto;

namespace Contracts
{
    public interface IUserService
    {
        string AgregateUsers(List<CustomerDto> customersDto);
    }
}