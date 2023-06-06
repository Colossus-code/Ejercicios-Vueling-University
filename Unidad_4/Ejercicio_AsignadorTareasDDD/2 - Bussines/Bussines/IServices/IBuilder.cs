using Bussines.Data_Transformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.IServices
{
    public interface IBuilder
    {
        bool buildItWorker(ItWorkerDto worker);
        bool buildNewTeam(TeamDto team);
        bool buildNewTask(TaskDto task);
    }
}
