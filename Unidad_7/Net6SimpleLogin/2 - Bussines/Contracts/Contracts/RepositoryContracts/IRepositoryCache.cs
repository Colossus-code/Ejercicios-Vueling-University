using DomainEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RepositoryContracts
{
    public interface IRepositoryCache
    {
        List<T> GetCache<T>(string userName);
        void SetCache<T>(string userName, List<T> generic);
    }
}
