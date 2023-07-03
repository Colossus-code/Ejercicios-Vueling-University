using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities
{
    public class TopSales
    {
        public int Id { get; set; }
        public List<int> ProductsId { get; set; }
        public string CountryShortName { get; set; }
    }
}
