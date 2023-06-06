using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entity
{
    public class Team
    {
        public string TeamName { get; set; }
        public int ManagerTeamId { get; set; }
        public List<int> TechnicianId = new List<int>();

    }
}
