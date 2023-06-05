using Ejercicio_AsignadorTareasMulti._2___Bussines.IServices;
using Ejercicio_AsignadorTareasMulti._3___Infrastructure.IRepository;
using Ejercicio_AsignadorTareasMulti.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Ejercicio_AsignadorTareasMulti._2___Bussines
{
    public class PrinterRepositoryAdmin : IPrinterRepositoryAdmin
    {
        private IRepositoryTeam _repositoryTeam;
        private IRepositoryTask _repositoryTask;
        private IRepositoryITWorker _repositoryWorker;

        public PrinterRepositoryAdmin(IRepositoryTask repoTask, IRepositoryTeam repoTeam, IRepositoryITWorker repoWorker)
        {
            _repositoryTask = repoTask;
            _repositoryTeam = repoTeam;
            _repositoryWorker = repoWorker;
        }
        public string printerRepositoryITWorkersByTeamNames(string teamName)
        {
            string itWorkersTeamNamesToString = "";

            try
            {
                Team team = _repositoryTeam.findTeamByTeamName(teamName);
                ITWorker managerTeam = _repositoryWorker.getWorkerById(team.ManagerTeamId);

                itWorkersTeamNamesToString += "\n***************************************************************************\n";
                itWorkersTeamNamesToString += $" Team name: {team.TeamName}.\n Team manager: {(managerTeam != null ? managerTeam.Name : " ")} {(managerTeam != null ? managerTeam.Surname : " ")}.\n";
                
                if (team.TechnicianId.Count() > 0)
                {
                    List<ITWorker> workersList = _repositoryWorker.getItWorkerList();

                    foreach(ITWorker worker in workersList.Where(e => e.TeamName == team.TeamName))
                    {
                        itWorkersTeamNamesToString += $" Technician worker: {worker.ItWorkerId}. {worker.Name} {worker.Surname}\n";
                    }

                    itWorkersTeamNamesToString += "\n***************************************************************************\n";
                }
                else
                {
                    return "Not found technicians for this team";
                }
                
                return itWorkersTeamNamesToString;
            }
            
            catch (Exception)
            {
                return "Not found team with that team name.";
            }
        }

        public string printerRepositoryTaskByTeamName(string teamName)
        {
            string itWorkersTeamNamesToString = "";

            try
            {
                Team team = _repositoryTeam.findTeamByTeamName(teamName);
                ITWorker managerTeam = _repositoryWorker.getWorkerById(team.ManagerTeamId);

                itWorkersTeamNamesToString += "\n***************************************************************************\n";
                itWorkersTeamNamesToString += $" Team name: {team.TeamName}.\n Team manager: {(managerTeam != null ? managerTeam.Name : " ")} {(managerTeam != null ? managerTeam.Surname : " ")}.\n";

                if (team.TechnicianId.Count() > 0)
                {
                    List<ITWorker> workersList = _repositoryWorker.getItWorkerList();

                    foreach (ITWorker worker in workersList.Where(e => e.TeamName == team.TeamName))
                    {
                        Task task = _repositoryTask.getTaskById(worker.ItWorkerTaskId);

                        itWorkersTeamNamesToString += $" Technician worker: {worker.Name} {worker.Surname}\n";

                        if(task != null)
                        {
                            itWorkersTeamNamesToString += $" Workers task assigned: {task.TaskDescription}\n";
                        }

                        itWorkersTeamNamesToString += "\n***************************************************************************\n";
                    }
                }
                else
                {
                    return "Not found technicians for this team";
                }

                return itWorkersTeamNamesToString;
            }

            catch (Exception)
            {
                return "Not found team with that team name.";
            }
        }

        public string printerRepositoryTeamNames()
        {
            List<Team> teamList = _repositoryTeam.getTeamsList();

            string teamListToString = "";

            foreach (Team team in teamList)
            {
                teamListToString += $" Team name: {team.TeamName}.\n";

            }

            return teamListToString;
        }

        public string printerRepositoryUnassignedTask()
        {
            List<Task> taskList = _repositoryTask.getTasks();

            string taskListToString = "";

            try
            {
                taskListToString += "\n***************************************************************************\n";
                foreach (Task task in taskList.Where(e => e.Assigned == false))
                {
                    taskListToString += $" ID Task: {task.TaskId}.\n Task description: {task.TaskDescription}.\n Task technology: {task.Technology}\n";
                }
                taskListToString += "\n***************************************************************************\n";
            }
            catch (Exception)
            {
                return "Not found unassigned tasks.";
            }
            return taskListToString;
        }
    }
}
