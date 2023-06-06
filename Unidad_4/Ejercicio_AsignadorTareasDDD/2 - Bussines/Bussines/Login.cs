using Bussines.IServices;
using Infrastructure.Entity;
using Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines
{
    public class Login : ILogin
    {
        private IRepositoryITWorker _repoITWorker;
        private IRepositoryTeam _repoTeam;
        public Login(IRepositoryITWorker repositoryWorker, IRepositoryTeam repositoryTeam)
        {
            _repoITWorker = repositoryWorker;
            _repoTeam = repositoryTeam;
        }
        public string itWorkerRol(int id)
        {
            try
            {
                ITWorker worker = _repoITWorker.getWorkerById(id);

                if (worker.ItWorkerId == 0) return "rolAdmin";

                Team team = _repoTeam.findTeamByTeamName(worker.TeamName);

                if (team.ManagerTeamId == worker.ItWorkerId)
                {
                    return "rolManager";
                }
                else
                {
                    return "rolTech";
                }

            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
