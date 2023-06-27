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
                    SaltPassword = userDto.PasswordSalt,
                    HashPassword = userDto.PasswordHash
                }
            };

            return userDomain;
        }
    }
}
