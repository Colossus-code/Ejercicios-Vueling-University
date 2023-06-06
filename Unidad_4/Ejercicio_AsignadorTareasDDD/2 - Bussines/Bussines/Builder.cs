using Bussines.Data_Transformation;
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
            ITWorker workerBuilt = new ITWorker();
            workerBuilt.Name = worker.WorkerName;
            workerBuilt.Surname = worker.WorkerSurname;
            workerBuilt.TechKnowledges = worker.Knowledge;
            workerBuilt.ItWorkerLevel = worker.TechLevel;
            workerBuilt.BirthDate = worker.WorkerBirthDay;
            workerBuilt.YearsExperiencie = worker.WorkerYearsExperience;
            
            return _repositoryWorkers.setItWorker(workerBuilt);

        }

        public bool buildNewTeam(TeamDto team)
        {
            Team teamBuilt = new Team();
            teamBuilt.TeamName = team.TeamName;
            return _repositoryTeams.setTeam(teamBuilt);
        }

        public bool buildNewTask(TaskDto task)
        {
            Task taskBuilt = new Task();
            taskBuilt.TaskDescription = task.TaskDescription;
            taskBuilt.Technology = task.Technology;
            taskBuilt.StatusOfTask = task.StatusOfTask;

            return _repositoryTasks.setTask(taskBuilt);
        }
    }
}
