using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class CountryDto
    {

        public string ShortName { get; set; }

        public string ShortCoinName { get; set; }

        public int DeliverTaxes { get; set; }


        public bool ValidateShortName()
        {
            List<string> validShortNames = new List<string>
            {
                "EUR",
                "CHN",
                "AMR",
                "CAN"
            };

            string selectedValidShortName = validShortNames.FirstOrDefault(e => e.Equals(ShortName));

            if (ShortName == null || selectedValidShortName == null)
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
