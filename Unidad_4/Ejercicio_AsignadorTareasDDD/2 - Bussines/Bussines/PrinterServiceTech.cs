using Bussines.IServices;
using Infrastructure.Entity;
using Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Bussines
{
    public class PrinterServiceTech : IPrinterServiceTech
    {
        private IRepositoryTask _repoTask;
        private IRepositoryITWorker _repoWorker;
        private IRepositoryTeam _repoTeam;

        private IPrinterServiceAdmin _printerServiceAdmin;

        public PrinterServiceTech(IRepositoryITWorker repoWorker, IRepositoryTask repoTask, IRepositoryTeam repoTeam, IPrinterServiceAdmin printerServiceAdmin)
        {
            _repoWorker = repoWorker;
            _repoTask = repoTask;
            _repoTeam = repoTeam;
            _printerServiceAdmin = printerServiceAdmin;
        }

        public string printTeamTask(int workerID)
        {
            string listOfTaskByTeamString = "";

            Team teamOfWorker = _repoTeam.getTeamsList().FirstOrDefault(e => e.TechnicianId.Contains(workerID));
            List<ITWorker> workersOfTeam = new List<ITWorker>();

            foreach (int workerId in teamOfWorker.TechnicianId)
            {
                workersOfTeam.Add(_repoWorker.getWorkerById(workerId));
            }

            listOfTaskByTeamString = "\n***************************************************************************\n";

            try
            {
                foreach (ITWorker worker in workersOfTeam.Where(e => e.ItWorkerTaskId > 0))
                {
                    Task taskOfWorker = _repoTask.getTaskById(worker.ItWorkerTaskId);

                    listOfTaskByTeamString += "\n__________________________________________________________________________________\n";
                    listOfTaskByTeamString += $" Task ID: {taskOfWorker.TaskId}.\n Task Description: {taskOfWorker.TaskDescription}.\n Worker Assigned: {worker.Name} {worker.Surname}\n";

                }
            }
            catch (Exception)
            {

                return "Not found task for the team";

            }

            return listOfTaskByTeamString += "\n***************************************************************************\n";

        }

        public string printUnassingTasks()
        {
            return _printerServiceAdmin.printerRepositoryUnassignedTask();
        }
    }
}
