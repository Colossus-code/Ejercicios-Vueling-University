using Bussines.IServices;
using Presentation.Contracts;
using Presentation.Helpers.InputValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.MenuManagers
{
    public class MenuManageTech : IMenuManageTech
    {
        private IPrinterServiceTech _printerTech;
        private IAssignerServiceTech _assigerTech;
        private IPrinterServiceTeamManager _printerTeamManager;

        private InputValidator _inputValidator = new InputValidator();

        private const string ERROR_WORKER = "Worker has not found.";

        public MenuManageTech(IPrinterServiceTech printerTech, IAssignerServiceTech assignerTech, IPrinterServiceTeamManager printerManager)
        {
            _printerTech = printerTech;
            _assigerTech = assignerTech;
            _printerTeamManager = printerManager;
        }

        public bool manageMenuTech(int option, int idWorker)
        {
            switch (option)
            {
                case 1:

                    Console.Clear();
                    Console.WriteLine(_printerTech.printUnassingTasks());
                    return true;

                case 2:
                    Console.Clear();
                    Console.WriteLine(_printerTech.printTeamTask(idWorker));
                    return true;

                case 3:
                    Console.Clear();
                    assingTask(idWorker);
                    return true;

                case 4:
                    Console.Clear();
                    return false;

                default:

                    return false;

            }

        }

        private void assingTask(int idWorker)
        {
            string methodResponse = "";
            bool havesTask = _assigerTech.workerHavesTask(idWorker, out methodResponse);

            if (havesTask && !methodResponse.Equals(ERROR_WORKER) && methodResponse != "")
            {
                Console.WriteLine(_printerTeamManager.printerRepositoryUnassignedTask());
                int taskID = _inputValidator.validationIntEntry("Introduce the task ID.");

                string answer;

                _inputValidator.validationYesOrNoEntry(methodResponse, out answer);

                if (answer.Equals("y"))
                {
                    _assigerTech.unassingTask(idWorker);
                    Console.WriteLine(_assigerTech.assingTaskToWorker(idWorker, taskID));
                }
                else
                {
                    Console.WriteLine("The task won't be assinged to the worker");
                }
            }
            else if (havesTask && methodResponse.Equals(""))
            {
                Console.WriteLine(_printerTeamManager.printerRepositoryUnassignedTask());
                int taskID = _inputValidator.validationIntEntry("Introduce the task ID.");
                Console.WriteLine(_assigerTech.assingTaskToWorker(idWorker, taskID));
            }
            else
            {
                Console.WriteLine(ERROR_WORKER);
            }
        }
    }
}
