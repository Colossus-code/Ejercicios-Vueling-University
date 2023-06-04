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
        string assingItWorkerToTeach(int idWorker, string teamName);
        string assingTaskToItWorker();
        string getItWorkersSeniorList();
        string getItWorkersList();
        string getTeamsList();
        bool workerHavesTeam(int idWorker, string teamName, out string methodResponse, bool toManager = true);
        bool switchTeamToManager(int workerID, string teamName);
        bool switchTeamTech(int workerId, string teamName);

    }
}
