using Bussines.IServices;
using Infrastructure.Entity;
using Infrastructure.Enum;
using Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Bussines
{
    public class AssignerServiceTech : IAssignerServiceTech
    {
        private IRepositoryTask _repoTask;
        private IRepositoryITWorker _repoWorker;

        public AssignerServiceTech(IRepositoryTask repoTask, IRepositoryITWorker repoWorkers)
        {
            _repoTask = repoTask;
            _repoWorker = repoWorkers;
        }

        public string assingTaskToWorker(int idWorker, int idTask)
        {
            Task taskToWorker = _repoTask.getTaskById(idTask);
            ITWorker worker = _repoWorker.getWorkerById(idWorker);

            if (worker.ItWorkerTaskId != taskToWorker.TaskId && !_repoTask.getTaskById(idTask).Assigned == false) // Se introduce un id task con un id que no se printea. 
            {
                return "Unallow operation";
            }
            else if (worker.ItWorkerTaskId > 0 && _repoTask.getTaskById(idTask).WorkerId == idWorker)
            {
                return "The task is already assigned to this techician";
            }

            taskToWorker.Assigned = true;
            taskToWorker.WorkerId = idWorker;
            worker.ItWorkerTaskId = idTask;

            return "Task has been asigned to you! ";
        }
        public bool workerHavesTask(int idWorker, out string methodResponse)
        {
            try
            {
                ITWorker worker = _repoWorker.getWorkerById(idWorker);

                if (worker.ItWorkerTaskId != 0)
                {

                    methodResponse = "You actually haves a task, do you want to unassing, your senior won't be happy? (y/n)";
                    return true;
                }

                methodResponse = ""; // El worker no tiene task, no hay que pedir verificacion.
                return true;
            }
            catch (Exception)
            {
                methodResponse = "Worker has not found."; // El worker no tiene task, no hay que pedir verificacion.
                return false;

            }
        }
        public void unassingTask(int idWorker)
        {
            ITWorker worker = _repoWorker.getWorkerById(idWorker);
            Task taskToWorker = _repoTask.getTaskById(worker.ItWorkerTaskId);
            worker.ItWorkerTaskId = -98;
            taskToWorker.WorkerId = -99;
            taskToWorker.StatusOfTask = TaskStatus.todo;
            taskToWorker.Assigned = false;

        }

    }
}
