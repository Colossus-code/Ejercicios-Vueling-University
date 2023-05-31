using Ejercicio_MultiCuentaOOP.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ejercicio_MultiCuentaOOP.EntityController
{
    public class Motor
    {

        public List<BankAccount> accounts { get; set; }
        public BankAccount account { get; set; }

        public Menu menuSelection { get; set; }

        public Motor(List<BankAccount> bk)
        {
            this.accounts = bk;

            menuSelection = new Menu();

        }


        public bool validateInfo(string nAccount, int nPinAccount)
        {
            foreach (BankAccount accountSelected in accounts)
            {
                if (accountSelected.IBAN.Equals(nAccount) && accountSelected.pin == nPinAccount)
                {

                    this.account = accountSelected;

                    return true;
                }

            }

            Console.WriteLine("The data was wrong!");
            return false;
        }

        public int selectMenu()
        {
            return menuSelection.showMenu(); ;

        }

        public bool optionSelectedMenu(int selectedOption)
        {
            switch (selectedOption)
            {

                case 1:

                    Console.WriteLine("Select the money income: ");
                    try
                    {
                        Decimal moneyIncome = Convert.ToDecimal(Console.ReadLine());
                        if (moneyIncome != 0)
                        {
                            account.moneyAccount += moneyIncome;
                            account.allMovements.Add(moneyIncome);
                            Console.WriteLine("Money has introduced.");

                        }
                        else
                        {
                            Console.WriteLine("You can't to place this cuantity.");
                        }


                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Wrong data, please write numbers");
                    }
                    Thread.Sleep(1500);
                    Console.Clear();
                    break;

                case 2:

                    Console.WriteLine("Select the money outcome: ");
                    try
                    {
                        Decimal moneyOutcome = Convert.ToDecimal(Console.ReadLine());

                        if (moneyOutcome != 0)
                        {

                            if (moneyOutcome > account.moneyAccount)
                            {
                                Console.WriteLine("You haven't enought money to do this!");
                            }
                            else
                            {
                                account.moneyAccount -= moneyOutcome;
                                account.allMovements.Add(moneyOutcome * -1);
                                Console.WriteLine("Money has retired.");
                            }

                        }
                        else
                        {
                            Console.WriteLine("You can't to place this cuantity.");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Wrong data, please write numbers");
                    }
                    Thread.Sleep(1500);
                    Console.Clear();
                    break;

                case 3:

                    if (account.allMovements.Count() > 0)
                    {
                        Console.WriteLine("Session movements: ");
                        foreach (decimal ac in account.allMovements)
                        {
                            Console.WriteLine(ac);

                        }
                    }
                    else
                    {
                        Console.WriteLine("Not movements for now.");
                    }
                    Thread.Sleep(1500);
                    Console.Clear();
                    break;

                case 4:

                    if (account.allMovements.Where(e => e > 0).Count() > 0)
                    {
                        Console.WriteLine("Positive session movements: ");
                        foreach (decimal ac in account.allMovements)
                        {
                            if (ac > 0)
                            {
                                Console.WriteLine(ac);
                            }

                        }
                    }
                    else
                    {
                        Console.WriteLine("Not movements for now.");
                    }
                    Thread.Sleep(1500);
                    Console.Clear();
                    break;

                case 5:

                    if (account.allMovements.Where(e => e < 0).Count() > 0)
                    {
                        Console.WriteLine("Negative session movements: ");
                        foreach (decimal ac in account.allMovements)
                        {
                            if (ac < 0)
                            {
                                Console.WriteLine(ac);
                            }

                        }
                    }
                    else
                    {
                        Console.WriteLine("Not movements for now.");
                    }

                    Thread.Sleep(1500);
                    Console.Clear();
                    break;

                case 6:

                    Console.WriteLine($"Account money: {account.moneyAccount}");

                    Thread.Sleep(1500);
                    Console.Clear();
                    break;

                case 7:

                    Console.WriteLine($"Account money: {account.moneyAccount}");
                    return true;

            }

            return false;

        }
    }
}
