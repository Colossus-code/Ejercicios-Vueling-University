using Ejercicio_AsignadorTareasMulti._1___Presentation.Contracts;
using Ejercicio_AsignadorTareasMulti._1___Presentation.Helpers.InputValidation;
using Ejercicio_AsignadorTareasMulti._2___Bussines.Data_Transformation;
using Ejercicio_AsignadorTareasMulti._2___Bussines.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareasMulti._1___Presentation.MenuManagers
{
    public class MenuManageAdmin : IMenuManageAdmin
    {
        private InputValidator _inputValidator = new InputValidator();
        private DataCoherency _dataCoherency = new DataCoherency();
        private ItWorkerDto _itWorkerDto;
        private IBuilder _builder;

        public MenuManageAdmin(IBuilder builder)
        {
            _builder = builder;
        }
        public void manageMenuAdmin(int option)
        {
            switch (option)
            {
                case 1:

                    _itWorkerDto = buildItWorker();

                    if (_builder.buildItWorker(_itWorkerDto))
                    {
                        Console.WriteLine("The IT Worker has been created");
                    }
                    else
                    {
                        Console.WriteLine("Error creating IT Worker");
                    }
                    
                    break;
            }
        }

        private ItWorkerDto buildItWorker()
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
            
            return new ItWorkerDto(workerName,workerSurname, workerBirthDay, workerYearsExperience, techLevel, knowledge);
        }
    }
}
