using Ejercicio_AsignadorTareas.Controller.Interfaces;
using Ejercicio_AsignadorTareas.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Task = Ejercicio_AsignadorTareas.Entity.Task;

namespace Ejercicio_AsignadorTareas.Controller
{
    internal class Assigner : IAssigner
    {
        public readonly Finder finder; 
        //Comprobe that parameters for assing manager or worker it's not null
        public bool checkParametersNotNull(List<Team> teams, List<ITWorker> workers)
        {
            return teams.Count() <= 0 || workers.Count() <= 0 ? false : true;

        }
        //Manage the assing of the manager to one team
        public List<Team> assignManagerForATeam(List<Team> teams, List<ITWorker> workers)
        {
            if (checkParametersNotNull(teams, workers))
            {

                var team = finder.findTeamByName(teams);

                var worker = finder.findWorker(workers); // TODO sgarciam 3005 checkear que el ITWorker no tenga equipo actual 


                if (worker == null || team == null)
                {
                    return null;
                }

                if (!checkParametersManagerTeams(teams, worker))
                {
                    return null;
                }
                else
                {
                    teams.Remove(team);

                    team.managerTeam = worker;

                    teams.Add(team);

                }

                return teams;

            }
            else
            {
                InputClass.ErrorMsg = "You must to create workers and teams first";
                return null;
            }
        }
        //Check that manager it's on a team as manager yet o tech and if the user want can switch, else keeps in the same situation
        public bool checkParametersManagerTeams(List<Team> teams, ITWorker worker)
        {
            try
            {
                bool switchTeamTech = true;
                bool switchManagerTeamVal = true;

                if (worker.yearsExperiencie < 5)
                {
                    InputClass.ErrorMsg = "The worker must to be a senior"; // HACK sgarciam 3105 aplicar enum de forma correcta
                    return false;
                }
                if (worker.Team != null)
                {
                    switchTeamTech = switchTechToManager(worker);

                }

                switchManagerTeamVal = switchManagerTeam(teams, worker);

                if (switchTeamTech == true && switchManagerTeamVal == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (ArgumentNullException)
            {
                return true;
            }
        }
        public bool switchTechToManager(ITWorker worker)
        {

            if (InputClass.inputMessageString("This worker actually haves a team, do you want to swich team? (y/n)").ToLower().Equals("y"))
            {
                var teamOfTheWorker = worker.Team;
                teamOfTheWorker.technician.Remove(worker);
                return true;

            }
            else
            {
                Console.WriteLine("Worker team won't be updated");
                return false;
            }
        }
        public bool switchManagerTeam(List<Team> teams, ITWorker worker)
        {
            Team teamToReplace = null;

            foreach (Team team in teams.Where(e => e.managerTeam != null))
            {
                if (team.managerTeam == worker)
                {
                    teamToReplace = team;

                    if (InputClass.inputMessageString("The worker actually haves a team, you want to swich to make manager? (y/n)").ToLower().Equals("y"))
                    {
                        teamToReplace.managerTeam = null;
                        return true;
                    }
                    else
                    {
                        InputClass.ErrorMsg = "The worker won't be updated";
                        return false;
                    }
                }
            }

            return true;

        }
        //Manage the assing of the worker to one team
        public List<Team> assignWorkerForATeam(List<Team> teams, List<ITWorker> workers)
        {
            if (checkParametersNotNull(teams, workers))
            {
                try
                {
                    var team = finder.findTeamByName(teams);

                    var worker = finder.findWorker(workers);

                    managerToTeach(teams, worker);

                    if (worker.Team == null || switchTechTeam(worker, teams) == true)
                    {
                        worker.Team = team;
                        team.technician.Add(worker);
                        return teams;
                    }
                    else
                    {
                        return null;
                    }


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
        //Check that worker it's on a team as manager yet o tech and if the user want can switch, else keeps in the same situation
        public bool switchTechTeam(ITWorker worker, List<Team> teams)
        {
            try
            {
                foreach (var team in teams.Where(e => e.technician != null))
                {
                    if (team.technician.Contains(worker))
                    {
                        if (InputClass.inputMessageString("This worker actually haves a team, do you want to swich team? (y/n)").ToLower().Equals("y"))
                        {
                            team.technician.Remove(worker);

                            return true;
                        }
                        else
                        {
                            InputClass.ErrorMsg = "The worker won't be updated";
                            return false;
                        }

                    }
                }

                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }
        public bool managerToTeach(List<Team> teams, ITWorker worker)
        {
            try
            {
                foreach (var team in teams.Where(e => e.managerTeam != null))
                {
                    if (team.managerTeam.Equals(worker))
                    {
                        if (InputClass.inputMessageString("This worker actually is a manager of a team, do you want to swich? (y/n)").ToLower().Equals("y"))
                        {
                            team.managerTeam = null;

                            return true;
                        }
                        else
                        {
                            InputClass.ErrorMsg = "The worker won't be updated";
                            return false;
                        }

                    }
                }

                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }
        //Manage the assing of the task to one worker
        public bool assignTaskToItWorker(List<ITWorker> workers, List<Task> tasks)
        {
            if (workers.Count == 0 || tasks.Count == 0)
            {
                InputClass.ErrorMsg = "You must to create workers or task first";
                return false;
            }

            try
            {

                var worker = finder.findWorker(workers);

                var taskToAssign = finder.findTask(worker, tasks);

                if (worker.techKnowledges.Contains(taskToAssign.technology) && switchTask(tasks,worker,taskToAssign))
                {
                    worker.itWorkerTask = taskToAssign;
                    taskToAssign.worker = worker;
                    taskToAssign.assigned = true;
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (FormatException)
            {
                InputClass.ErrorMsg = "The worker must to have the technology of the task to do it.";
                return false;
            }
        }
        //Check that worker haves a task yet and if the user want can switch, else keeps in the same situation
        public bool switchTask(List<Task> tasks, ITWorker worker, Task allreadyAssigned)
        {
            foreach (Task task in tasks.Where(e => e.worker != null))
            {
                if (task.worker.Equals(worker))
                {

                    if (InputClass.inputMessageString("The worker actually haves a task, you want to swich? (y/n)").ToLower().Equals("y"))
                    {
                        task.worker = null;
                        return true;
                    }
                    else
                    {
                        InputClass.ErrorMsg = "The worker's task won't be updated";
                        return false;
                    }
                }
            }

            return true;
        }
        // Unassing the worker to one team, like tech or manager and unassing task if he had
        public void deleteWorker(List<ITWorker> workers)
        {
            var worker = finder.findWorker(workers);

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
        // TODO sgarciam 0106 EXTRAER EN CLASE APARTE CON INTERFAZ
        

    }
}
