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
    public class AssignerServiceTeamManager : IAssignerServiceTeamManager
    {
        private IRepositoryITWorker _repositoryWorkers;
        private IRepositoryTeam _repositoryTeams;
        private IRepositoryTask _repositoryTasks;


        public AssignerServiceTeamManager(IRepositoryITWorker repoWorker, IRepositoryTeam team, IRepositoryTask task)
        {
            _repositoryWorkers = repoWorker;
            _repositoryTeams = team;
            _repositoryTasks = task;
        }

        public string assingItWorkerToTeach(int idWorker, int idManager)
        {

            try
            {
                ITWorker worker = _repositoryWorkers.getWorkerById(idWorker);

                Team workerTeam = _repositoryTeams.getTeamsList().Where(e => e.ManagerTeamId == idManager).FirstOrDefault();

                if(worker.TeamName != null && worker.TeamName != _repositoryTeams.getTeamsList().FirstOrDefault(e => e.ManagerTeamId == idManager).TeamName) // Se introduce un it worker con un id que no se printea. 
                {
                    return "Unallow operation";

                }
                else if( worker.TeamName != null)
                {

                    return "The worker is already on this team";

                }
                else
                {
                    workerTeam.TechnicianId.Add(idWorker);
                    worker.TeamName = workerTeam.TeamName;

                    return "Worker has been agregated to this team";
                }


            }catch (Exception )
            {
                return "Error assigning new IT Worker to the team";
            }
            
        }

        public string assingTaskToItWorker(int idWorker, int taskID, int idManager)
        {
            try
            {
                Task taskToWorker = _repositoryTasks.getTaskById(taskID);
                ITWorker worker = _repositoryWorkers.getWorkerById(idWorker);

                if (worker.ItWorkerTaskId != taskToWorker.TaskId && _repositoryTasks.getTaskById(taskID).Assigned == false) // Se introduce un id task con un id que no se printea. 
                {
                    return "Unallow operation";
                }else if(worker.ItWorkerTaskId > 0 && _repositoryTasks.getTaskById(taskID).WorkerId == idWorker)
                {
                    return "The task is already assigned to this techician";
                }

                if (worker.TechKnowledges.Contains(taskToWorker.Technology))
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

        public string getITWorkersListWithoutTeam()
        {
            List<ITWorker> workers = _repositoryWorkers.getItWorkerList().Where(e => e.TeamName == null).ToList();

            string workersToString = "";

            workersToString += "\n***************************************************************************\n";
            
            if(workers.Count == 0) 
            {
                return "Not unassigned workers for now";
            
            }
            
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

            if (tasks.Count == 0)
            {
                return "Not unassigned task for now";

            }
            foreach (Task task in tasks)
            {
                tasksToString += "____________________________________________________________________________\n";
                tasksToString += $" Task ID: {task.TaskId}\n Task description: {task.TaskDescription}\n";
            }
            tasksToString += "\n***************************************************************************\n";
            return tasksToString;
        }
        public bool workerHavesTask(int idWorker, out string methodResponse)
        {
            try
            {
                ITWorker worker = _repositoryWorkers.getWorkerById(idWorker);
            
                if(worker.ItWorkerTaskId != 0)
                {

                    methodResponse = "Worker actually haves a task, you want to unassing? (y/n)";
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
    }
}
