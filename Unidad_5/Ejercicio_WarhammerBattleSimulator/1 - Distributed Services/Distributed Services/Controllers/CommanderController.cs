using Distributed_Services.Models;
using Services.Contracts;
using Services.DataTransferObject;
using System.Net;
using System.Web.Http;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace Distributed_Services.Controllers
{
    public class CommanderController : ApiController
    {
        private readonly ICommanderService _commanderService;

        public CommanderController(ICommanderService commanderSerivce)
        {
            _commanderService = commanderSerivce;
        }

        [HttpPost]
        public IHttpActionResult InvokeCommanderService(CommanderAndWeaponModel commanderAndWeaponModel)
        {
            WeaponModel weaponModel = commanderAndWeaponModel.Weapon;
            CommanderProfileModel commanderProfileModel = commanderAndWeaponModel.Profile;

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

            string msg = null;
            bool sucess = false;
            (msg, sucess) = _commanderService.CreateCommanderProfileWithWeapon(commanderDto, weaponDto);

            if (sucess)
            {
                return Ok(msg);
            }
            else
            {
                return StatusCode(HttpStatusCode.InternalServerError); 
            }
        }
       
    }
}