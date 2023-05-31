using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareas.Controller
{
    internal static class InputClass
    {
        public static string inputMessageString(string msg)
        {
            string inputMsg = "";
            do
            {
                Console.WriteLine(msg);
                inputMsg = Console.ReadLine();

            } while (inputMsg == "");

            return inputMsg;
        }

        public static int inputMessageInt(string msg)
        {
            int inputMsgInt = 0;
            do
            {
                Console.WriteLine(msg);

                try
                {
                    inputMsgInt = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    return 0;
                }
            } while (inputMsgInt < 0);

            return inputMsgInt;
        }

        public static DateTime inputMessageDateTime(string msg, out int workingYears)
        {
            int workerRealAge = 0;

            do
            {
                Console.WriteLine(msg);

                try
                {
                    DateTime workerBirthday = Convert.ToDateTime(Console.ReadLine());
                    var today = DateTime.Now.Date;

                    workerRealAge = (today - workerBirthday).Days / 365;

                    if (workerRealAge < 18)
                    {
                        Console.WriteLine("The employer must to be legal.");
                    }
                    else
                    {
                        var workerAge = InputClass.inputMessageInt("Please, introduce the age of the worker.");

                        if (workerAge != workerRealAge)
                        {
                            Console.WriteLine("The age haves some incoherency, check it.");
                        }
                        else
                        {
                            workingYears = InputClass.inputMessageInt("Write the experience years of the employer.");

                            if (workerRealAge < workingYears)
                            {
                                Console.WriteLine("The employer mustn't have more years of experience than lived ones");
                                continue;
                            }

                            return workerBirthday;
                        }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("You must to write a real age or date");
                }

            } while (true);
        }
    }
}
