using Bussines.DataTransferObject;
using Distributed_Services.Models;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Distributed_Services.Controllers
{
    public class UnitsController : ApiController
    {
        private readonly IUnitsService _unitsService;

        public UnitsController(IUnitsService unitsService)
        {
            _unitsService = unitsService;
        }

        [HttpGet]
        public List<UnitProfileModel> GetUnitsProfiles(string race)
        {
            List<UnitProfileModel> unitProfileModel = new List<UnitProfileModel>();

            List<UnitsProfileDto> unitProfilesDto = _unitsService.FindUnitsProfiles(race);

            if (unitProfilesDto != null)
            {
                foreach (var unitProfileDto in unitProfilesDto)
                {
                    unitProfileModel.Add(new UnitProfileModel
                    {
                        UnitName = unitProfileDto.UnitName,
                        Strength = unitProfileDto.Strength,
                        Resistance = unitProfileDto.Resistance,
                        Attacks = unitProfileDto.Attacks,
                        Race = unitProfileDto.Race,

                    });

                }

            }

            return unitProfileModel;
        }
    }
}
