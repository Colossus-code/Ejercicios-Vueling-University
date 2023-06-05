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

        private IPrinterServiceTeamManager _printerRepositoryTeamManager;
        private IAssignerServiceTeamManager _assignerRepositoryTeamManager;

        private const string ERROR_WORKER = "Not unassigned workers for now";
        private const string ERROR_TASK = "Not unassigned task for now";

        public MenuManageTeamManager(IPrinterServiceTeamManager printerRepo, IAssignerServiceTeamManager assignerRepo)
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
                    assingItWorkerToTeam(idManager);
                    return true;

                case 5:;
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
            if(!_assignerRepositoryTeamManager.getItWorkersList(idManager).Equals(ERROR_WORKER) && !_assignerRepositoryTeamManager.getTaskId().Equals(ERROR_TASK))
            {
                Console.WriteLine(_assignerRepositoryTeamManager.getItWorkersList(idManager));
                int workerId = _inputValidator.validationIntEntry("Introduce the worker ID.");

                Console.WriteLine(_assignerRepositoryTeamManager.getTaskId());
                int taskID = _inputValidator.validationIntEntry("Introduce the task ID.");

                string methodResponse;

                if (_assignerRepositoryTeamManager.workerHavesTask(workerId, out methodResponse))
                {
                    string answer;

                    _inputValidator.validationYesOrNoEntry(methodResponse, out answer);

                    if (answer.Equals("y"))
                    {
                        Console.WriteLine(_assignerRepositoryTeamManager.assingTaskToItWorker(workerId, taskID, idManager));
                    }
                    else
                    {
                        Console.WriteLine("The task won't be assinged to the worker");
                    }
                }
            }
            else
            {
                Console.WriteLine(ERROR_TASK);
            }

        }

        private void assingItWorkerToTeam(int idManager)
        {
            if (!_assignerRepositoryTeamManager.getITWorkersListWithoutTeam().Equals(ERROR_WORKER))
            {
                Console.WriteLine(_assignerRepositoryTeamManager.getITWorkersListWithoutTeam());

                int workerId = _inputValidator.validationIntEntry("Introduce the worker ID.");

                Console.WriteLine(_assignerRepositoryTeamManager.assingItWorkerToTeach(workerId, idManager));
            }
            else
            {
                Console.WriteLine(ERROR_WORKER);
            }

        }
    }
}

