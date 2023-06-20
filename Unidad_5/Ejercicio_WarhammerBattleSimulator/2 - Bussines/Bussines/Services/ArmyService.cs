using Bussines.DataTransferObject;
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
        private readonly IRepositoryArmy _repoArmy;
        public ArmyService(IRepositoryArmy repoArmy)
        {
            _repoArmy = repoArmy;
        }

        public (string, bool) CreateArmy(ArmyDto army)
        {
            Army domainEntityArmy = new Army
            {
                ArmyName = army.ArmyName,
                ArmyRace = army.ArmyRace,
                ArmyCommander = army.ArmyCommander,

            };

            return _repoArmy.SaveArmy(domainEntityArmy);
        }

        public (string, bool) GetArmy(ArmyDto army)
        {
            Army domainEntitArmy = new Army
            {
                ArmyName = army.ArmyName,
            };

            return ("Ok", true);

        }
    }
}
