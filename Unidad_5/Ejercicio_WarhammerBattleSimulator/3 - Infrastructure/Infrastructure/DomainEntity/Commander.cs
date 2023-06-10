using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DomainEntity
{
    public class Commander : Character
    {
        public int Id { get; set; }

        public string CommanderName { get; set; }

        public Weapon CommanderWeapon { get; set; }
    }
}
