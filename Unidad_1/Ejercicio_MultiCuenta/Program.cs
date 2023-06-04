using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_MultiCuenta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;

            Dictionary<string, int> accounts = new Dictionary<string, int>();
            Dictionary<string, decimal> moneyForAccounts = new Dictionary<string, decimal>();

            accounts.Add("ES778895", 3005);
            accounts.Add("ES887795", 2505);
            accounts.Add("ES958877", 1797);

            moneyForAccounts.Add("ES778895", 120);
            moneyForAccounts.Add("ES887795", 150);
            moneyForAccounts.Add("ES958877", 85);

            do
            {
                Console.Clear();
                Console.WriteLine("Please, introduce the account number.");

                string accountNumber = Console.ReadLine();

                Console.WriteLine("Introduce the secret pin of the account, thank you.");

                int accountPin = 0;

                try
                {
                    accountPin = Convert.ToInt32(Console.ReadLine());

                }
                catch (Exception)
                {

                    Console.WriteLine("The pincode must to be numeric!"); // No seria necesario controlarla realmente, ya que indicaría data incorrecta.
                }
                Console.Clear();

                if (accounts.ContainsKey(accountNumber) && accountPin == accounts[accountNumber])
                {

                    bool exitAccount = false;


                    List<string> allMovements = new List<string>();
                    List<decimal> incomeMovements = new List<decimal>();
                    List<decimal> outcomeMovements = new List<decimal>();

                    while (!exitAccount)
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
                                moneyForAccounts[accountNumber] += incomeMoney;
                                incomeMovements.Add(incomeMoney);
                                allMovements.Add("+" + incomeMoney);

                                break;

                            case 2:

                                Console.WriteLine("Please, select the money outcome.");

                                decimal outcomeMoney = Convert.ToDecimal(Console.ReadLine());

                                if (moneyForAccounts[accountNumber] < outcomeMoney)
                                {
                                    Console.WriteLine("You haven't enought money to do this.");
                                }
                                else
                                {
                                    moneyForAccounts[accountNumber] -= outcomeMoney;
                                    outcomeMovements.Add(outcomeMoney);
                                    allMovements.Add("-" + outcomeMoney);

                                    Console.WriteLine("The money has retired.");
                                }

                                break;

                            case 3:

                                Console.WriteLine("All moves during session:");

                                for (int i = 0; i < allMovements.Count; i++)
                                {
                                    Console.WriteLine(allMovements[i]);
                                }
                                break;

                            case 4:

                                Console.WriteLine("All income moves during session: ");
                                for (int i = 0; i < incomeMovements.Count; i++)
                                {
                                    Console.WriteLine(incomeMovements[i]);
                                }
                                break;

                            case 5:

                                Console.WriteLine("All outcome moves during session: ");
                                for (int i = 0; i < outcomeMovements.Count; i++)
                                {
                                    Console.WriteLine(outcomeMovements[i]);
                                }
                                break;

                            case 6:

                                Console.WriteLine($"The current money account is: {moneyForAccounts[accountNumber]}");
                                break;

                            case 7:

                                bool allowAnswer = false;
                                while (!allowAnswer)
                                {
                                    Console.WriteLine("You want to manage other account? (yes/no)");

                                    string answer = Console.ReadLine().ToLower();

                                    switch (answer)
                                    {
                                        case "yes":

                                            allowAnswer = true;
                                            exitAccount = true;
                                            break;
                                        case "no":

                                            Console.WriteLine($"The current money account is: {moneyForAccounts[accountNumber]}");
                                            allowAnswer = true;
                                            exit = true;
                                            exitAccount = true;
                                            break;

                                        default:

                                            Console.WriteLine("The answer was wrong, please select yes or no.");
                                            break;

                                    }
                                }

                                break;

                            default:
                                Console.WriteLine("The answer was wrong, restarting the operation");
                                break;

                        }
                    }
                }
                else
                {

                    Console.WriteLine("Introduced data it's wrong. You want to try again?");

                    string answer = Console.ReadLine().ToLower();

                    switch (answer)
                    {
                        case "yes":

                            break;
                        case "no":

                            exit = true;
                            break;

                        default:

                            Console.WriteLine("The answer was wrong, restarting the operation");
                            break;

                    }
                }


            } while (!exit);
        }
    }
}
