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

        public Builder(IRepositoryITWorker repoWorker) 
        {
            _worker = repoWorker;
        }

        public bool buildItWorker(ItWorkerDto worker)
        {
            return _worker.setItWorker(worker);

        }
    }
}
