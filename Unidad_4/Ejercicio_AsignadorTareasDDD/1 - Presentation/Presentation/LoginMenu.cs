using Presentation.Contracts;
using Presentation.Helpers.InputValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class LoginMenu : ILoginMenu
    {
        private InputValidator _validateInt = new InputValidator();
        private readonly string loginMsg = "****************************************************************\n Please introduce the ID of Worker for login\n****************************************************************";
        public int loginMenu()
        {
            Console.Clear();
            // First creating a default value witch isn't assigned to any tech
            int introducedIDLogin = -1;

            // If the introduced ones isn't a number the program is going to ask again
            do
            {
                introducedIDLogin = _validateInt.validationIntEntry(loginMsg);

            } while (introducedIDLogin == -1);

            return introducedIDLogin;
        }
    }
}
