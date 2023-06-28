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

        public UserDomainEntity ? GetUser(UserDto userDto)
        {
            var userData = _dbConnection.Users.FirstOrDefault(e => e.UserName.Equals(userDto.Username));

            if(userData != null)
            {
                var userPass =  _dbConnection.UserPassword.Find(userData.UserId);

                userData.UserPassword = userPass;

                UserDomainEntity ? userDomainEntity = ValidateUser(userDto, userData);
                
                return userDomainEntity;

            }

            return null; 
           
        }

        private UserDomainEntity ? ValidateUser(UserDto userDto, Users userData)
        {

            using (var hmac = new HMACSHA512(userData.UserPassword.UserSalt))
            {
                userDto.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password));

                if (userDto.PasswordHash.SequenceEqual(userData.UserPassword.UserHash))
                {


                    return TransformFromDb.TransformDataToEntity(userData);
                }

            }

            return null;  
        }
    }
}