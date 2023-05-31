using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareas.Entity
{
    internal class ITWorker : Worker
    {
        public int yearsExperiencie { get; set; }
        public int itWorkerID { get; set; }
        public List<string> techKnowledges { get; set; }
        public Task itWorkerTask { get; set; }
        public Team Team { get; set; }

        public enum level
        {
            junior,
            medium,
            senior
        }

        public ITWorker(string _name, string _surname, DateTime _birthDate, int yearsWorking, List<string> knowledge, level itLvl = level.junior) : base(_name, _surname, _birthDate)
        {
            this.itWorkerID = Worker.workerId;
            this.yearsExperiencie = yearsWorking;
            this.techKnowledges = knowledge;

        }



    }
}
