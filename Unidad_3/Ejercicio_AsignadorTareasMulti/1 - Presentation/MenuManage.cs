using Ejercicio_AsignadorTareas.Enum;
using Ejercicio_AsignadorTareasMulti._1___Presentation.Contracts;
using Ejercicio_AsignadorTareasMulti._1___Presentation.Helpers.InputValidation;
using Ejercicio_AsignadorTareasMulti._2___Bussines.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareasMulti._2___Bussines
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
        public void manageMenu(string rol, int option)
        {

            switch (rol)
            {
                case "rolAdmin":
                    _menuManageAdmin.manageMenuAdmin(option);
                    break;

                case "rolManager":
                    manageMenuManager(option);
                    break;

                case "rolTech":
                    manageMenuTech(option);
                    break;

            }
        }
        
        public void manageMenuManager(int option)
        {
           

        }

        public void manageMenuTech(int option)
        {
            

        }

    }
}
