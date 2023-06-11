using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Distributed_Services.Models
{
    public class UnitProfileModel
    {
        public string UnitName { get; set; }
        
        public int Strength { get; set; }

        public int Resistance { get; set; }

        public int Attacks { get; set; }

        public string Race { get; set; }
    }
}