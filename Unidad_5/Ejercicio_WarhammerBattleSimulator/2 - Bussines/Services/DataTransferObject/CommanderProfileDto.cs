using Infrastructure.DomainEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataTransferObject
{
    public class CommanderProfileDto
    {
        public string CommanderName { get; set; }

        public int Strength { get; set; }

        public int Resistance { get; set; }

        public int Attacks { get; set; }

        public int Lives { get; set; }

        public string Race { get; set; }

        public WeaponDto WeaponDto { get; set; }
    }
}
