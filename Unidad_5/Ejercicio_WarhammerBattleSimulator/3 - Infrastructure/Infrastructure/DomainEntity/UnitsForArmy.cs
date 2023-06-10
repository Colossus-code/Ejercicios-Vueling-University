using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DomainEntity
{
    public class UnitsForArmy
    {
        public int Id { get; set; }

        public Army ArmyId { get; set; }

        public UnitProfile UnitProfilesId { get; set; }

        public int CuantityOfUnits {get; set; }
    }
}
