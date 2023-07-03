using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public string CustomerSurname { get; set; }

        public List<DeliverDto> OrdersId { get; set; }

        public CountryDto CountryShortName { get; set; }

        public bool ValidateCustomerId()
        {
            if(Id <= 0)
            {
                return false;
            }
            else
            {
                return true; 
            }
        }
    }
}
