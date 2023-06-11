using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Distributed_Services.Models
{
    public class CommanderAndWeaponModel
    {
        public CommanderProfileModel Profile { get; set; }

        public WeaponModel Weapon { get; set; }
    }
}