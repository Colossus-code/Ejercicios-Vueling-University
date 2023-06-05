using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareasMulti._2___Bussines.IServices
{
    public interface IPrinterServiceTeamManager
    {
        string printerRepositoryITWorkers(int managerId);
        string printerRepositoryUnassignedTask();
        string printerRepositoryTask(int managerId);
    }
}
