using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts
{
    public interface IMenu
    {
        void StartProgram();

        bool ManageOptionSelected(string loginResponse, string accountNumber);

    }
}
