using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Helpers.Validators
{
    public class InputValidator
    {
        public int ValidateNumber(string msg)
        {
            do
            {
                Console.WriteLine(msg);
                
                try
                {
                    return Convert.ToInt32(Console.ReadLine());

                }catch(FormatException) { continue; }

            } while (true);

        }

        public string ValidateString(string msg)
        {
            do
            {
                Console.WriteLine(msg);

                string stringToReturn = Console.ReadLine();

                if(stringToReturn == null || stringToReturn.Trim().Equals(""))
                {
                    continue;
                }

                return stringToReturn;

            } while (true);
        }
    }
}
