using Ejercicio_AsignadorTareas.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Ejercicio_AsignadorTareas.Entity.Task;

namespace Ejercicio_AsignadorTareas.Controller.Interfaces
{
    internal interface IFinder
    {
        Team findTeamByName(List<Team> teams);
        ITWorker findWorker(List<ITWorker> workers);
        Task findTask(ITWorker worker, List<Task> taskList);


    }
}
