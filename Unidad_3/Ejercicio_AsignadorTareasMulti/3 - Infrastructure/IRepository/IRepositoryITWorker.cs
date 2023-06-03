using Ejercicio_AsignadorTareasMulti.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareasMulti._3___Infrastructure.IRepository
{
    public interface IRepositoryITWorker
    {
        List<ITWorker> getItWorkerList();
        bool setListItWorker(ITWorker newWorker);
        ITWorker getWorkerById(int idWorker);
        
    }
}
