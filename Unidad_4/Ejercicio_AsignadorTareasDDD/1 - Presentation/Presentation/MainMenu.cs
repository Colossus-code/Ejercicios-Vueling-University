using Bussines.IServices;
using Presentation.Contracts;
using Presentation.Helpers.InputValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class MainMenu
    {
        private InputValidator _inputValidationInt = new InputValidator();
        private ILogin _login;
        private ILoginMenu _loginMenu;
        private IPrinterMenuOptions _printerMenuOptions;
        private IMenuManage _menuManage;
        private bool _exitProgram { get; set; }


        public MainMenu(ILogin login, ILoginMenu loginMenu, IPrinterMenuOptions printerMenuOptions, IMenuManage menuManage)
        {
            _login = login;
            _loginMenu = loginMenu;
            _printerMenuOptions = printerMenuOptions;
            _menuManage = menuManage;
        }


        //Start of the program, it will be runing while the user don't exit
        public void StartProgram()
        {

            do
            {
                // First print and input the login option 
                int idSelected = _loginMenu.loginMenu();
                // Comprobe that rol selected by the id of ITWorker selected
                string rolSelected = _login.itWorkerRol(idSelected);

                if (rolSelected.Equals(""))
                {
                    continue;
                }
                bool exit = false;

                do
                {
                    // Prints the message of bussisnes layer by the rol kind
                    int menuOptionSelected = _inputValidationInt.validationIntEntry(_printerMenuOptions.menuToPrint(rolSelected));

                    exit = _menuManage.manageMenu(rolSelected, menuOptionSelected, idSelected);

                } while (exit != false);

            } while (_exitProgram == false);

        }
    }
}
