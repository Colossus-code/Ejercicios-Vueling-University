using Ejercicio_AsignadorTareasMulti._1___Presentation.Contracts;
using Ejercicio_AsignadorTareasMulti._1___Presentation.Helpers.InputValidation;
using Ejercicio_AsignadorTareasMulti._2___Bussines;
using Ejercicio_AsignadorTareasMulti._2___Bussines.Data_Transformation;
using Ejercicio_AsignadorTareasMulti._2___Bussines.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareasMulti._1___Presentation.MenuManagers
{
    public class MenuManageTeamManager : IMenuManageTeamManager
    {
        private InputValidator _inputValidator = new InputValidator();
        private DataCoherency _dataCoherency = new DataCoherency();

        private IPrinterRepositoryTeamManager _printerRepositoryTeamManager;
        private IAssignerRepositoryTeamManager _assignerRepositoryTeamManager;
        public MenuManageTeamManager(IPrinterRepositoryTeamManager printerRepo, IAssignerRepositoryTeamManager assignerRepo)
        {
            _printerRepositoryTeamManager = printerRepo;
            _assignerRepositoryTeamManager = assignerRepo;
        }
        public bool manageMenuTeamManager(int option, int idManager)
        {
            switch (option)
            {               
                case 1:
                    Console.Clear();
                    Console.WriteLine(_printerRepositoryTeamManager.printerRepositoryITWorkers(idManager));
                    return true;

                case 2:
                    Console.Clear();
                    Console.WriteLine(_printerRepositoryTeamManager.printerRepositoryUnassignedTask());
                    return true;

                case 3:
                    Console.Clear();
                    Console.WriteLine(_printerRepositoryTeamManager.printerRepositoryTask(idManager));
                    return true;             

                case 4:
                    Console.Clear();
                    /*_assignerRepositoryTeamManager.assingItWorkerToTeach(idManager)*/;

                    return true;

                case 5:
                    Console.Clear();
                    assingTaskToWorker(idManager);
                    break;

                case 6:

                    return false;

                default:

                    return false;

            }

            return true;
        }

        private void assingTaskToWorker(int idManager)
        {
            Console.WriteLine(_assignerRepositoryTeamManager.getItWorkersList(idManager));
            int workerId = _inputValidator.validationIntEntry("Introduce the worker ID.");            
            
            Console.WriteLine(_assignerRepositoryTeamManager.getTaskId());
            int taskID = _inputValidator.validationIntEntry("Introduce the task ID.");

            Console.WriteLine(_assignerRepositoryTeamManager.assingTaskToItWorker(workerId, taskID, idManager));

        }
    }
}

