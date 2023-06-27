using Contracts;
using Contracts.Dto;
using Contracts.RepositoryContracts;
using DomainEntity;
using SimpleLoginRepository.Models;

namespace Implementations
{
    public class RegistUserService : IRegistUserService
    {
        private readonly IRepositoryUserLogin _repositoryLogin;

        public RegistUserService(IRepositoryUserLogin repoLogin)
        {
            _repositoryLogin = repoLogin;


        }

        public async Task<bool> GenerateUser(UserDto userDto)
        {
            UserDomainEntity userDomain = TransformHelper.TransformDtoToEntityWithoutOrders(userDto);

            var result = await _repositoryLogin.PersistDb(userDomain);

            return result;
            
            

        }
    }
}