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

        public (string,bool) SaveCommanderProfile(Commander domainEntityCommander)
        {
            if(domainEntityCommander != null)
            {
                CommanderProfiles domainDataCommander = new CommanderProfiles
                {
                    CommanderName = domainEntityCommander.CommanderName,
                    CommanderStrength = domainEntityCommander.Strength,
                    CommanderResistance = domainEntityCommander.Resistance,
                    CommanderLives = domainEntityCommander.Lives,
                    CommanderAttacks = domainEntityCommander.Attacks,
                    
                };



                if(domainEntityCommander.CommanderWeapon != null )
                {
                    Weapons domainDataWeapon = _warhammerDb.Weapons.FirstOrDefault(e => e.WeaponName.Equals(domainEntityCommander.CommanderWeapon.WeaponName));

                    domainDataCommander.Weapons = domainDataWeapon; 
                }

                try
                {
                    _warhammerDb.CommanderProfiles.Add(domainDataCommander);
                    _warhammerDb.SaveChanges();

                    return ($"The comander {domainEntityCommander.CommanderName} has been created", true);

                }
                catch (Exception)
                {

                    return ($"The weapon name weren't found", false);
                }

            }

            return ($"Something was wrong",false);
        }
    }
}
