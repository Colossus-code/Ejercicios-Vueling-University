using Bussines.DataTransferObject;
using Distributed_Services.Models;
using Services.Contracts;
using Services.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace Distributed_Services.Controllers
{
    public class UnitsForArmyController : ApiController
    {
        private readonly IUnitsForArmyService _unitsForArmyService;

        public UnitsForArmyController(IUnitsForArmyService unitsForArmy)
        {
            _unitsForArmyService = unitsForArmy; 
        }

        [HttpPost]
        public IHttpActionResult CreateUnitsForArmy(UnitsForArmyModel unitsForArmyModel)
        {
            ArmyDto armyDto = new ArmyDto
            {
                ArmyName = unitsForArmyModel.ArmyName
            };

            UnitsProfileDto unitsProfileDto = new UnitsProfileDto
            {
                UnitName = unitsForArmyModel.UnitsName
            };

            UnitsForArmyDto unitsForArmyDto = new UnitsForArmyDto
            {
                Army = armyDto,
                Units = unitsProfileDto,
                CuantityOfUnits = unitsForArmyModel.CuantityOfUnits

            };

            bool succes = false;
            string msg = null; 

            (msg,succes) = _unitsForArmyService.AddUnitsToArmy(unitsForArmyDto);

            if(succes)
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