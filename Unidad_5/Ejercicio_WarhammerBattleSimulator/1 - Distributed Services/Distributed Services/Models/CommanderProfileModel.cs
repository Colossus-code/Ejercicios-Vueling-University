using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Distributed_Services.Models
{
    public class CommanderProfileModel
    {
        public string CommanderName { get; set; }

        public int Strength { get; set; }

        public int Resistance { get; set; }

        public int Attacks { get; set; }

        public int Lives { get; set; }

        public string Race { get; set; }

        public WeaponModel WeaponModel { get; set; } 
    }
}
