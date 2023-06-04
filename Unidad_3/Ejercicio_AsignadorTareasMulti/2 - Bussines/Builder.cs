using Ejercicio_AsignadorTareasMulti._2___Bussines.Data_Transformation;
using Ejercicio_AsignadorTareasMulti._2___Bussines.IServices;
using Ejercicio_AsignadorTareasMulti._3___Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareasMulti._2___Bussines
{
    public class Builder : IBuilder
    {
        private IRepositoryITWorker _repositoryWorkers;
        private IRepositoryTeam _repositoryTeams;
        private IRepositoryTask _repositoryTasks; 

        public Builder(IRepositoryITWorker repoWorker, IRepositoryTeam team, IRepositoryTask task) 
        {
            _repositoryWorkers = repoWorker;
            _repositoryTeams = team;
            _repositoryTasks = task;
        }

        public bool buildItWorker(ItWorkerDto worker)
        {
            return _repositoryWorkers.setItWorker(worker);

        }

        public bool buildNewTeam(TeamDto team)
        {
            return _repositoryTeams.setTeam(team);
        }

        public bool buildNewTask(TaskDto task)
        {
            return _repositoryTasks.setTask(task);
        }
    }
}
