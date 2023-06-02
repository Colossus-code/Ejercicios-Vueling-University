using Ejercicio_AsignadorTareas.Controller.Interfaces;
using Ejercicio_AsignadorTareas.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using Task = Ejercicio_AsignadorTareas.Entity.Task;

namespace Ejercicio_AsignadorTareas.Controller
{
    public class Finder : IFinder
    {
        public Team findTeamByName(List<Team> teams)
        {

            Console.WriteLine("\n****************************************************************");

            foreach (var team in teams)
            {
                Console.WriteLine($"Team Name: {team.teamName}\n");
            }

            Console.WriteLine("****************************************************************\n");

            string teamName = InputClass.inputMessageString("Select the team name please.");

            Team teamVaue = null;
            try
            {
                teamVaue = teams.FirstOrDefault(e => e.teamName.Equals(teamName));

                if (teamVaue == null)
                {
                    InputClass.ErrorMsg = "There isn't a team with that name";
                    return null;
                }
            }
            catch (ArgumentNullException)
            {
                return null;
            }

            return teamVaue;
        }
        public ITWorker findWorker(List<ITWorker> workers)
        {
            Console.WriteLine("\n****************************************************************");
            foreach (ITWorker wrk in workers)
            {
                Console.WriteLine($"Worker ID: {wrk.itWorkerID}\n" +
                    $"Worker name: {wrk.Name}\n");
            }
            Console.WriteLine("****************************************************************\n");

            int employerId = InputClass.inputMessageInt("Select the ID of the Worker please.");

            ITWorker worker = null;

            try
            {
                worker = workers.FirstOrDefault(e => e.itWorkerID == employerId);

                if (worker == null)
                {
                    InputClass.ErrorMsg = "There isn't a worker with that ID";
                    return null;
                }
            }
            catch (ArgumentNullException)
            {
                return null;
            }

            return worker;
        }
        public Task findTask(ITWorker worker, List<Task> taskList)
        {

            Console.WriteLine("\n****************************************************************");
            foreach (Task tsk in taskList.Where(e => e.assigned == false && e.StatusOfTask == Enum.TaskStatus.todo))
            {
                Console.WriteLine($"Task ID: {tsk.taskId}\n" +
                    $"Task description: {tsk.taskDescription}\n");
            }
            Console.WriteLine("****************************************************************\n");

            int taskID = InputClass.inputMessageInt("Select the ID of the Task please.");

            try
            {
                var task = taskList.FirstOrDefault(e => e.taskId == taskID);
                
                if (task == null)
                {
                    InputClass.ErrorMsg = "There isn't a task with that ID";
                    return null;
                }
                return task;
            }
            catch (ArgumentNullException)
            {
                
                return null;
            }

        }
    }
}
