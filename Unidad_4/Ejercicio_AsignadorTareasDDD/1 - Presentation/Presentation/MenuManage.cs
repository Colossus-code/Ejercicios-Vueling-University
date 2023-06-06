using Presentation.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class MenuManage : IMenuManage
    {

        private IMenuManageAdmin _menuManageAdmin;
        private IMenuManageTeamManager _menuManageTeamManager;
        private IMenuManageTech _menuManageTech;

        public MenuManage(IMenuManageTeamManager menuManageTeamManager, IMenuManageTech menuManageTech, IMenuManageAdmin menuManageAdmin)
        {
            _menuManageTeamManager = menuManageTeamManager;

            _menuManageTech = menuManageTech;

            _menuManageAdmin = menuManageAdmin;
        }
        public bool manageMenu(string rol, int option, int idSelected)
        {

            switch (rol)
            {
                case "rolAdmin":

                    return _menuManageAdmin.manageMenuAdmin(option);

                case "rolManager":
                    return _menuManageTeamManager.manageMenuTeamManager(option, idSelected);

                case "rolTech":
                    return _menuManageTech.manageMenuTech(option, idSelected);


            }

            return false;
        }


    }
}
