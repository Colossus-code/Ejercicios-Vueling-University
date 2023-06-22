using Dto;
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
        public string MoveType { get; set; }      
        public List<LenguagesDomainEntity> MoveLenguage { get; set; }

    }
}
