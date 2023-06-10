using Infrastructure.DomainEntity;
using Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryCommanderProfile : IRepositoryCommanderProfile
    {
        private readonly WarhammerDataBaseEntities _warhammerDb;

        public RepositoryCommanderProfile()
        {
            _warhammerDb = new WarhammerDataBaseEntities();
        }

        public bool SaveCommanderProfile(Commander commander)
        {
            if(commander != null)
            {
                CommanderProfiles domainDataCommander = new CommanderProfiles
                {
                    CommanderName = commander.CommanderName,
                    CommanderStrength = commander.Strength,
                    CommanderResistance = commander.Resistance,
                    CommanderLives = commander.Lives,
                    CommanderAttacks = commander.Attacks,
                    
                };

                if(commander.CommanderWeapon != null )
                {
                    Weapons domainDataWeapon = _warhammerDb.Weapons.FirstOrDefault(e => e.Id == commander.CommanderWeapon.Id);

                    domainDataCommander.Weapons = domainDataWeapon; 
                }

                WarhammerDataBaseEntities warhammerDataBaseEntities = new WarhammerDataBaseEntities();

                warhammerDataBaseEntities.CommanderProfiles.Add(domainDataCommander);

                try
                {
                    _warhammerDb.SaveChanges();

                    return true;

                }
                catch (Exception)
                {

                    return false;
                }

            }

            return false;
        }
    }
}
