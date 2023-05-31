using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_Variables
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Introduce a bool value: (true/false)");

            string valbool = Console.ReadLine();
            bool selectedTrue = true;

            if (valbool.ToLower().Equals("true"))
            {

                Console.WriteLine("Your opposite selecciton was: " + !selectedTrue);
            }
            else if (valbool.ToLower().Equals("false"))
            {

                Console.WriteLine("Your opposite selecciton was: " + selectedTrue);

            }
            else
            {
                Console.WriteLine("Error, you musted to select true or false.");
            }

            Console.WriteLine("Introduce a entery number:");

            int number = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Introduce a decimal number:");

            decimal numberDecimal = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Result is: " + number / numberDecimal);

            Console.WriteLine("Introduce a little text:");

            string text = "(" + Console.ReadLine() + ")";

            Console.WriteLine("Introduce a character:");

            try
            {
                char charText = Convert.ToChar(Console.ReadLine());
                StringBuilder stringBuilder = new StringBuilder(text);
                stringBuilder.Insert(0, charText);
                stringBuilder.Insert(text.Length + 1, charText);

                Console.WriteLine(stringBuilder.ToString());

            }
            catch (FormatException)
            {
                Console.WriteLine("The introduced one wasn't a character");
            }

            Console.WriteLine("Introduce a date");
            try
            {
                DateTime dataTime = Convert.ToDateTime(Console.ReadLine());
                DateTime lastDate = new DateTime(dataTime.Year, dataTime.Month, DateTime.DaysInMonth(dataTime.Year, dataTime.Month));

                Console.WriteLine(lastDate);

            }
            catch (FormatException)
            {
                Console.WriteLine("Error on the date");
            }

        }
    }
}
