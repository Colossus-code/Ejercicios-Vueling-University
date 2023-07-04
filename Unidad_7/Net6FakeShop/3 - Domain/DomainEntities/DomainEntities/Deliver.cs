using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities
{
    public class Deliver
    {

        public int Id { get; set; } 
        public Dictionary<int,int> ProductsIdQuantity { get; set; }

        public int CustomerId { get; set; }

        public decimal TotalPriece { get; set; }

        public DateTime DeliverDay { get; set; }

    }
}
