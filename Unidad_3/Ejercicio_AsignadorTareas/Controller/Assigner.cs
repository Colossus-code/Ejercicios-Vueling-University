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
    internal class Assigner : IAssigner
    {
        public List<Team> assignManagerForATeam(List<Team> teams, List<ITWorker> workers)
        {
            if (checkParameters(teams, workers))
            {
                try
                {

                    var team = findTeamByName(teams);

                    var worker = findWorker(workers); // TODO sgarciam 3005 checkear que el ITWorker no tenga equipo actual 

                    if (worker.Team != null)
                    {
                        switchWorkersTeam(worker);
                    }
                    return assingManager(team, worker, teams);


                }
                catch (ArgumentNullException)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }


        }
        public List<Team> assignWorkerForATeam(List<Team> teams, List<ITWorker> workers)
        {
            if (checkParameters(teams, workers))
            {
                try
                {
                    var team = findTeamByName(teams);

                    var worker = findWorker(workers);

                    if (worker.Team != null)
                    {
                        switchWorkersTeam(worker);
                    }
                    team.technician.Add(worker);

                    worker.Team = team;

                    teams.Remove(team);

                    teams.Add(team);

                    return teams;

                }
                catch (ArgumentNullException)
                {

                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public bool assignTaskToItWorker(List<ITWorker> workers, List<Task> tasks)
        {
            try
            {

                var worker = findWorker(workers);

                var taskToAssign = findTask(worker, tasks);

                if (worker.techKnowledges.Contains(taskToAssign.technology))
                {
                    worker.itWorkerTask = taskToAssign;
                    taskToAssign.worker = worker;
                    taskToAssign.assigned = true;
                    return true;
                }
                else
                {
                    Console.WriteLine("The worker must to have the technology of the task to do it.");
                    return false;
                }

            }
            catch (FormatException)
            {
                return false;
            }
        }
        public void deleteWorker(List<ITWorker> workers)
        {
            var worker = findWorker(workers);

            if (worker.itWorkerTask != null)
            {
                Task unassingTask = worker.itWorkerTask;

                unassingTask.assigned = false;

                unassingTask.worker = null;

                worker.itWorkerTask = null;
            }

            if (worker.Team != null)
            {
                Team unassingTeam = worker.Team;

                unassingTeam.technician.Remove(worker);

                worker.Team = null;
            }

            worker.LeavingDate = DateTime.Now.Date;

        }
        public bool checkParameters(List<Team> teams, List<ITWorker> workers)
        {
            return teams.Count() <= 0 || workers.Count() <= 0 ? false : true;

        }
        public Team findTeamByName(List<Team> teams)
        {
            string teamName = InputClass.inputMessageString("Select the team name please.");

            return teams.FirstOrDefault(e => e.teamName.Equals(teamName));

        }
        public ITWorker findWorker(List<ITWorker> workers)
        {

            Console.WriteLine("Select the id of the worker: ");
            Console.WriteLine("\n****************************************************************");
            foreach (ITWorker wrk in workers)
            {
                Console.WriteLine($"Worker ID: {wrk.itWorkerID}\n" +
                    $"Worker name: {wrk.Name}\n");
            }
            Console.WriteLine("****************************************************************\n");
            int employerId = 0;
            int.TryParse(Console.ReadLine(), out employerId);

            return workers.FirstOrDefault(e => e.itWorkerID == employerId);


        }
        public void switchWorkersTeam(ITWorker worker)
        {
            bool found = false;
            do
            {
                if (worker.Team != null)
                {
                    string answer = InputClass.inputMessageString("This worker actually haves a team, do you want to swich team? (y/n)");

                    if (answer.Equals("y"))
                    {
                        worker.Team = null;


                    }
                    else if (answer.Equals("n"))
                    {
                        found = false;

                    }
                    else
                    {
                        Console.WriteLine("You must to select (y/n)");
                    }

                }

            } while (!found);


        }
        public Task findTask(ITWorker worker, List<Task> taskList)
        {

            bool found = false;
            do
            {
                Console.WriteLine("Select the id of the task: ");
                Console.WriteLine("\n****************************************************************");
                foreach (Task tsk in taskList.Where(e => e.assigned == false))
                {
                    Console.WriteLine($"Task ID: {tsk.taskId}\n" +
                        $"Task description: {tsk.taskDescription}\n");
                }
                Console.WriteLine("****************************************************************\n");
                int taskID = 0;
                int.TryParse(Console.ReadLine(), out taskID);

                var task = taskList.FirstOrDefault(e => e.taskId == taskID);

                return task;

            } while (!found);

        }
        public List<Team> assingManager(Team teamSelected, ITWorker worker, List<Team> teams)
        {

            if (teamSelected.managerTeam != null && worker.yearsExperiencie > 5)
            {
                do
                {
                    var answer = InputClass.inputMessageString("Actually this team has a manager, you want to swing? (y/n)");

                    if (answer.Equals("y"))
                    {
                        if (teamSelected.technician.Contains(worker))
                        {
                            teamSelected.technician.Remove(worker);
                        }
                        teamSelected.managerTeam = worker;
                        worker.Team = teamSelected;
                        teams.Remove(teamSelected);
                        teams.Add(teamSelected);

                        return teams;

                    }
                    else if (answer.Equals("n"))
                    {
                        return teams;
                    }
                    else
                    {
                        Console.WriteLine("You must to write (y/n)");
                    }

                } while (true);
            }
            else if (teamSelected.managerTeam == null && worker.yearsExperiencie > 5)
            {
                teamSelected.managerTeam = worker;
                worker.Team = teamSelected;
                teams.Remove(teamSelected);
                teams.Add(teamSelected);
                return teams;
            }
            else
            {
                Console.WriteLine("The employer must to have more than 5 years of experience.");
                return null;
            }
        }
    }
}
