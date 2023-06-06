using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts
{
    public interface IMenuManage
    {
        bool manageMenu(string rol, int option, int idSelected);

    }
}
