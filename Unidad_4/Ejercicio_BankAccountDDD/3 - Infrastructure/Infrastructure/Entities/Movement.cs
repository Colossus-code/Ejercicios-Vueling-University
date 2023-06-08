using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Movement
    {
        public int Id { get; set; }
        public DateTime DateMovement { get; set; }
        public decimal Amount { get; set; }
        public int AccountId { get; set; }

    }
}
