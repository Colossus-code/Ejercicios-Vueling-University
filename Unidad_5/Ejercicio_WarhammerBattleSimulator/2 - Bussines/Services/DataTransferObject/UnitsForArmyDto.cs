using Bussines.DataTransferObject;
using Infrastructure.DomainEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataTransferObject
{
    public class UnitsForArmyDto
    {

        public ArmyDto Army { get; set; }

        public UnitsProfileDto Units { get; set; }

        public int CuantityOfUnits { get; set; }
    }
}
