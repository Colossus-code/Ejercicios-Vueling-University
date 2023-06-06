using Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepository
{
    public interface IRepositoryITWorker
    {
        List<ITWorker> getItWorkerList();
        bool setListItWorker(ITWorker newWorker);
        ITWorker getWorkerById(int idWorker);
        bool setItWorker(ITWorker newWorker);
        List<ITWorker> getWorkersByTeamName(string teamName);


    }
}
