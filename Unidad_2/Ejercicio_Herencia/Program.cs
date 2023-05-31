using Ejercicio_Herencia.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_Herencia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Son son = null;

            string sonName = "";
            int sonAge = 0;
            string sonWork = "";

            string fatherName = "";
            int fatherAge = 0;
            string fatherWork = "";

            string grandFatherName = "";
            int grandFatherAge = 0;
            string grandFatherWork = "";


            Console.WriteLine("Select one of the possible options: ");
            do
            {
                Console.WriteLine("\r 1. Show values. \n \r 2. Change values. \n \r 3. Exit program. \n");
                string answer = Console.ReadLine();
                if (!optionSelected(answer))
                {
                    break;
                }


            } while (true);


            bool optionSelected(string option)
            {
                switch (option)
                {
                    case "1":

                        showValues();
                        return true;

                    case "2":
                        inputValues();
                        createOrModify();
                        return true;

                    case "3":

                        return false;
                    default:

                        Console.WriteLine("Please, select a valid option");
                        return true;

                }
            }

            void showValues()
            {
                try
                {
                    son.showData();

                }
                catch (Exception)
                {
                    Console.WriteLine("You first must to create something to show!");
                }

            }

            void inputValues()
            {
                do
                {


                    try
                    {
                        Console.WriteLine("Introduce a name for the son:");
                        sonName = Console.ReadLine();
                        Console.WriteLine("Introduce a age for the son:");
                        sonAge = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Introduce a work for the son:");
                        sonWork = Console.ReadLine();

                        Console.WriteLine("Introduce a name for the father:");
                        fatherName = Console.ReadLine();
                        Console.WriteLine("Introduce a age for the father:");
                        fatherAge = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Introduce a work for the father:");
                        fatherWork = Console.ReadLine();

                        Console.WriteLine("Introduce a name for the grandfather:");
                        grandFatherName = Console.ReadLine();
                        Console.WriteLine("Introduce a age for the grandfather:");
                        grandFatherAge = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Introduce a work for the grandfather:");
                        grandFatherWork = Console.ReadLine();

                        if (grandFatherAge < fatherAge || fatherAge < sonAge || grandFatherAge < sonAge)
                        {
                            Console.WriteLine("The father or grandfather mustn't be young than son or father.");
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("The entry values must to be correct");
                    }
                } while (true);
            }
            void createOrModify()
            {

                if (son == null)
                {

                    son = new Son(sonName, sonAge, sonWork, fatherName, fatherAge, fatherWork, grandFatherName, grandFatherAge, grandFatherWork);
                }
                else
                {
                    son.changeValuesOfSon(sonName, sonAge, sonWork, fatherName, fatherAge, fatherWork, grandFatherName, grandFatherAge, grandFatherWork);
                }
            }
        }
    }
}

