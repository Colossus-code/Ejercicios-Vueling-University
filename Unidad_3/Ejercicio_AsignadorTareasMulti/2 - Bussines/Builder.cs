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
        private IRepositoryITWorker _worker;
        private IRepositoryTeam _team;
        private IRepositoryTask _task; 

        public Builder(IRepositoryITWorker repoWorker, IRepositoryTeam team, IRepositoryTask task) 
        {
            _worker = repoWorker;
            _team = team;
            _task = task;
        }

        public bool buildItWorker(ItWorkerDto worker)
        {
            return _worker.setItWorker(worker);

        }

        public bool buildNewTeam(TeamDto team)
        {
            return _team.setTeam(team);
        }

        public bool buildNewTask(TaskDto task)
        {
            return _task.setTask(task);
        }
    }
}
