using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Data_Transformation
{
    public class TeamDto
    {
        public TeamDto(string teamName)
        {
            TeamName = teamName;
        }

        public string TeamName { get; set; }
    }
}
