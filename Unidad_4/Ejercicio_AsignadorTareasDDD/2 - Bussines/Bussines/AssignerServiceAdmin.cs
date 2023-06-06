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
    public class AssignerServiceAdmin : IAssignerServiceAdmin
    {
        private IRepositoryITWorker _repositoryWorkers;
        private IRepositoryTeam _repositoryTeams;
        private IRepositoryTask _repositoryTasks;

        public AssignerServiceAdmin(IRepositoryITWorker repoWorker, IRepositoryTeam team, IRepositoryTask task)
        {
            _repositoryWorkers = repoWorker;
            _repositoryTeams = team;
            _repositoryTasks = task;
        }
        public string assingItWorkerToManager(int idWorker, string teamName)
        {
            try
            {
                switchTeamToManager(idWorker, teamName);
                return "Worker has been updated.";
            }
            catch (Exception)
            {
                return "Error updating the worker.";
            }
        }
        public string assingItWorkerToTeach(int idWorker, string teamName)
        {
            try
            {
                switchTeamTech(idWorker, teamName);
                return "Worker has been updated.";
            }
            catch (Exception)
            {
                return "Error updating the worker.";
            }
        }
        public string assingTaskToItWorker(int idWorker, int taskID)
        {
            if (switchTaskTech(idWorker, taskID))
            {
                return "Task assigned.";
            }
            else
            {
                return "Error assigning the task";
            }
        }
        public string unassingItWorker(int idSelected)
        {
            if (deleteWorker(idSelected))
            {
                return "Worker has been deleted";
            }
            else
            {
                return "Error deleting this worker";
            }
        }
        public bool workerHavesTeam(int idWorker, string teamName, out string methodResponse, bool toManager)
        {
            ITWorker worker = _repositoryWorkers.getWorkerById(idWorker);
            Team teamOfWorker = _repositoryTeams.findTeamByTeamName(teamName);

            if (worker.TeamName != null)
            {
                if (worker.TeamName == teamName && teamOfWorker.TechnicianId.Contains(worker.ItWorkerId) && toManager) // ES IT WORKER DEL TEAM PASADO
                {
                    methodResponse = "The worker actually is a technician of this team, you want to switch? (y/n)";
                    return true;

                }
                else if (worker.TeamName == teamName && teamOfWorker.ManagerTeamId == worker.ItWorkerId) // ES MANAGER DEL EQUIPO PASADO 
                {
                    methodResponse = "The worker it's already the manager of this team!";
                    return false;
                }
                else if (worker.TeamName == teamName && teamOfWorker.TechnicianId.Contains(worker.ItWorkerId) && !toManager)
                {
                    methodResponse = "The worker it's already the on this team!";
                    return false;
                }
                else if (worker.TeamName != null && !teamOfWorker.TechnicianId.Contains(worker.ItWorkerId) && !toManager)
                {
                    methodResponse = "The worker actually haves a team, you want to switch? (y/n)";
                    return true;
                }
                else
                {
                    methodResponse = "The worker actually haves a team, you want to switch to assing manager? (y/n)";
                    return true;
                }

            }

            methodResponse = ""; // ESTO QUIERE DECIR QUE NO ESTA ASIGNADO A NINGUN EQUIPO 
            return false;
        }
        public bool workerHavesTask(int idWorker, int taskID, out string methodResponse)
        {
            ITWorker worker = _repositoryWorkers.getWorkerById(idWorker);
            Task taskOfWorker = _repositoryTasks.getTaskById(taskID);

            if (worker.ItWorkerTaskId > 0)
            {
                if (taskOfWorker.TaskId == worker.ItWorkerTaskId)
                {
                    methodResponse = "The worker already haves assigned that task";
                    return false;
                }
                else
                {
                    methodResponse = "The worker actualy haves a task, you want to swap? (y/n)";
                    return true;
                }

            }

            if (taskOfWorker.WorkerId > 0)
            {
                methodResponse = "The task is actualy assigned to other thech, you want to swap? (y/n)";
                return true;
            }

            methodResponse = ""; // ESTO QUIERE DECIR QUE NO ESTA ASIGNADO A NINGUN EQUIPO 
            return false;
        }
        private bool switchTeamToManager(int workerId, string teamName)
        {
            try
            {
                ITWorker worker = _repositoryWorkers.getWorkerById(workerId);
                Team teamOfWorker = _repositoryTeams.findTeamByTeamName(worker.TeamName);
                Team goingTeam = _repositoryTeams.findTeamByTeamName(teamName);

                if (goingTeam.Equals(teamOfWorker))
                {

                    if (teamOfWorker.ManagerTeamId > 0)
                    {
                        ITWorker actuallyManager = _repositoryWorkers.getWorkerById(teamOfWorker.ManagerTeamId);
                        actuallyManager.TeamName = null;
                        teamOfWorker.TechnicianId.Remove(actuallyManager.ItWorkerId);
                        // ELIMINAMOS EL MANAGER ACTUAL DEL EQUIPO 
                    }

                    teamOfWorker.ManagerTeamId = worker.ItWorkerId;
                    // ASIGNAMOS COMO MANAGER AL WORKER SELECCIONADO EN EL MISMO EQUIPO
                    return true;
                }
                else
                {
                    if (teamOfWorker != null)
                    {
                        // COMPROBAMOS SI ES MANAGER
                        if (teamOfWorker.ManagerTeamId == worker.ItWorkerId)
                        {
                            teamOfWorker.ManagerTeamId = -1;
                            //ELIMINAMOS
                        }

                        teamOfWorker.TechnicianId.Remove(worker.ItWorkerId);
                    }
                    // COMPROBAMOS QUE AL EQUIPO QUE VAYA NO TENGA MANAGER
                    if (goingTeam.ManagerTeamId <= 0)
                    {
                        goingTeam.ManagerTeamId = workerId;
                        goingTeam.TechnicianId.Add(workerId);

                        worker.TeamName = teamName;


                        // ASIGNAMOS DIRECTAMENTE
                    }
                    else
                    {
                        ITWorker actuallyManager = _repositoryWorkers.getWorkerById(goingTeam.ManagerTeamId);
                        actuallyManager.TeamName = null;
                        goingTeam.TechnicianId.Remove(actuallyManager.ItWorkerId);

                        //ELIMINAMOS AL TRABAJADOR DEL EQUIPO EN EL QUE ESTABA

                    }

                    // ASIGNAMOS AL EQUIPO Y ASIGNAMOS COMO MANAGER 

                    goingTeam.ManagerTeamId = workerId;
                    goingTeam.TechnicianId.Add(workerId);
                    worker.TeamName = teamName;

                    return true;

                }


            }
            catch (Exception)
            {

                return false;
            }

        }
        private bool switchTeamTech(int workerId, string teamName)
        {
            try
            {
                ITWorker worker = _repositoryWorkers.getWorkerById(workerId);
                Team teamOfWorker = _repositoryTeams.findTeamByTeamName(worker.TeamName);
                Team goingTeam = _repositoryTeams.findTeamByTeamName(teamName);

                if (goingTeam.Equals(teamOfWorker))
                {
                    return false;
                }
                else
                {
                    if (worker.TeamName != null)
                    {
                        if (teamOfWorker.ManagerTeamId == workerId)
                        {
                            teamOfWorker.ManagerTeamId = -1;
                        }

                        teamOfWorker.TechnicianId.Remove(workerId);
                    }

                    worker.TeamName = goingTeam.TeamName;

                    goingTeam.TechnicianId.Add(workerId);

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool switchTaskTech(int workerId, int taskID)
        {
            try
            {
                ITWorker worker = _repositoryWorkers.getWorkerById(workerId);
                Task seachTask = _repositoryTasks.getTaskById(taskID);

                if (!worker.TechKnowledges.Contains(seachTask.Technology))
                {
                    return false;
                }
                if (worker.ItWorkerTaskId > 0)
                {
                    Task taskToUnassing = _repositoryTasks.getTaskById(worker.ItWorkerTaskId);

                    taskToUnassing.StatusOfTask = TaskStatus.todo;
                    taskToUnassing.Assigned = false;
                    taskToUnassing.WorkerId = -98;
                }
                if (seachTask.WorkerId > 0)
                {
                    ITWorker workerToRemoveTask = _repositoryWorkers.getItWorkerList().FirstOrDefault(e => e.ItWorkerTaskId == seachTask.WorkerId);

                    workerToRemoveTask.ItWorkerTaskId = -99;
                }


                worker.ItWorkerTaskId = seachTask.TaskId;
                seachTask.WorkerId = worker.ItWorkerId;
                seachTask.Assigned = true;

                return true;

            }
            catch (Exception)
            {
                return false;

            }
        }
        private bool deleteWorker(int idSelected)
        {
            try
            {
                ITWorker worker = _repositoryWorkers.getWorkerById(idSelected);

                if (worker.ItWorkerTaskId > 0)
                {
                    Task taskToUnassing = _repositoryTasks.getTaskById(worker.ItWorkerTaskId);

                    taskToUnassing.StatusOfTask = TaskStatus.todo;
                    taskToUnassing.Assigned = false;
                    taskToUnassing.WorkerId = -98;

                    worker.ItWorkerTaskId = -99;
                }
                if (worker.TeamName != null)
                {
                    Team teamToUnassing = _repositoryTeams.getTeamsList().FirstOrDefault(e => e.TechnicianId.Contains(worker.ItWorkerId));
                    teamToUnassing.TechnicianId.Remove(worker.ItWorkerId);

                    worker.TeamName = null;
                }

                worker.ItWorkerId = -100;

                return true;

            }
            catch (Exception)
            {
                return false;

            }
        }
        public bool workerHavesSomething(int idSelected, out string methodResponse)
        {
            try
            {
                ITWorker worker = _repositoryWorkers.getWorkerById(idSelected);

                if (worker.ItWorkerTaskId > 0)
                {
                    methodResponse = "The worker actually haves a task, are you sure about to delete him? (y/n)";
                    return true;
                }
                if (worker.TeamName != "")
                {
                    methodResponse = "The worker actually haves a team, are you sure about to delete him? (y/n)";
                    return true;
                }

                if (worker.TeamName != "" && worker.ItWorkerTaskId > 0)
                {
                    methodResponse = "The worker haves a team and task to doing, are you sure to delete him? (y/n)";
                    return true;
                }
                else
                {

                    methodResponse = "Are you sure about to delete this worker? (y/n)";
                    return true;
                }
            }
            catch (Exception)
            {
                methodResponse = "";
                return false;

            }
        }
        public string getItWorkersSeniorList()
        {
            string listItWorkersToString = "";

            List<ITWorker> workers = _repositoryWorkers.getItWorkerList();
            foreach (ITWorker worker in workers.Where(e => e.ItWorkerLevel == ITWorkerLevel.senior))
            {
                listItWorkersToString += $" \n Worker ID: {worker.ItWorkerId}.\n Worker name: {worker.Name}";
            }

            return listItWorkersToString;
        }
        public string getItWorkersList()
        {
            string listItWorkersToString = "\n***************************************************************************";

            List<ITWorker> workers = _repositoryWorkers.getItWorkerList();

            foreach (ITWorker worker in workers.Where(e => e.ItWorkerId > 0))
            {
                listItWorkersToString += "\n_________________________________________________________________________\n";
                listItWorkersToString += $" \n Worker ID: {worker.ItWorkerId}.\n Worker name: {worker.Name}";
            }
            listItWorkersToString += "\n***************************************************************************\n";
            return listItWorkersToString;
        }
        public string getTeamsList()
        {
            string listTeamsToString = "\n***************************************************************************\n";

            List<Team> teams = _repositoryTeams.getTeamsList();
            foreach (Team team in teams)
            {
                listTeamsToString += "\n_________________________________________________________________________\n";
                listTeamsToString += $" Team name: {team.TeamName}.\n";
            }
            listTeamsToString += "\n***************************************************************************\n";
            return listTeamsToString;
        }
        public string getTaskList()
        {
            string listTaskToString = "\n***************************************************************************\n";

            List<Task> tasks = _repositoryTasks.getTasks();
            foreach (Task task in tasks)
            {
                listTaskToString += "\n_________________________________________________________________________\n";
                listTaskToString += $" Task ID: {task.TaskId} \n Team description: {task.TaskDescription}.\n";
            }
            listTaskToString += "\n***************************************************************************\n";
            return listTaskToString;
        }
    }
}
