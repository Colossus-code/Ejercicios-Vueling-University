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
    internal class PrinterBy : IPrinter
    {
        Finder finderBy = new Finder();
        public string printTask(List<Task> tasks)
        {
            if (tasks.Count == 0)
            {
                return InputClass.ErrorMsg = "Not created tasks yet";
            }

            string teamName = InputClass.inputMessageString("Introduce the name of the team.");

            string toShow = null;
           
            try
            {
                var taskFound = tasks.FirstOrDefault(e => e.worker.Team.Equals(teamName));

                toShow += $"\nTask ID: {taskFound.taskId}. \nTask description: {taskFound.taskDescription}. \nTask technology: {taskFound.technology}. \nTeam name: {taskFound.worker.Team.teamName}. \nTask Status: {taskFound.StatusOfTask}.";

            }
            catch (Exception)
            {
                return InputClass.ErrorMsg = "Team not found";
            }
            return toShow;
        }

        public string printTeams(List<Team> teams)
        {
            if(teams.Count == 0)
            {
                return InputClass.ErrorMsg = "Not created teams yet";
            }
            
            string toShow = null;
            string teamName = InputClass.inputMessageString("Introduce the name of the team");

            try
            {
                var teamKnowledge = teams.FirstOrDefault(e => e.teamName.Equals(teamName));

                toShow += $"\nTeam name: {teamKnowledge.teamName}. \nTeam manager: {(teamKnowledge.managerTeam != null ? teamKnowledge.managerTeam.Name : "")}.\n";

                foreach (var i in teamKnowledge.technician)
                {
                    toShow += $"Technician name: {(i != null ? i.Name : "")} {(i.Surname != null ? i.Surname : "")}.\n";
                }

            }
            catch (Exception)
            {
                return InputClass.ErrorMsg = "Team not found";
            }
            return toShow;
        }
    }
}
