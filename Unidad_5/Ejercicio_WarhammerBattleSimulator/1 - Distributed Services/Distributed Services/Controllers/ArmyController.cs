using Bussines.DataTransferObject;
using Distributed_Services.Models;
using Services.Contracts;
using System.Net;
using System.Web.Http;

namespace Distributed_Services.Controllers
{
    public class ArmyController : ApiController
    {
        private readonly IArmyService _armyService;

        public ArmyController(IArmyService armyService)
        {
            _armyService = armyService;
        }


        [HttpGet]
        public IHttpActionResult InvokeArmyServiceGet(string armyName)
        {
            ArmyDto armydto = new ArmyDto
            {
                ArmyName = armyName
            };

            return Ok("ok");

        }
        
        
        [HttpPost]
        public IHttpActionResult InvokeArmyServiceCreation(ArmyModel armyModel)
        {
            ArmyDto armyDto = new ArmyDto
            {
                ArmyCommander = armyModel.ArmyCommander,
                ArmyName = armyModel.ArmyName,
                ArmyRace = armyModel.ArmyRace
            };

            string msg = null;
            bool sucess = false;

            (msg, sucess) = _armyService.CreateArmy(armyDto);

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