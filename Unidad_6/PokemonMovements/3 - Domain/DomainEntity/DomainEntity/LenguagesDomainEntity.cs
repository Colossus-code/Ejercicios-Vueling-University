using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class LenguagesDomainEntity
    {
        public int MoveId { get; set; }
        public Dictionary<string,string> MovementNameByLanguage { get; set; }
        public Dictionary<string,string> MovementDescByLanguage { get; set; }


    }
}
