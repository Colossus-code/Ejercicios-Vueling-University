using Ejercicio_AsignadorTareasMulti.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareasMulti._2___Bussines.IServices
{
    public interface IAssignerRepositoryAdmin
    {
        string assingItWorkerToManager(int idWorker, string teamName);
        string assingItWorkerToTeach(int idWorker, string teamName);
        string assingTaskToItWorker(int idWorker, int taskID);
        string unassingItWorker(int idSelected);
        string getItWorkersSeniorList();
        string getItWorkersList();
        string getTeamsList();
        string getTaskList();   
        bool workerHavesTeam(int idWorker, string teamName, out string methodResponse, bool toManager = true);
        bool workerHavesTask(int idWorker, int taskID, out string methodResponse);
        bool workerHavesSomething(int idSelected, out string methodResponse);

    }
}
