using Ejercicio_AsignadorTareas.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareasMono
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool ext = false;
            Menu optionMenu = new Menu();

            Console.WriteLine("****************************************************************");
            Console.WriteLine("Wellcome to Task Planifier, please, select one option to start:");
            Console.WriteLine("**************************************************************** \n");
            do
            {
                optionMenu.showMenu();

                try
                {
                    int selectedValue = Convert.ToInt32(Console.ReadLine());
                    ext = optionMenu.selectedChoice(selectedValue);
                }
                catch (FormatException) { Console.WriteLine("You must to select a numeric value."); }



            } while (!ext);
        }
    }
}
