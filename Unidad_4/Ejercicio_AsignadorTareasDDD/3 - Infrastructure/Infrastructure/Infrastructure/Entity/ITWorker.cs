using Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entity
{
    public class ITWorker : Worker
    {
        public int ItWorkerId { get; set; }
        public int YearsExperiencie { get; set; }
        public List<string> TechKnowledges { get; set; }
        public int ItWorkerTaskId { get; set; }
        public string TeamName { get; set; }
        public ITWorkerLevel ItWorkerLevel { get; set; }

    }
}
