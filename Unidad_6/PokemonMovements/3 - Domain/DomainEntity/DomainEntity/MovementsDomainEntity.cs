using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntity
{
    public class MovementsDomainEntity
    {
        public int MoveId { get; set; }
        public string MoveName { get; set; } 
        public string MoveType { get; set; }
        public string MoveDescription { get; set; }       
        public List<string> MoveLenguajes { get; set; }
    }
}
