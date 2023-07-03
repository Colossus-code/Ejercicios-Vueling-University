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
        public List<int> ProductsId { get; set; }

        public int CustomerId { get; set; }

        public decimal TotalPriece { get; set; }

    }
}
