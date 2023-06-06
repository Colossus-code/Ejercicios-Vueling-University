using Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Data_Transformation
{
    public class ItWorkerDto
    {
        public string WorkerName { get; set; }
        public string WorkerSurname { get; set; }
        public DateTime WorkerBirthDay { get; set; }
        public int WorkerYearsExperience { get; set; }
        public ITWorkerLevel TechLevel { get; set; }
        public List<string> Knowledge { get; set; }

        public ItWorkerDto(string workerName, string workerSurname, DateTime workerBirthDay, int workerYearsExperience, Enum techLevel, List<string> knowledge)
        {
            this.WorkerName = workerName;
            this.WorkerSurname = workerSurname;
            this.WorkerBirthDay = workerBirthDay;
            this.WorkerYearsExperience = workerYearsExperience;
            this.TechLevel = (ITWorkerLevel)techLevel;
            this.Knowledge = knowledge;
        }

    }
}
