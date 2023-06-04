using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareasMulti._2___Bussines.IServices
{
    public interface IPrinterRepositoryAdmin
    {
        string printerRepositoryTeamNames();
        string printerRepositoryITWorkersByTeamNames(string teamName);
        string printerRepositoryUnassignedTask();
        string printerRepositoryTaskByTeamName(string teamName);

    }
}
