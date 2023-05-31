using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_MonoCuenta
{
    internal class Program
    {
        static void Main(string[] args)
        {


            #region Ejercicio 1
            bool exitEx1 = false;
            decimal moneyEx1 = 0;
            List<string> allMovementsEx1 = new List<string>();
            List<decimal> outcomeMovementsEx1 = new List<decimal>();
            List<decimal> incomeMovementsEx1 = new List<decimal>();


            do
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

                int selectedOption = Convert.ToInt32(Console.ReadLine());

                switch (selectedOption)
                {
                    case 1:
                        Console.WriteLine("Please, select the money income.");

                        decimal incomeMoney = Convert.ToDecimal(Console.ReadLine());
                        moneyEx1 += incomeMoney;
                        incomeMovementsEx1.Add(incomeMoney);
                        allMovementsEx1.Add("+" + incomeMoney);

                        break;

                    case 2:

                        Console.WriteLine("Please, select the money outcome.");

                        decimal outcomeMoney = Convert.ToDecimal(Console.ReadLine());

                        if (moneyEx1 < outcomeMoney)
                        {
                            Console.WriteLine("You haven't enought money to do this.");
                        }
                        else
                        {
                            moneyEx1 -= outcomeMoney;
                            outcomeMovementsEx1.Add(outcomeMoney);
                            allMovementsEx1.Add("-" + outcomeMoney);

                            Console.WriteLine("The money has retired.");
                        }

                        break;

                    case 3:

                        Console.WriteLine("All moves during session:");

                        for (int i = 0; i < allMovementsEx1.Count; i++)
                        {
                            Console.WriteLine(allMovementsEx1[i]);
                        }
                        break;

                    case 4:

                        Console.WriteLine("All income moves during session: ");
                        for (int i = 0; i < incomeMovementsEx1.Count; i++)
                        {
                            Console.WriteLine(incomeMovementsEx1[i]);
                        }
                        break;

                    case 5:

                        Console.WriteLine("All outcome moves during session: ");
                        for (int i = 0; i < outcomeMovementsEx1.Count; i++)
                        {
                            Console.WriteLine(outcomeMovementsEx1[i]);
                        }
                        break;

                    case 6:

                        Console.WriteLine($"The current money account is: {moneyEx1}");
                        break;

                    case 7:

                        Console.WriteLine($"The current money account is: {moneyEx1}");
                        exitEx1 = true;

                        break;

                    default:
                        Console.WriteLine("The answer was wrong, restarting the operation");
                        break;

                }
            } while (!exitEx1);

            #endregion
        }
    }
}
