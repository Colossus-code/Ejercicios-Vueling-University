using Infrastructure.DomainEntity;
using Services.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.DataTransferObject
{
    public class ArmyDto
    {

        public string ArmyName { get; set; }

        public string ArmyCommander { get; set; }

        public string ArmyRace { get; set; }
    }
}
