using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DomainEntity
{
    public class Army
    {
        public int Id { get; set; }

        public string ArmyName { get; set; }

        public string ArmyCommander { get; set; }    

        public string ArmyRace { get; set; }
    }
}
