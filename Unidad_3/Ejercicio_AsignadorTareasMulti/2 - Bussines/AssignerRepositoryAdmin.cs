using Ejercicio_AsignadorTareas.Enum;
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
                switchTeam(idWorker, teamName);
                return "Worker has been updated.";
            }
            catch (Exception)
            {
                return "Error updating the worker.";
            }
        }

        public string assingItWorkerToTeach()
        {
            throw new NotImplementedException();
        }

        public string assingTaskToItWorker()
        {
            throw new NotImplementedException();
        }

        public string getItWorkersList()
        {
            string listItWorkersToString = "";

            List<ITWorker> workers = _repositoryWorkers.getItWorkerList();
            foreach (ITWorker worker in workers.Where(e => e.ItWorkerLevel == ITWorkerLevel.senior))
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

        public bool workerHavesTeam(int idWorker, string teamName, out string response)
        {
            ITWorker worker = _repositoryWorkers.getWorkerById(idWorker);
            Team teamOfWorker = _repositoryTeams.findTeamByTeamName(teamName);

            if (worker.TeamName != null)
            {
                if(worker.TeamName == teamName && teamOfWorker.TechnicianId.Contains(worker.ItWorkerId)) // ES IT WORKER DEL TEAM PASADO
                {
                    response = "The worker actually is a technician of this team, you want to switch to assing manager? (y/n)";
                    return true;
                
                }else if(worker.TeamName == teamName && teamOfWorker.ManagerTeamId == worker.ItWorkerId) // ES MANAGER DEL EQUIPO PASADO 
                {
                    response = "The worker it's already the manager of this team!";
                    return false;
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

        public bool switchTeam(int workerId, string teamName)
        {
            try
            {
                ITWorker worker = _repositoryWorkers.getWorkerById(workerId);
                Team teamOfWorker = _repositoryTeams.findTeamByTeamName(worker.TeamName);
                Team goingTeam = _repositoryTeams.findTeamByTeamName(teamName); 

                if(goingTeam.Equals(teamOfWorker)) {

                    ITWorker actuallyManager = _repositoryWorkers.getWorkerById(teamOfWorker.ManagerTeamId);
                    actuallyManager.TeamName = null;
                    teamOfWorker.TechnicianId.Remove(actuallyManager.ItWorkerId);
                    // ELIMINAMOS EL MANAGER ACTUAL DEL EQUIPO 

                    teamOfWorker.ManagerTeamId = worker.ItWorkerId;
                    // ASIGNAMOS COMO MANAGER AL WORKER SELECCIONADO 
                }
                else
                {
                    ITWorker actuallyManager = _repositoryWorkers.getWorkerById(goingTeam.ManagerTeamId);
                    actuallyManager.TeamName = null;
                    goingTeam.TechnicianId.Remove(actuallyManager.ItWorkerId);
                    // ELIMINAMOS EL MANAGER DEL EQUIPO AL QUE VA

                    goingTeam.ManagerTeamId = workerId;
                    goingTeam.TechnicianId.Add(workerId);
                    // ASIGNAMOS AL EQUIPO Y ASIGNAMOS COMO MANAGER 
                }
                teamOfWorker.TechnicianId.Remove(worker.ItWorkerId);
                teamOfWorker.ManagerTeamId = -1;
                worker.TeamName = null;

                return true; 
            
            }catch (Exception) {

                return false;
            }

        }
    }
}
