using Distributed_Services.Models;
using Services.Contracts;
using Services.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Distributed_Services.Controllers
{
    public class CommanderController
    {
        private readonly ICommanderService _commanderService;

        public CommanderController(ICommanderService commanderSerivce)
        {
            _commanderService = commanderSerivce;
        }

        [HttpPost]
        public bool InvokeCommanderService(CommanderProfileModel commanderProfileModel, WeaponModel weaponModel)
        {
            WeaponDto weaponDto = new WeaponDto
            {
                WeaponAddAtribute = weaponModel.WeaponAddAtribute,
                WeaponName = weaponModel.WeaponName,
            };

            CommanderProfileDto commanderDto = new CommanderProfileDto
            {
                CommanderName = commanderProfileModel.CommanderName,
                Strength = commanderProfileModel.Strength,
                Resistance = commanderProfileModel.Resistance,
                Attacks = commanderProfileModel.Attacks,
                Lives = commanderProfileModel.Lives,
                Race = commanderProfileModel?.Race,
                WeaponDto = weaponDto
            };

            return _commanderService.CreateCommanderProfileWithWeapon(commanderDto, weaponDto);
        }
    }
}