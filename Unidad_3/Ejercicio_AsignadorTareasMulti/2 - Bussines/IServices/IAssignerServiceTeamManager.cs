using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareasMulti._2___Bussines.IServices
{
    public interface IAssignerServiceTeamManager
    {
        string assingItWorkerToTeach(int idWorker, int idManager);
        string assingTaskToItWorker(int idWorker, int taskID, int idManager);
        string getItWorkersList(int idManager);
        string getITWorkersListWithoutTeam();
        string getTaskId();
        bool workerHavesTask(int idWorker, out string methodResponse);
    }
}
