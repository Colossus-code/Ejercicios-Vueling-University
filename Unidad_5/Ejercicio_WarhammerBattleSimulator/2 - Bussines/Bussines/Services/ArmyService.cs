using Infrastructure.DomainEntity;
using Infrastructure.IRepository;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Services
{
    public class ArmyService : IArmyService
    {
        private readonly IRepositoryUnitsProfile _repoUnitsProfile; 
        public ArmyService(IRepositoryUnitsProfile repoUnitsProfile)
        {
            _repoUnitsProfile = repoUnitsProfile;
        }

    }
}
