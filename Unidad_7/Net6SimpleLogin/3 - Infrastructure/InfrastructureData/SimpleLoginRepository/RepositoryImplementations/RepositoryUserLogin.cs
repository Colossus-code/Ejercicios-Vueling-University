using Contracts.Dto;
using Contracts.RepositoryContracts;
using DomainEntity;
using SimpleLoginRepository.Models;
using System.Security.Cryptography;
using System.Text;

namespace SimpleLoginRepository.RepositoryImplementations
{
    public class RepositoryUserLogin : IRepositoryUserLogin
    {
        private OrderTrackerContext _dbConnection;

        public RepositoryUserLogin()
        {
            _dbConnection = new OrderTrackerContext();
        }
        public async Task<bool> PersistDb(UserDomainEntity userDomain)
        {
            Users user = new Users
            {
                Orders = null,
                UserName = userDomain.Username,
                UserPassword = new UserPassword()
            };

            user.UserPassword = new UserPassword()
            {
                User = user,
                UserSalt = userDomain.Password.SaltPassword,
                UserHash = userDomain.Password.HashPassword
            };

            _dbConnection.Users.Add(user); 

            await _dbConnection.SaveChangesAsync();

            return true;
        }

        public async Task<UserDomainEntity> ? GetUser(UserDomainEntity userDomainEntity)
        {
            var userData = _dbConnection.Users.FirstOrDefault(e => e.UserName.Equals(userDomainEntity.Username));

            if(userData != null)
            {
                var userPass = await _dbConnection.UserPassword.FindAsync(userData.UserId);

                userData.UserPassword = userPass;

                if (ValidateUser(userDomainEntity, userData))
                {
                    return userDomainEntity;
                }
                else
                {
                    return null; 
                }
            }

            return null; 
           
        }

        private bool ValidateUser(UserDomainEntity userDomainEntity, Users userData)
        {

            UserDto userDto = new UserDto();

            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(userData.UserPassword.UserSalt)))
            {
                userDto.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDomainEntity.Password.HashPassword));

                string tempPass = Encoding.UTF8.GetString(userDto.PasswordSalt);

                if (tempPass.Equals(userData.UserPassword.UserHash))
                {
                    userDomainEntity =  TransformFromDb.TransformDataToEntity(userData);

                    return true;
                }

            }

            return false;
        }
    }
}