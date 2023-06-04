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
    internal class Menu : IMenu
    {
        private Register newRegist = new Register();
        private Printer printer = new Printer();
        private PrinterBy printerBy = new PrinterBy();
        private Assigner assigner = new Assigner();

        public Task newTask;
        public Team newTeam;
        public ITWorker newWorker;

        public List<Task> tasks = new List<Task>();
        public List<Team> teams = new List<Team>();
        public List<ITWorker> workers = new List<ITWorker>();

        public void showMenu()
        {
            Console.WriteLine("\n****************************************************************\n" +
                "1. Register new IT worker. \n" +
                "2. Register new team. \n" +
                "3. Register new task. \n" +
                "4. List all team names. \n" +
                "5. List team members by team name. \n" +
                "6. List unassigned tasks. \n" +
                "7. List task assignments by team name. \n" +
                "8. Assign IT worker to a team as manager. \n" +
                "9. Assign IT worker to a team as technician. \n" +
                "10. Assign task to IT worker. \n" +
                "11. Unregister worker. \n" +
                "12. Exit. \n \n" +
                "****************************************************************");
        }

        public bool selectedChoice(int selected)
        {
            switch (selected)
            {
                case 1:
                    Console.Clear();

                    newWorker = newRegist.registNewWorker();
                    workers.Add(newWorker);
                    return false;
                case 2:
                    Console.Clear();

                    newTeam = newRegist.registNewTeam();
                    teams.Add(newTeam);
                    return false;
                case 3:
                    Console.Clear();

                    newTask = newRegist.registNewTask();
                    tasks.Add(newTask);
                    return false;
                case 4:

                    Console.Clear();

                    string printNameTeams = printer.printTeams(teams);

                    if (printNameTeams == "")
                    {
                        Console.WriteLine("****************************************************************");
                        Console.WriteLine(InputClass.ErrorMsg);
                        Console.WriteLine("****************************************************************");
                    }
                    else
                    {
                        Console.WriteLine($"****************************************************************\n{printNameTeams}\n****************************************************************");
                    }
                    return false;

                case 5:

                    Console.Clear();

                    string printTeamByName = printerBy.printTeams(teams);

                    if (printTeamByName == "")
                    {
                        Console.WriteLine("****************************************************************");
                        Console.WriteLine(InputClass.ErrorMsg);
                        Console.WriteLine("****************************************************************");
                    }
                    else
                    {
                        Console.WriteLine($"****************************************************************\n{printTeamByName}\n****************************************************************");
                    }
                    return false;
                case 6:

                    Console.Clear();

                    string print = printer.printTask(tasks);

                    if (print == "")
                    {
                        Console.WriteLine("****************************************************************");
                        Console.WriteLine(InputClass.ErrorMsg);
                        Console.WriteLine("****************************************************************");
                    }
                    else
                    {
                        Console.WriteLine("****************************************************************");
                        Console.WriteLine(print);
                        Console.WriteLine("****************************************************************");
                    }

                    return false;
                case 7:

                    Console.Clear();

                    string printTaskByTeamName = printerBy.printTask(tasks);

                    if (printTaskByTeamName == "")
                    {
                        Console.WriteLine("****************************************************************");
                        Console.WriteLine(InputClass.ErrorMsg);
                        Console.WriteLine("****************************************************************");
                    }
                    else
                    {
                        Console.WriteLine("****************************************************************");
                        Console.WriteLine(printTaskByTeamName);
                        Console.WriteLine("****************************************************************");
                    }
                    return false;
                case 8:
                    Console.Clear();

                    if (assigner.assignManagerForATeam(teams, workers))
                    {
                        Console.WriteLine("Worker has been updated to manager of the team.");
                    }
                    else
                    {
                        Console.WriteLine(InputClass.ErrorMsg);
                    }

                    return false;
                case 9:
                    Console.Clear();

                    if (assigner.assignWorkerForATeam(teams, workers))
                    {
                        Console.WriteLine("Worker has introduced to the team.");
                    }
                    else
                    {
                        Console.WriteLine(InputClass.ErrorMsg);
                    }
                    return false;
                case 10:
                    Console.Clear();

                    if (assigner.assignTaskToItWorker(workers, tasks))
                    {
                        Console.WriteLine("Task introduced.");
                    }
                    else
                    {
                        Console.WriteLine(InputClass.ErrorMsg);
                    }
                    return false;

                case 11:
                    Console.Clear();

                    if (assigner.deleteWorker(workers))
                    {
                        Console.WriteLine("Worker has been deleted.");
                    }
                    else
                    {
                        Console.WriteLine(InputClass.ErrorMsg);
                    }
                    
                    return false;
                case 12:

                    return true;

                default:
                    Console.WriteLine("Please, select a valid option.");
                    return false;
            }
        }
    }
}
