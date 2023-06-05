using Ejercicio_AsignadorTareasMulti._2___Bussines.IServices;
using Ejercicio_AsignadorTareasMulti._3___Infrastructure.IRepository;
using Ejercicio_AsignadorTareasMulti.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Ejercicio_AsignadorTareasMulti.Entity.Task;

namespace Ejercicio_AsignadorTareasMulti._2___Bussines
{
    public class AssignerRepositoryTeamManager : IAssignerRepositoryTeamManager
    {
        private IRepositoryITWorker _repositoryWorkers;
        private IRepositoryTeam _repositoryTeams;
        private IRepositoryTask _repositoryTasks;


        public AssignerRepositoryTeamManager(IRepositoryITWorker repoWorker, IRepositoryTeam team, IRepositoryTask task)
        {
            _repositoryWorkers = repoWorker;
            _repositoryTeams = team;
            _repositoryTasks = task;
        }

        public string assingItWorkerToTeach(int idWorker, string teamName, int idManager)
        {
            throw new NotImplementedException();
        }

        public string assingTaskToItWorker(int idWorker, int taskID, int idManager)
        {
            try
            {
                Task taskToWorker = _repositoryTasks.getTaskById(taskID);
                ITWorker worker = _repositoryWorkers.getWorkerById(idWorker);

                if(worker.TechKnowledges.Contains(taskToWorker.Technology))
                {
                    taskToWorker.WorkerId = idWorker;
                    worker.ItWorkerTaskId = taskID;

                    taskToWorker.Assigned = true;

                    return "Task updated to the worker.";
                }
                else
                {
                    return "The worker doesn't haves the knowledge required to this task";
                }
                
                //TODO sgarciam 0506 gestionar cambio de task de la que ya tiene por otra y liberar la que tenia 

            }catch(Exception) 
            {

                return "Error assigning task to worker";
            }
            
        }

        public string getItWorkersList(int idManager)
        {
            Team managerTeam = _repositoryTeams.getTeamsList().FirstOrDefault(e => e.ManagerTeamId == idManager);
            List<ITWorker> workers = _repositoryWorkers.getItWorkerList().Where(e => e.TeamName == managerTeam.TeamName).ToList();

            string workersToString = "";

            workersToString += "\n***************************************************************************\n";
            foreach (ITWorker worker in workers)
            {
                workersToString += "____________________________________________________________________________\n";
                workersToString += $" Worker ID: {worker.ItWorkerId}\n Worker name: {worker.Name} {worker.Surname}\n";
            }
            workersToString += "\n***************************************************************************\n";
            return workersToString;
        }

        public string getTaskId()
        {
            List<Task> tasks = _repositoryTasks.getTasks().Where(e => e.Assigned == false).ToList();

            string tasksToString = "";

            tasksToString += "\n***************************************************************************\n";
            foreach (Task task in tasks)
            {
                tasksToString += "____________________________________________________________________________\n";
                tasksToString += $" Task ID: {task.TaskId}\n Task description: {task.TaskDescription}\n";
            }
            tasksToString += "\n***************************************************************************\n";
            return tasksToString;
        }
    }
}
