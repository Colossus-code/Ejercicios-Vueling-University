using Bussines.IService;
using Presentation.Contracts;
using Presentation.Helpers.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class Menu : IMenu
    {
        private readonly ILoginVerification _loginVerification;

        private readonly IMenuManager _menuManager;

        private readonly InputValidator _validator = new InputValidator();

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
                else
                {

                    exit = ManageOptionSelected(loginResponse, accountNumber);

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

            } while (selectedChoice <= 0 && selectedChoice > 7);

            return SelectedOption(selectedChoice, acountNumber);

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

                    return true;

                case 2:

                    return true;

                case 3:
                    return true;

                case 4:
                    decimal amount = 0; 
                    
                    do
                    {
                        amount = _validator.ValidateNumber("Introduce the amount of money to income.");

                    } while (amount <= 0);

                    Console.WriteLine(_menuManager.GenerateInput(accountNumber, amount));

                    return true;

                case 5:
                        
                        
                    return true;

                case 6:

                    return true;

                case 7:

                    return false;


            }

            return true;
        }
    }
}
