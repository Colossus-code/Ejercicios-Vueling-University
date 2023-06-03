using Ejercicio_AsignadorTareas.Enum;
using Ejercicio_AsignadorTareasMulti._1___Presentation.Contracts;
using Ejercicio_AsignadorTareasMulti._1___Presentation.Helpers.InputValidation;
using Ejercicio_AsignadorTareasMulti._2___Bussines.Data_Transformation;
using Ejercicio_AsignadorTareasMulti._2___Bussines.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Ejercicio_AsignadorTareasMulti._1___Presentation.MenuManagers
{
    public class MenuManageAdmin : IMenuManageAdmin
    {
        private InputValidator _inputValidator = new InputValidator();
        private DataCoherency _dataCoherency = new DataCoherency();

        private ItWorkerDto _itWorkerDto;
        private TeamDto _teamDto;
        private TaskDto _taskDto;

        private IBuilder _builder;

        public MenuManageAdmin(IBuilder builder)
        {
            _builder = builder;
        }
        public bool manageMenuAdmin(int option)
        {
            switch (option)
            {
                case 1:

                    _itWorkerDto = buildItWorkerDto();

                    if (_builder.buildItWorker(_itWorkerDto))
                    {
                        Console.WriteLine("The IT Worker has been created.");
                    }
                    else
                    {
                        Console.WriteLine("Error creating IT Worker.");
                    }
                    return true;

                case 2:

                    _teamDto = buildTeamDto();
                    if (_builder.buildNewTeam(_teamDto))
                    {
                        Console.WriteLine("The new team has been created.");
                    }
                    else
                    {
                        Console.WriteLine("Error creating team.");
                    }

                    return true;

                case 3:

                    _taskDto = buildTaskDto();

                    if (_builder.buildNewTask(_taskDto))
                    {
                        Console.WriteLine("The new task has been created.");
                    }
                    else
                    {
                        Console.WriteLine("Error creating task.");
                    }
                    return true;

                case 4:
                    break;

                case 5:
                    break;

                case 6:
                    break;

                case 7:
                    break;

                case 8:
                    break;

                case 9:
                    break;

                case 10:
                    break;

                case 11:
                    break;

                case 12:

                    return false;

                default:

                    return false;

            }

            return true;
        }

        private ItWorkerDto buildItWorkerDto()
        {
            bool validatedData = false;

            bool addOtherKnowledge = false;

            int workerYearsExperience = 0;

            DateTime workerBirthDay;

            string workerName = _inputValidator.validationStringEntry("Please, introduce a name for the worker.");

            string workerSurname = _inputValidator.validationStringEntry("Introduce the surname of the worker.");

            do
            {
                workerBirthDay = _inputValidator.validationDateTimeEntry("Select the birthdate of the employer.", out validatedData);

                validatedData = _dataCoherency.legalAge(workerBirthDay);

                if (validatedData == false) continue;

                workerYearsExperience = _inputValidator.validationIntEntry("Introduce the working years of experience.");

                validatedData = _dataCoherency.yearsWorkingCoherency(workerBirthDay, workerYearsExperience);

            } while (validatedData != true);

            List<string> knowledge = new List<string>();

            do
            {
                string knowledgeValue = _inputValidator.validationStringEntry("Introduce the worker IT knowledge.");

                knowledge.Add(knowledgeValue);

                string answer = "";
                _inputValidator.validationYesOrNoEntry("Do you want to introduce another one? (y/n)", out answer);

                if (answer.Equals("n"))
                {
                    addOtherKnowledge = false;
                }
                else
                {
                    addOtherKnowledge = true;
                }

            } while (addOtherKnowledge != false);

            var techLevel = _inputValidator.validationWorkerLevel("Please select the level of the worker (junior , medium, senior)", workerYearsExperience);

            return new ItWorkerDto(workerName, workerSurname, workerBirthDay, workerYearsExperience, techLevel, knowledge);
        }

        private TeamDto buildTeamDto()
        {
            var teamName = _inputValidator.validationStringEntry("Please, introduce the team name");
            return new TeamDto(teamName);
        }

        private TaskDto buildTaskDto()
        {
            var taskDesc = _inputValidator.validationStringEntry("Please, introduce a description of the task.");

            var taskTech = _inputValidator.validationStringEntry("Please, introduce the technology wich will be use.");

            string defineStatus;

            _inputValidator.validationYesOrNoEntry("Do you want to give a status to the task? (y/n)", out defineStatus);

            Enum taskProgress;

            if (defineStatus.Equals("y"))
            {

                taskProgress = _inputValidator.validationTaskStatus("Wich status you want to give? (to do, doing, done)");

                return new TaskDto(taskDesc, taskTech, (TaskStatus)taskProgress);
            }
            else
            {
                Console.WriteLine("The task status will be created by to do defaults value.");
                return new TaskDto(taskDesc, taskTech, TaskStatus.todo);
            }

        }
    }
}
