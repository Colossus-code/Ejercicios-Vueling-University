using Ejercicio_AsignadorTareasMulti.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareasMulti._2___Bussines.IServices
{
    public interface IAssignerRepositoryAdmin
    {
        string assingItWorkerToManager(int idWorker, string teamName);
        string assingItWorkerToTeach();
        string assingTaskToItWorker();
        string getItWorkersList();
        string getTeamsList();
        bool workerHavesTeam(int idWorker, string teamName, out string methodResponse);
        bool switchTeam(int workerID, string teamName);

    }
}
