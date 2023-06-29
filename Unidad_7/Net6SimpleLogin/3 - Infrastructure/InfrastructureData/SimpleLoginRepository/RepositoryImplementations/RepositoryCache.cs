using Contracts.RepositoryContracts;
using DomainEntity;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLoginRepository.RepositoryImplementations
{
    public class RepositoryCache : IRepositoryCache
    {
        private readonly IMemoryCache _memoryCache;

        public RepositoryCache(IMemoryCache memoCache)
        {
            _memoryCache = memoCache;
        }

        public List<T> GetCache<T>(string userName)
        {
            var response = _memoryCache.Get(userName);

            return (List<T>)response; 
        }

        public void SetCache<T>(string userName, List<T> generic)
        {
            _memoryCache.Set(userName, generic);
        }
    }
}
