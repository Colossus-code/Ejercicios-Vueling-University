using Services.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICommanderService
    {
        bool CreateCommanderProfileWithWeapon(CommanderProfileDto commanderProfileDto, WeaponDto weaponDto);
    }
}
