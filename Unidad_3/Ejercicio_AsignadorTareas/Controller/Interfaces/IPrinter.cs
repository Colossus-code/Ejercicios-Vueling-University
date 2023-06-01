using Ejercicio_AsignadorTareas.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareas.Controller.Interfaces
{
    internal interface IPrinter
    {
        string printTeams(List<Team> teams);

        string printTask(List<Entity.Task> tasks);
    }
}
