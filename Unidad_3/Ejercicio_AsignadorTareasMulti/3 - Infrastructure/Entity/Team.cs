

using System.Collections.Generic;

namespace Ejercicio_AsignadorTareasMulti.Entity
{
    public class Team
    {
        public string TeamName { get; set; }
        public int ManagerTeamId { get; set; }
        public List<int> TechnicianId = new List<int>();

    }
}
