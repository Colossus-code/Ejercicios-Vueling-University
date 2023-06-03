using Ejercicio_AsignadorTareas.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareasMulti._2___Bussines.Data_Transformation
{
    public class ItWorkerDto
    {
        public string WorkerName {get;set;}
        public string WorkerSurname { get; set; }
        public DateTime WorkerBirthDay { get; set; }
        public int WorkerYearsExperience { get; set; }
        public Enum TechLevel { get; set; }
        public List<string> Knowledge { get; set; }

        public ItWorkerDto(string workerName, string workerSurname, DateTime workerBirthDay, int workerYearsExperience, Enum techLevel, List<string> knowledge)
        {
            this.WorkerName = workerName;
            this.WorkerSurname = workerSurname;
            this.WorkerBirthDay = workerBirthDay;
            this.WorkerYearsExperience = workerYearsExperience;
            this.TechLevel = techLevel;
            this.Knowledge = knowledge;
        }

    }
}
