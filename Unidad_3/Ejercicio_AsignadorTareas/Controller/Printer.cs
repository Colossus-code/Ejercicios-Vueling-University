using Ejercicio_AsignadorTareas.Controller.Interfaces;
using Ejercicio_AsignadorTareas.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Ejercicio_AsignadorTareas.Entity.Task;

namespace Ejercicio_AsignadorTareas.Controller
{
    internal class Printer : IPrinter
    {

        public string printTeams(List<Team> teams)
        {
            string toShow = null;

            try
            {
                foreach (var e in teams)
                {
                    toShow += $"\nTeam name: {e.teamName}. \n";
                }
            }
            catch (ArgumentNullException)
            {
                return toShow;
            }
            return toShow;
        }

        public string printTask(List<Task> tasks)
        {
            string toShow = null;

            try
            {
                foreach (var e in tasks.Where(e => e.assigned == false))
                {
                    toShow += $"\nTask ID: {e.taskId}. \n" +
                        $"Task description: {e.taskDescription}. \n" +
                        $"Task technology: {e.technology}. \n" +
                        $"Task team: {(e.worker.Team != null ? e.worker.Team.teamName : "")}";
                }
            }
            catch (ArgumentNullException)
            {
                return toShow;
            }
            return toShow;
        }
    }
}
