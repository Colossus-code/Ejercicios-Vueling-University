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

        public int Id { get; set; }

        public string ArmyName { get; set; }

        public string UnitsName { get; set; }

        public int CuantityOfUnits { get; set; }
    }
}
