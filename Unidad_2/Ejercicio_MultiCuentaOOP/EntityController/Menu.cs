using Ejercicio_MultiCuentaOOP.EntityController.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_MultiCuentaOOP.EntityController
{
    public class Menu : IMenu
    {
        public int showMenu()
        {
            Console.WriteLine("__________________________________");

            Console.WriteLine("Please, select one of this options: \n" +
                "1. Money income \n" +
                "2. Money outcome \n" +
                "3. List all movements \n" +
                "4. List incomes \n" +
                "5. List outcomes \n" +
                "6. Show current money \n" +
                "7. Exit");

            Console.WriteLine("__________________________________");

            try
            {
                return Convert.ToInt32(Console.ReadLine());

            }
            catch (FormatException)
            {
                Console.WriteLine("Select a valid option, please");

                return 0;
            }
        }
    }
}
