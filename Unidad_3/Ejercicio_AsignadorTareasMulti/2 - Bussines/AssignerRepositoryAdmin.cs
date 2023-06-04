using Ejercicio_AsignadorTareas.Enum;
using Ejercicio_AsignadorTareasMulti._2___Bussines.IServices;
using Ejercicio_AsignadorTareasMulti._3___Infrastructure.IRepository;
using Ejercicio_AsignadorTareasMulti.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareasMulti._2___Bussines
{
    public class AssignerRepositoryAdmin : IAssignerRepositoryAdmin
    {
        private IRepositoryITWorker _repositoryWorkers;
        private IRepositoryTeam _repositoryTeams;
        private IRepositoryTask _repositoryTasks;

        public AssignerRepositoryAdmin(IRepositoryITWorker repoWorker, IRepositoryTeam team, IRepositoryTask task)
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

        public string assingTaskToItWorker()
        {
            throw new NotImplementedException();
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
            string listItWorkersToString = "";

            List<ITWorker> workers = _repositoryWorkers.getItWorkerList();
            
            foreach (ITWorker worker in workers.Where(e => e.ItWorkerId > 0))
            {
                listItWorkersToString += $" \n Worker ID: {worker.ItWorkerId}.\n Worker name: {worker.Name}";
            }

            return listItWorkersToString;
        }

        public string getTeamsList()
        {
            string listTeamsToString = "";

            List<Team> teams = _repositoryTeams.getTeamsList();
            foreach (Team team in teams)
            {
                listTeamsToString += $" Team name: {team.TeamName}.\n";
            }

            return listTeamsToString;
        }

        public bool workerHavesTeam(int idWorker, string teamName, out string response, bool toManager)
        {
            ITWorker worker = _repositoryWorkers.getWorkerById(idWorker);
            Team teamOfWorker = _repositoryTeams.findTeamByTeamName(teamName);

            if (worker.TeamName != null)
            {
                if (worker.TeamName == teamName && teamOfWorker.TechnicianId.Contains(worker.ItWorkerId) && toManager) // ES IT WORKER DEL TEAM PASADO
                {
                    response = "The worker actually is a technician of this team, you want to switch? (y/n)";
                    return true;

                }
                else if (worker.TeamName == teamName && teamOfWorker.ManagerTeamId == worker.ItWorkerId) // ES MANAGER DEL EQUIPO PASADO 
                {
                    response = "The worker it's already the manager of this team!";
                    return false;
                }
                else if (worker.TeamName == teamName && teamOfWorker.TechnicianId.Contains(worker.ItWorkerId) && !toManager) 
                {
                    response = "The worker it's already the on this team!";
                    return false;
                }                
                else if (worker.TeamName != null && !teamOfWorker.TechnicianId.Contains(worker.ItWorkerId) && !toManager) 
                {
                    response = "The worker actually haves a team, you want to switch? (y/n)";
                    return true;
                }
                else
                {
                    response = "The worker actually haves a team, you want to switch to assing manager? (y/n)";
                    return true;
                }

            }

            response = ""; // ESTO QUIERE DECIR QUE NO ESTA ASIGNADO A NINGUN EQUIPO 
            return false;
        }

        public bool switchTeamToManager(int workerId, string teamName)
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

        public bool switchTeamTech(int workerId, string teamName)
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
                    if (teamOfWorker.ManagerTeamId == workerId)
                    {
                        teamOfWorker.ManagerTeamId = -1;
                    }

                    teamOfWorker.TechnicianId.Remove(workerId);

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
    }
}
