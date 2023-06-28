using Contracts;
using Contracts.CustomExceptions;
using Contracts.Dto;
using Contracts.RepositoryContracts;
using DomainEntity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations
{
    public class LoginUserService : ILoginUserService
    {
        private readonly IRepositoryUserLogin _repositoryLogin;
        private readonly IConfiguration _configuration;

        public LoginUserService(IRepositoryUserLogin repoLogin, IConfiguration config)
        {
            _repositoryLogin = repoLogin;
            _configuration = config;
        }

        public bool LoggingUser(UserDto userDto)
        {

            if (_repositoryLogin.GetUser(userDto) != null)
            {
                return true;
            }
            else
            {
                throw new DataIntroducedErrorException("Wrong username or password");
            }
        }

        public bool ValidateToken(string accessToken)
        {


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
               _configuration.GetSection("Jwt:Key").Value));

            var issuer = _configuration.GetSection("Jwt:Issuer").Value;

            var audience = _configuration.GetSection("Jwt:Audience").Value;

            var tokenHandler = new JwtSecurityTokenHandler();

            tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = key,
            }, out SecurityToken validatedToken);


            return true;


        }
    }
}
