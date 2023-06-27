using Contracts.Dto;
using DomainEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations
{
    public class TransformHelper
    {

        public static UserDomainEntity TransformDtoToEntityWithoutOrders(UserDto userDto)
        {
            UserDomainEntity userDomain = new UserDomainEntity
            {

                Orders = null,
                Username = userDto.Username,
                Password = new PasswordEncryptDomainEntity
                {
                    SaltPassword = Encoding.UTF8.GetString(userDto.PasswordSalt),
                    HashPassword = Encoding.UTF8.GetString(userDto.PasswordHash)
                }
            };

            return userDomain;
        }
    }
}
