using Bussines;
using Bussines.IService;
using Presentation.Contracts;
using Presentation.Helpers.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Presentation
{
    public class Menu : IMenu
    {
        private readonly ILoginVerification _loginVerification;

        private readonly IMenuManager _menuManager;

        private readonly InputValidator _validator = new InputValidator();

        private DomainBankAccountDto _bankAccountDto;

        private const string _DATA_MISTAKE = "The data entry has mistakes.";

        private const string _WELLCOME_MSG = "Wellcome to IBank the Intelligent Bank option!";

        public Menu(ILoginVerification logVerif, IMenuManager menuManager)
        {
            _loginVerification = logVerif;
            _menuManager = menuManager;
        }


        public void StartProgram()
        {
            bool exit = false;

            ushort trys = 3;

            Console.WriteLine(_WELLCOME_MSG);

            do
            {
                (string accountNumber, int pinIntroduced) = InputDataCredentials();

                string loginResponse = _loginVerification.ValidateLogin(accountNumber, pinIntroduced);


                if (loginResponse.Equals(_DATA_MISTAKE))
                {

                    --trys;

                    if (trys <= 0)
                    {
                        exit = true;
                    }
                    else
                    {
                        Console.WriteLine($"{_DATA_MISTAKE}\nAntemps left before the application will be destroyed: {trys}.\n");
                    }

                }
                else if(!loginResponse.Equals(_DATA_MISTAKE) && (loginResponse.Contains("Show account")))
                {
                    do
                    {
                        exit = ManageOptionSelected(loginResponse, accountNumber);

                    } while (!exit);
                }
                else if (loginResponse.Contains("administrator") && pinIntroduced == 0102)
                {
                    do
                    {
                        exit = ManageOptionSelectedAdministrator(loginResponse);
                    
                    }while (!exit);
                }
                else
                {
                    continue;
                }


            } while (!exit);
        }
        public bool ManageOptionSelected(string loginResponse, string acountNumber)
        {
            int selectedChoice;
            do
            {
                Console.WriteLine(loginResponse);
                selectedChoice = _validator.ValidateNumber("Select one choice of the list:");

                Console.Clear();

            } while (selectedChoice <= 0 && selectedChoice > 7);

            return SelectedOption(selectedChoice, acountNumber);

        }

        public bool ManageOptionSelectedAdministrator(string loginResponse)
        {
            int selectedChoice;
            do
            {
                Console.WriteLine(loginResponse);
                selectedChoice = _validator.ValidateNumber("Select one choice of the list:");

                Console.Clear();

            } while (selectedChoice <= 0 && selectedChoice > 3);

            return SelectedOptionAdministrator(selectedChoice);

        }
        private (string accountId, int pinNumber) InputDataCredentials()
        {

            string accountNumber = _validator.ValidateString("Please, introduce the account identicator");

            int pinNumber = _validator.ValidateNumber("Please, introduce the account pin number.");

            return (accountNumber, pinNumber);

        }

        private bool SelectedOption(int optionSelected, string accountNumber)
        {
            switch (optionSelected)
            {
                case 1:

                    Console.Clear();
                    Console.WriteLine(_menuManager.GetMovements(accountNumber, "all"));
                    return false;

                case 2:

                    Console.Clear();
                    Console.WriteLine(_menuManager.GetMovements(accountNumber, "positive"));
                    return false;

                case 3:
                    Console.Clear();
                    Console.WriteLine(_menuManager.GetMovements(accountNumber, "negative"));
                    return false;

                case 4:

                    Console.Clear();

                    decimal amountIncome = 0;

                    do
                    {
                        amountIncome = _validator.ValidateDecimal("Introduce the amount of money to income.");

                    } while (amountIncome <= 0);

                    Console.WriteLine(_menuManager.GenerateInput(accountNumber, amountIncome));

                    return false;

                case 5:

                    Console.Clear();

                    decimal amountOutcome = 0;

                    do
                    {
                        amountOutcome = _validator.ValidateDecimal("Introduce the amount of money to outcome.");

                    } while (amountOutcome <= 0);

                    Console.WriteLine(_menuManager.GenerateOutput(accountNumber, amountOutcome * -1));

                    return false;

                case 6:

                    Console.Clear();

                    string newPin = _validator.ValidateString("Introduce the new pin of the account");

                    Console.WriteLine(_menuManager.ChangePinAccount(accountNumber, newPin));

                    return false;

                case 7:

                    return true;


            }

            return true;
        }

        private bool SelectedOptionAdministrator(int optionSelected)
        {
            switch (optionSelected)
            {
                case 1:
                    Console.Clear();

                    string accountNumber = _validator.ValidateString("Introduce the account identificator:");

                    int pinAccount = _validator.ValidateNumber("Introduce the account pin:"); // I dont make a validation here because the person who will manage it's a admin and my propouse here it's just try the crud...

                    decimal moneyAccount = 0;
                    do
                    {
                        moneyAccount = _validator.ValidateDecimal("Introduce the money of the account: ");

                    } while (moneyAccount <= 0);

                    _bankAccountDto = new DomainBankAccountDto(accountNumber, pinAccount, moneyAccount);

                    Console.WriteLine(_menuManager.CreateAccount(_bankAccountDto));

                    return false;

                case 2:
                    Console.Clear();

                    Console.WriteLine(_menuManager.GetAllAccounts());
                    int accountIdToDelete = _validator.ValidateNumber("Select the ID of the account to delete.");

                    Console.WriteLine(_menuManager.DeleteAccount(accountIdToDelete));

                    return false;

                case 3:

                    return true;


            }

            return true;
        }
    }
}
