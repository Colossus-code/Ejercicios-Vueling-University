using Bussines.Data_Transformation;
using Bussines.IServices;
using Presentation.Contracts;
using Presentation.Helpers.InputValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Presentation.MenuManagers
{
    public class MenuManageAdmin : IMenuManageAdmin
    {
        private InputValidator _inputValidator = new InputValidator();
        private DataCoherency _dataCoherency = new DataCoherency();

        private ItWorkerDto _itWorkerDto;
        private TeamDto _teamDto;
        private TaskDto _taskDto;

        private IBuilder _builder;
        private IPrinterServiceAdmin _printerRepositoryAdmin;
        private IAssignerServiceAdmin _assignerRepositoryAdmin;

        public MenuManageAdmin(IBuilder builder, IPrinterServiceAdmin printerRepositoryAdmin, IAssignerServiceAdmin assignerRepositoryAdmin)
        {
            _builder = builder;
            _printerRepositoryAdmin = printerRepositoryAdmin;
            _assignerRepositoryAdmin = assignerRepositoryAdmin;

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
                    Console.Clear();
                    Console.WriteLine(_printerRepositoryAdmin.printerRepositoryTeamNames());
                    return true;

                case 5:
                    Console.Clear();
                    Console.WriteLine(_printerRepositoryAdmin.printerRepositoryITWorkersByTeamNames(_inputValidator.validationStringEntry("Introduce the name of the team.")));
                    return true;

                case 6:
                    Console.Clear();
                    Console.WriteLine(_printerRepositoryAdmin.printerRepositoryUnassignedTask());
                    return true;

                case 7:
                    Console.Clear();
                    Console.WriteLine(_printerRepositoryAdmin.printerRepositoryTaskByTeamName(_inputValidator.validationStringEntry("Introduce the name of the team.")));
                    return true;

                case 8:
                    Console.Clear();
                    assingManagerToTeam();

                    return true;

                case 9:
                    Console.Clear();
                    assingTechToTeam();

                    return true;

                case 10:
                    Console.Clear();
                    assingTaskToWorker();
                    break;

                case 11:
                    Console.Clear();
                    unassingItWorker();
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

            string taskProgress;

            if (defineStatus.Equals("y"))
            {

                taskProgress = _inputValidator.validationTaskStatus("Wich status you want to give? (to do, doing, done)");

                return new TaskDto(taskDesc, taskTech, taskProgress);
            }
            else
            {
                Console.WriteLine("The task status will be created by to do defaults value.");
                return new TaskDto(taskDesc, taskTech);
            }

        }

        private void assingManagerToTeam()
        {
            Console.WriteLine(_assignerRepositoryAdmin.getItWorkersSeniorList());
            int idSelected = _inputValidator.validationIntEntry("Select the ID of the worker.");

            Console.WriteLine(_assignerRepositoryAdmin.getTeamsList());
            string teamSelected = _inputValidator.validationStringEntry("Select the name of the team.");

            string methodResponse = "";
            bool actuallyOnTeam = _assignerRepositoryAdmin.workerHavesTeam(idSelected, teamSelected, out methodResponse);

            if (actuallyOnTeam && methodResponse != "")
            {
                string answer = "";

                _inputValidator.validationYesOrNoEntry(methodResponse, out answer);

                if (answer.Equals("n"))
                {
                    Console.WriteLine("The worker won't be updated.");
                }
                else
                {
                    Console.WriteLine(_assignerRepositoryAdmin.assingItWorkerToManager(idSelected, teamSelected));
                }
            }
            else if (actuallyOnTeam && methodResponse == "")
            {
                Console.WriteLine(methodResponse);
            }
            else
            {
                Console.WriteLine(_assignerRepositoryAdmin.assingItWorkerToManager(idSelected, teamSelected));

            }

        }

        private void assingTechToTeam()
        {
            Console.WriteLine(_assignerRepositoryAdmin.getItWorkersList());
            int idSelected = _inputValidator.validationIntEntry("Select the ID of the worker.");

            Console.WriteLine(_assignerRepositoryAdmin.getTeamsList());
            string teamSelected = _inputValidator.validationStringEntry("Select the name of the team.");

            string methodResponse = "";
            bool actuallyOnTeam = _assignerRepositoryAdmin.workerHavesTeam(idSelected, teamSelected, out methodResponse, false);

            if (actuallyOnTeam && methodResponse != "")
            {
                string answer = "";

                _inputValidator.validationYesOrNoEntry(methodResponse, out answer);

                if (answer.Equals("n"))
                {
                    Console.WriteLine("The worker won't be updated.");
                }
                else
                {
                    Console.WriteLine(_assignerRepositoryAdmin.assingItWorkerToTeach(idSelected, teamSelected));
                }
            }
            else
            {
                Console.WriteLine(_assignerRepositoryAdmin.assingItWorkerToTeach(idSelected, teamSelected));
            }
        }

        private void assingTaskToWorker()
        {
            Console.WriteLine(_assignerRepositoryAdmin.getItWorkersList());
            int idSelected = _inputValidator.validationIntEntry("Select the ID of the worker.");

            Console.WriteLine(_assignerRepositoryAdmin.getTaskList());
            int taskSelected = _inputValidator.validationIntEntry("Select the ID of the Task.");

            string methodResponse = "";

            bool actuallyAssigned = _assignerRepositoryAdmin.workerHavesTask(idSelected, taskSelected, out methodResponse);

            string answer = "";
            _inputValidator.validationYesOrNoEntry(methodResponse, out answer);

            if (answer.Equals("n"))
            {
                Console.WriteLine("The worker won't be updated.");
            }
            else
            {
                Console.WriteLine(_assignerRepositoryAdmin.assingTaskToItWorker(idSelected, taskSelected));
            }
        }

        private void unassingItWorker()
        {
            Console.WriteLine(_assignerRepositoryAdmin.getItWorkersList());
            int idSelected = _inputValidator.validationIntEntry("Select the ID of the worker.");

            string methodResponse = "";

            bool actuallyAssigned = _assignerRepositoryAdmin.workerHavesSomething(idSelected, out methodResponse);

            string answer = "";

            _inputValidator.validationYesOrNoEntry(methodResponse, out answer);

            if (answer.Equals("n"))
            {
                Console.WriteLine("The worker won't be deleted.");
            }
            else
            {
                Console.WriteLine(_assignerRepositoryAdmin.unassingItWorker(idSelected));
            }
        }
    }
}
