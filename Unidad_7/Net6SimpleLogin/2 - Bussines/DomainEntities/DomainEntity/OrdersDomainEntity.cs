using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntity
{
    public class OrdersDomainEntity
    {
        public string OrderName { get; set; } = string.Empty;

        public string? OrderDescription { get; set; }

        public DateTime DeliverTime { get; set; }

    }
}
