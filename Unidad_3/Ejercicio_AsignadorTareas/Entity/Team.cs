using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareas.Entity
{
    internal class Team
    {
        public string teamName { get; set; }
        public ITWorker managerTeam { get; set; }
        public List<ITWorker> technician = new List<ITWorker>();

        public Team(string teamName, ITWorker managerTeam, List<ITWorker> technician)
        {
            this.teamName = teamName;
            this.managerTeam = managerTeam;
            this.technician = technician;
        }

        public Team(string teamName) { this.teamName = teamName; }


    }
}
