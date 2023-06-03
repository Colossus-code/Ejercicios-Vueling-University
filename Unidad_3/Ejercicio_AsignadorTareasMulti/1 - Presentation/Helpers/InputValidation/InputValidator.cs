using Ejercicio_AsignadorTareas.Enum;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareasMulti._1___Presentation.Helpers.InputValidation
{
    public class InputValidator
    {
        // Validates the input of the program 
        public string validationStringEntry(string msg)
        {
            Console.WriteLine(msg);

            string introducedStringValue = Console.ReadLine();

            if (introducedStringValue.Trim().Equals(""))
            {
                return null;
            }

            return introducedStringValue;
        }
        public int validationIntEntry(string msg)
        {
            Console.WriteLine(msg);

            int introducedIntValue = -1;
            try
            {
                return introducedIntValue = Convert.ToInt32(Console.ReadLine());

            }
            catch (FormatException)
            {
                return introducedIntValue;
            }

        }
        public DateTime validationDateTimeEntry(string msg, out bool isParsed)
        {
            Console.WriteLine(msg);

            isParsed = DateTime.TryParse(Console.ReadLine(), out DateTime dateTime);

            return dateTime;


        }
        public void validationYesOrNoEntry(string msg, out string answer)
        {
            bool validEntry = false;
            do
            {
                Console.WriteLine(msg);
                answer = Console.ReadLine();

                validEntry = !(answer.Equals("y") || answer.Equals("n")) ? false : true;

            } while (validEntry != true);
        }
        public Enum validationWorkerLevel(string msg, int workingYears)
        {
            do
            {
                var lvlSelect = validationStringEntry(msg);

                switch (lvlSelect)
                {
                    case "junior":

                        return ITWorkerLevel.junior;

                    case "medium":

                        return ITWorkerLevel.medium;

                    case "senior":

                        if (workingYears >= 5)
                        {
                            return ITWorkerLevel.senior;
                        }
                        else
                        {
                            Console.WriteLine("You must to have more than 5 years of experience to be senior.");
                        }
                        break;

                    default:
                        Console.WriteLine("Please, select a correct answer (junior , medium, senior).");
                        break;
                }
            } while (true);

        }
    }
}

