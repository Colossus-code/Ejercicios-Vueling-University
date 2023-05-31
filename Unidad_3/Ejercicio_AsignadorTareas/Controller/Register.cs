using Ejercicio_AsignadorTareas.Controller.Interfaces;
using Ejercicio_AsignadorTareas.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ejercicio_AsignadorTareas.Entity.ITWorker;
using Task = Ejercicio_AsignadorTareas.Entity.Task;

namespace Ejercicio_AsignadorTareas.Controller
{
    internal class Register : IRegister
    {
        public Task registNewTask()
        {
            var taskDesc = InputClass.inputMessageString("Please, introduce a description of the task.");

            var taskTech = InputClass.inputMessageString("Please, introduce the technology wich will be use.");

            bool correctAnswers = false;

            do
            {
                var defineStatus = InputClass.inputMessageString("Do you want to give a status to the task? (y/n)").ToLower();

                var taskProgress = Task.status.todo;

                if (defineStatus.Equals("y"))
                {

                    defineStatus = InputClass.inputMessageString("Wich status you want to give? (to do, doing, done)").ToLower().Trim();

                    switch (defineStatus)
                    {
                        case "todo":

                            break;

                        case "doing":
                            taskProgress = Task.status.doing;
                            break;

                        case "done":
                            taskProgress = Task.status.done;
                            break;

                        default:

                            Console.WriteLine("Please, select a correct answer (to do, doing, done)");
                            break;
                    }
                }
                else if (defineStatus.Equals("n"))
                {
                    Console.WriteLine("The task status will be created by to do defaults value.");
                }

                Console.WriteLine("The task has been created.");
                return new Task(taskDesc, taskTech, taskProgress);

            } while (!correctAnswers);


        }

        public Team registNewTeam()
        {
            string teamName = InputClass.inputMessageString("Please, introduce a name for the team.");

            return new Team(teamName);
        }

        public ITWorker registNewWorker()
        {
            var workerName = InputClass.inputMessageString("Please, introduce a name for the worker.");

            var workerSurname = InputClass.inputMessageString("Introduce the surname of the worker.");

            int workingYears = 0;
            DateTime workerBirthday = InputClass.inputMessageDateTime("Select the birthdate of the employer.", out workingYears);

            List<string> knowledge = new List<string>();

            knowledge = registKnowledges(knowledge);

            var lvlSelect = InputClass.inputMessageString("Please select the level of the worker (junior , medium, senior)").ToLower();

            var techLevel = level.junior;
            switch (lvlSelect)
            {
                case "junior":
                    break;
                case "medium":
                    techLevel = level.medium;
                    break;
                case "senior":
                    if (workingYears >= 5)
                    {
                        techLevel = level.senior;
                    }
                    else
                    {
                        Console.WriteLine("You must to have more than 5 years of experience to be senior.");
                    }
                    break;
                default:
                    Console.WriteLine("Please, select a correct answer (junior , medium, senior)");
                    break;
            }
            return new ITWorker(workerName, workerSurname, workerBirthday, workingYears, knowledge, techLevel);
        }
        public List<String> registKnowledges(List<String> knowledge)
        {
            knowledge.Add(InputClass.inputMessageString("Write the tech knowledge of the employer."));

            do
            {
                var answer = InputClass.inputMessageString("Do you want to add another one? (y/n)").ToLower();

                if (answer.Equals("n"))
                {
                    break;

                }
                else if (answer.Equals("y"))
                {
                    knowledge.Add(InputClass.inputMessageString("Add the another tech."));

                }
                else
                {
                    Console.WriteLine("Please select y/n");
                }
            } while (true);

            return knowledge;
        }
    }
}
