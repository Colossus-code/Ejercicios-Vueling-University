
using Infrastructure.DomainEntity;
using Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryUnitsProfile : IRepositoryUnitsProfile
    {
        private readonly WarhammerDataBaseEntities _warhammerDb;

        public RepositoryUnitsProfile()
        {
            _warhammerDb = new WarhammerDataBaseEntities();
        }

        public List<UnitProfile> FindUnitsByRace(string faction)
        {
            List<UnitProfiles> domainDataUnitsProfile = null;
            List<UnitProfile> domainEntityUnitProfile = new List<UnitProfile>();

            domainDataUnitsProfile = _warhammerDb.UnitProfiles.Where(e => e.UnitRace == faction).ToList();

            if (domainDataUnitsProfile != null)
            {
                foreach (var unitProfile in domainDataUnitsProfile)
                {
                    domainEntityUnitProfile.Add(new UnitProfile
                    {
                        UnitName = unitProfile.UnitName,
                        Strength = unitProfile.UnitStrength,
                        Resistance = unitProfile.UnitResistence,
                        Attacks = unitProfile.UnitAttacks,
                        Race = unitProfile.UnitRace,
                        
                    });
                }
            }

            return domainEntityUnitProfile;
        }

    }
}


