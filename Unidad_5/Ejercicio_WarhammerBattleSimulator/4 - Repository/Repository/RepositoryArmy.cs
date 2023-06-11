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

        public (string,bool) SaveArmy(Army domainEntityArmy)
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
                        return ($"Must to select a commander name",false);
                    }
                }
                catch (Exception)
                {
                    return ($"The commander {domainEntityArmy.ArmyCommander} weren't found",false);
                }

                try
                {
                    _warhammerDb.Armies.Add(domainDataArmy);
                    _warhammerDb.SaveChanges();

                    return ($"The army {domainEntityArmy.ArmyName} has been created succesfull",true);

                }
                catch (Exception)
                {
                    return ($"Something went wrong",false);
                }
            }

            return ($"Error processing the data",false);
        }

    }
}
