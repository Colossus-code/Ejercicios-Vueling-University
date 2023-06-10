using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DomainEntity
{
    public class Character
    {
        public int Strength { get; set; }

        public int Resistance { get; set; }

        public int Attacks { get; set; }

        public int Lives { get; set; }

        public string Race { get; set; }
    }
}
