using Ejercicio_AsignadorTareas.Controller.Interfaces;
using Ejercicio_AsignadorTareas.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareas.Controller
{
    internal class PrinterBy : IPrinter
    {
        public string printTask(List<Entity.Task> tasks)
        {

            string teamName = Console.ReadLine();

            string toShow = null;

            try
            {
                foreach (var e in tasks.Where(e => e.worker.Team.teamName == teamName))
                {
                    toShow += $"\nTask ID: {e.taskId}. \n" +
                        $"Task description: {e.taskDescription}. \n" +
                        $"Task technology: {e.technology}. \n" +
                        $"Team name: {e.worker.Team.teamName}";
                }
            }
            catch (ArgumentNullException)
            {
                return toShow;
            }
            return toShow;
        }

        public string printTeams(List<Team> teams)
        {
            string toShow = null;

            string teamName = Console.ReadLine();

            try
            {
                foreach (var e in teams.Where(e => e.teamName.Equals(teamName)))
                {
                    toShow += $"\nTeam name: {e.teamName}. \n" +
                        $"Team manager: {e.managerTeam}. \n" +
                        $"Team technicians: {e.technician.Select(i => i.Name)}. \n"; // TODO sgarciam 3005 error porque no hay técnicos en el equipo
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
