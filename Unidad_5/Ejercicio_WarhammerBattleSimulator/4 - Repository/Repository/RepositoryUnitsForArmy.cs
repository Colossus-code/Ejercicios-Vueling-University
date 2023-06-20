using Infrastructure.DomainEntity;
using Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryUnitsForArmy : IRepositoryUnitsForArmy
    {
        private readonly WarhammerDataBaseEntities _warhammerDb;

        public RepositoryUnitsForArmy()
        {
            _warhammerDb = new WarhammerDataBaseEntities();
        }

        public (string,bool) AddUnitsForArmy(UnitsForArmy unitsForArmies)
        {
            if(unitsForArmies != null)
            {

                UnitsForArmies domainDataUnitsForArmies = _warhammerDb.UnitsForArmies.Add(new UnitsForArmies
                {
                    Quantity = unitsForArmies.CuantityOfUnits,
                });

                Armies domainDataArmy = null; 
                try
                {
                    domainDataArmy = _warhammerDb.Armies.FirstOrDefault(e => e.ArmyName.Equals(unitsForArmies.ArmyId.ArmyName));

                }
                catch (Exception)
                {
                    return ($"Not found a unit named {unitsForArmies.ArmyId.ArmyName}", false);

                }

                UnitProfiles domainDataUnitsProfiles = null;

                try
                {
                    domainDataUnitsProfiles = _warhammerDb.UnitProfiles.FirstOrDefault(e => e.UnitName.Equals(unitsForArmies.UnitProfilesId.UnitName));

                }
                catch (Exception)
                {
                    return ($"Not found a unit named {unitsForArmies.UnitProfilesId.UnitName}", false);

                }

                domainDataUnitsForArmies.UnitProfiles = domainDataUnitsProfiles;
                domainDataUnitsForArmies.Armies = domainDataArmy;

                try
                {
                    _warhammerDb.UnitProfiles.Add(domainDataUnitsProfiles);

                    _warhammerDb.SaveChanges();

                    return ($"You add {unitsForArmies.CuantityOfUnits} of {unitsForArmies.UnitProfilesId.UnitName} for your army {unitsForArmies.ArmyId.ArmyName}", true);
                }
                catch(Exception)
                {
                    return ("Something gone wrong", false);
                }

            }
            
            return ("Not valid data", false);

        }
    }
}
