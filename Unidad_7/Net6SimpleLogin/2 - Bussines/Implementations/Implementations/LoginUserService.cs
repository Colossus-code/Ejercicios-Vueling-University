using Contracts;
using Contracts.CustomExceptions;
using Contracts.Dto;
using Contracts.RepositoryContracts;
using DomainEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations
{
    public class LoginUserService : ILoginUserService
    {
        private readonly IRepositoryUserLogin _repositoryLogin;

        public LoginUserService(IRepositoryUserLogin repoLogin)
        {
            _repositoryLogin = repoLogin;   
        }

        public bool LoggingUser(UserDto userDto, string password)
        {
            UserDomainEntity userDomain = new UserDomainEntity
            {
                Orders = null,
                Username = userDto.Username,
                Password = new PasswordEncryptDomainEntity
                {
                    HashPassword = password
                }
            };

            if (_repositoryLogin.GetUser(userDomain) != null)
            {
                return true;
            }
            else
            {
                throw new DataIntroducedErrorException("Wrong username or password");
            }

            
            
        }

    }
}
