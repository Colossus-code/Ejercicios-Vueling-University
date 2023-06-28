using Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ILoginUserService
    {
        bool LoggingUser(UserDto userDto);
        bool ValidateToken(string accessToken);
    }
}
