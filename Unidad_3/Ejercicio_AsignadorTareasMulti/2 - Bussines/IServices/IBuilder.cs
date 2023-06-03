using Ejercicio_AsignadorTareasMulti._2___Bussines.Data_Transformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareasMulti._2___Bussines.IServices
{
    public interface IBuilder
    {
        bool buildItWorker(ItWorkerDto worker);
    }
}
