using Infrastructure.DomainEntity;
using Infrastructure.IRepository;
using Services.Contracts;
using Services.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Services
{
    public class CommanderService : ICommanderService
    {

        private readonly IRepositoryCommanderProfile _repoCommanderProfile;

        public CommanderService(IRepositoryCommanderProfile repoCommanderProfile)
        {
            _repoCommanderProfile = repoCommanderProfile;


        }

        public (string, bool) CreateCommanderProfileWithWeapon(CommanderProfileDto commanderProfileDto, WeaponDto weaponDto)
        {

            Commander commander = new Commander
            {
                CommanderName = commanderProfileDto.CommanderName,
                Strength = commanderProfileDto.Strength,
                Resistance = commanderProfileDto.Resistance,
                Attacks = commanderProfileDto.Attacks,
                Lives = commanderProfileDto.Lives,
                Race = commanderProfileDto.Race,

            };
            
            if (weaponDto != null)
            {
                Weapon commanderWeapon = new Weapon
                {
                    WeaponName = weaponDto.WeaponName,
                    WeaponAddAtribute = weaponDto.WeaponAddAtribute,

                };

                if (commanderWeapon.WeaponName.Contains("Sword") || commanderWeapon.WeaponName.Contains("Hammer") || commanderWeapon.WeaponName.Contains("Axe"))
                {
                    commander.Strength += commanderWeapon.WeaponAddAtribute;
                }

                else if (commanderWeapon.WeaponName.Contains("Shield") || commanderWeapon.WeaponName.Contains("Armor") || commanderWeapon.WeaponName.Contains("Helmet"))
                {
                    commander.Resistance += commanderWeapon.WeaponAddAtribute;

                }
                else
                {
                    commander.Lives += commanderWeapon.WeaponAddAtribute;
                }

                commander.CommanderWeapon = commanderWeapon;
            }

            return _repoCommanderProfile.SaveCommanderProfile(commander);
        }

    }
}
