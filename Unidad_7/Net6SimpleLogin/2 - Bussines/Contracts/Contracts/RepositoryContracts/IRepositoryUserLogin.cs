using DomainEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RepositoryContracts
{
    public interface IRepositoryUserLogin
    {

        Task<bool> PersistDb(UserDomainEntity userDomain);

        Task<UserDomainEntity>? GetUser(UserDomainEntity userDomainEntity);
    }
}
