using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Helpers.InputValidation
{
    public class DataCoherency
    {
        public bool legalAge(DateTime dateTime)
        {
            var today = DateTime.Now.Date;

            var workerRealAge = (today - dateTime).Days / 365;

            if (workerRealAge < 16)
            {
                Console.WriteLine("The worker must to be legal.");
                return false;
            }

            return true;
        }

        public bool yearsWorkingCoherency(DateTime dateTime, int workingYears)
        {
            if (workingYears < 0)
            {

                Console.WriteLine("Wrong data from working years, introduce again");
                return false;
            }
            var today = DateTime.Now.Date;

            var todayIntValue = (today - dateTime).Days / 365;

            if (todayIntValue - 16 < workingYears)
            {
                Console.WriteLine("The employer mustn't have more years of experience than lived ones.");
                return false;
            }

            return true;
        }

    }
}
