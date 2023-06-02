using Ejercicio_AsignadorTareas.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Ejercicio_AsignadorTareas.Entity.Task;

namespace Ejercicio_AsignadorTareas.Controller.Interfaces
{
    internal interface IAssigner
    {
        bool assignManagerForATeam(List<Team> teams, List<ITWorker> workers);
        bool assignWorkerForATeam(List<Team> teams, List<ITWorker> workers);
        bool assignTaskToItWorker(List<ITWorker> workers, List<Task> tasks);
    }
}
