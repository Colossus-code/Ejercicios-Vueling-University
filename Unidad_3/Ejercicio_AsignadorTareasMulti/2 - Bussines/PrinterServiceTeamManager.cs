using Ejercicio_AsignadorTareasMulti._2___Bussines.IServices;
using Ejercicio_AsignadorTareasMulti._3___Infrastructure.IRepository;
using Ejercicio_AsignadorTareasMulti.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareasMulti._2___Bussines
{
    public class PrinterServiceTeamManager : IPrinterServiceTeamManager
    {
        private IRepositoryTeam _repositoryTeam;
        private IRepositoryTask _repositoryTask;
        private IRepositoryITWorker _repositoryWorker;
        private IPrinterServiceAdmin _printerAdmin; 

        public PrinterServiceTeamManager(IRepositoryTask repoTask, IRepositoryTeam repoTeam, IRepositoryITWorker repoWorker, IPrinterServiceAdmin printerAdmin) 
        {
            _repositoryTask = repoTask;
            _repositoryTeam = repoTeam;
            _repositoryWorker = repoWorker;
            _printerAdmin = printerAdmin;
        }

        public string printerRepositoryITWorkers(int managerId)
        {
            List<int> workersID = workersOfTeam(managerId);

            string printerRepo = "";

            printerRepo += "\n***************************************************************************\n";

            foreach (int workerID in workersID)
            {
                var worker = _repositoryWorker.getWorkerById(workerID);

                printerRepo += $" ID Worker: {worker.ItWorkerId}\n Worker name: {worker.Name} {worker.Surname}\n";
            }

            printerRepo += "\n***************************************************************************\n";

            return printerRepo;
        }

        public string printerRepositoryTask(int managerId)
        {
            List<int> workersID = workersOfTeam(managerId);

            string printerRepo = "";

            printerRepo += "\n***************************************************************************\n";

            foreach (int workerID in workersID)
            {
                var worker = _repositoryWorker.getWorkerById(workerID);

                var task = _repositoryTask.getTasks().FirstOrDefault(e => e.WorkerId == workerID);

                printerRepo += $" ID Task: {task.TaskId}\n Task description: {task.TaskDescription}\n";
            }

            printerRepo += "\n***************************************************************************\n";

            return printerRepo;
        }

        public string printerRepositoryUnassignedTask()
        {
            return _printerAdmin.printerRepositoryUnassignedTask();
        }

        private List<int> workersOfTeam(int idManager)
        {
            Team teamOfManager = _repositoryTeam.getTeamsList().Where(e=> e.ManagerTeamId == idManager).FirstOrDefault();
            return teamOfManager.TechnicianId;
        } 
    }
}
