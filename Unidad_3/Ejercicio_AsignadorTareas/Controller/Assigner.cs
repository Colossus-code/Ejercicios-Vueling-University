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
        public readonly Finder finder = new Finder();
        //Comprobe that parameters for assing manager or worker it's not null
        public bool checkParametersNotNull(List<Team> teams, List<ITWorker> workers)
        {
            if (teams.Count() <= 0 || workers.Count() <= 0)
            {
                InputClass.ErrorMsg = "You must to create first teams and workers";

                return false;
            }
            else
            {
                return true;
            }

        }
        //Manage the assing of the manager to one team
        public bool assignManagerForATeam(List<Team> teams, List<ITWorker> workers)
        {
            if (checkParametersNotNull(teams, workers))
            {

                var team = finder.findTeamByName(teams);

                var worker = finder.findWorker(workers); // TODO sgarciam 3005 checkear que el ITWorker no tenga equipo actual 

                if (worker == null || team == null)
                {
                    return false;
                }

                if (!checkParametersManagerTeams(teams, worker))
                {
                    return false;
                }
                else
                {
                    teams.Remove(team);

                    team.managerTeam = worker;

                    teams.Add(team);

                }

                return true;

            }
            else
            {
                InputClass.ErrorMsg = "You must to create workers and teams first";
                return false;
            }
        }
        //Check that manager it's on a team as manager yet o tech and if the user want can switch, else keeps in the same situation
        public bool checkParametersManagerTeams(List<Team> teams, ITWorker worker)
        {
            try
            {
                bool switchTeamTech = true;
                bool switchManagerTeamVal = true;

                if (worker.itWorkerLevel != Enum.ITWorkerLevel.senior)
                {
                    InputClass.ErrorMsg = "The worker must to be a senior";
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
                InputClass.ErrorMsg = "Worker team won't be updated";
                return false;
            }
        }
        public bool switchManagerTeam(List<Team> teams, ITWorker worker)
        {
            Team teamToReplace = null;

            try
            {
                foreach (Team team in teams.Where(e => e.managerTeam == worker))
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

                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }
        //Manage the assing of the worker to one team
        public bool assignWorkerForATeam(List<Team> teams, List<ITWorker> workers)
        {
            if (checkParametersNotNull(teams, workers))
            {
                try
                {
                    var team = finder.findTeamByName(teams);

                    var worker = finder.findWorker(workers);

                    if (worker == null || team == null)
                    {
                        return false;
                    }
                    managerToTeach(teams, worker);

                    if (worker.Team == null || switchTechTeam(worker, teams) == true)
                    {
                        worker.Team = team;
                        team.technician.Add(worker);
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }
                catch (ArgumentNullException)
                {

                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        //Check that worker it's on a team as manager yet o tech and if the user want can switch, else keeps in the same situation
        public bool switchTechTeam(ITWorker worker, List<Team> teams)
        {
            try
            {
                foreach (var team in teams.Where(e => e.technician.Contains(worker)))
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

                if (worker == null || taskToAssign == null)
                {
                    return false;
                }

                if (worker.techKnowledges.Contains(taskToAssign.technology) && switchTask(tasks, worker, taskToAssign))
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
            try
            {
                foreach (Task task in tasks.Where(e => e.worker.Equals(worker)))
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
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }
        // Unassing the worker to one team, like tech or manager and unassing task if he had
        public bool deleteWorker(List<ITWorker> workers)
        {
            if (workers.Count == 0)
            {
                InputClass.ErrorMsg = "Workers has not created";
                return false;
            }
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
            return true;

        }
        // TODO sgarciam 0106 EXTRAER EN CLASE APARTE CON INTERFAZ


    }
}
