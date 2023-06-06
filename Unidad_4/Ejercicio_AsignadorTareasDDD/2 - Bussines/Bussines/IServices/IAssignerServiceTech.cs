using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.IServices
{
    public interface IAssignerServiceTech
    {
        string assingTaskToWorker(int idWorker, int idTask);
        bool workerHavesTask(int idWorker, out string methodResponse);
        void unassingTask(int idWorker);

    }
}
