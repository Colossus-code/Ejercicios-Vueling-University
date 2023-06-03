using Ejercicio_AsignadorTareas.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ejercicio_AsignadorTareasMulti.Entity
{
    public class ITWorker : Worker
    {
        public int ItWorkerId = WorkerId++;
        public int YearsExperiencie { get; set; }
        public List<string> TechKnowledges { get; set; }
        public int ItWorkerTaskId { get; set; }
        public string TeamName { get; set; }
        public ITWorkerLevel ItWorkerLevel { get; set; }

    }
}
