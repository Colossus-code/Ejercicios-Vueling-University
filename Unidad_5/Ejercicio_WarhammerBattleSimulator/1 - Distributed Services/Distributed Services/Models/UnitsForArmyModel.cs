using Infrastructure.DomainEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Distributed_Services.Models
{
    public class UnitsForArmyModel
    {

        public int Id { get; set; }

        public Army ArmyId { get; set; }

        public UnitProfile UnitProfilesId { get; set; }

        public int CuantityOfUnits { get; set; }
    }
}