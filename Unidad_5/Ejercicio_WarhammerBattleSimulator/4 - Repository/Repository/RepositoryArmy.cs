using Infrastructure.DomainEntity;
using Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryArmy : IRepositoryArmy
    {
        private readonly WarhammerDataBaseEntities _warhammerDb;

        public RepositoryArmy()
        {

            _warhammerDb = new WarhammerDataBaseEntities();

        }

        public (string, bool) SaveArmy(Army domainEntityArmy)
        {
            if (domainEntityArmy != null)
            {
                Armies domainDataArmy = new Armies
                {

                    ArmyName = domainEntityArmy.ArmyName,
                    TypeArmy = domainEntityArmy.ArmyRace,

                };

                int commanderId = 0;

                try
                {
                    if (domainEntityArmy.ArmyCommander != null)
                    {
                        commanderId = _warhammerDb.CommanderProfiles.FirstOrDefault(e => e.CommanderName.Equals(domainEntityArmy.ArmyCommander)).Id;
                    }
                    else
                    {
                        return ($"Must to select a commander name", false);
                    }
                }
                catch (Exception)
                {
                    return ($"The commander {domainEntityArmy.ArmyCommander} weren't found", false);
                }

                try
                {
                    _warhammerDb.Armies.Add(domainDataArmy);
                    _warhammerDb.SaveChanges();

                    return ($"The army {domainEntityArmy.ArmyName} has been created succesfull", true);

                }
                catch (Exception)
                {
                    return ($"Something went wrong", false);
                }
            }

            return ($"Error processing the data", false);
        }

        public (string, bool) GetArmyForName(Army domainEntityArmy)
        {
            Armies domainDataArmy = new Armies();

            try
            {
                domainDataArmy = _warhammerDb.Armies.FirstOrDefault(e => e.ArmyName.Equals(domainEntityArmy.ArmyName));

            }
            catch (Exception)
            {
                return ($"Not found army named {domainEntityArmy.ArmyName}", false);
            }

            UnitsForArmies domainDataArmys = new UnitsForArmies();

            try
            {

                domainDataArmys = _warhammerDb.UnitsForArmies.FirstOrDefault(e => e.Armies.Id == domainEntityArmy.Id);
            }
            catch
            {
                return ($"Not found units for {domainEntityArmy.ArmyName} army", false);
            }

            if (domainDataArmys != null)
            {
                string returnListArmy = $"The army {domainDataArmy.ArmyName}, haves the commander {domainDataArmy.CommanderProfiles.CommanderName}";

                if (domainDataArmys.UnitProfiles != null)
                {
                    List<UnitProfiles> unitProfiles = _warhammerDb.UnitProfiles.Where(e => e.UnitName.Equals(domainDataArmys.UnitProfiles.UnitName)).ToList();

                    foreach (var unitProfile in unitProfiles)
                    {
                        returnListArmy += $"and unit {unitProfile.UnitName}: {domainDataArmys.Quantity}";

                        //TODO 1206 sgarciam Encontrar la forma de enlazar las unidades y la cantidad 
                    }
                }

                return ("Ok", true);
            }

            return ("Ok", true);
        }

    }
}
