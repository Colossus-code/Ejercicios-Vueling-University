
using Ejercicio_AsignadorTareasMulti._1___Presentation.Helpers.InputValidation;
using Ejercicio_AsignadorTareasMulti._2___Bussines.IServices;
using System;

namespace Ejercicio_AsignadorTareasMulti._1___Presentation
{
    public class MainMenu
    {
        private InputValidator _inputValidationInt = new InputValidator();
        private ILogin _login;
        private ILoginMenu _loginMenu;
        private IPrinterMenuOptions _printerMenuOptions;

        public MainMenu(ILogin login, ILoginMenu loginMenu, IPrinterMenuOptions printerMenuOptions)
        {
            _login = login;
            _loginMenu = loginMenu;
            _printerMenuOptions = printerMenuOptions;
        }

        private bool _exitProgram { get; set;}
        
        //Start of the program, it will be runing while the user don't exit
        public void StartProgram()
        {

            do
            {
                int idSelected = _loginMenu.loginMenu();

                string rolSelected = _login.itWorkerRol(idSelected);

                if (rolSelected.Equals(""))
                {
                    continue;
                }
                Console.WriteLine(_printerMenuOptions.menuToPrint(rolSelected));

                int menuOptionSelected = _inputValidationInt.validationIntEntry();
                

            } while (_exitProgram == false);

        }
    }
}
