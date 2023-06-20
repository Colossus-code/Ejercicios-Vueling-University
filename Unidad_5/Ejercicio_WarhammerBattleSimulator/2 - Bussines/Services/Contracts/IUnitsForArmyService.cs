using Services.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IUnitsForArmyService
    {
        (string, bool) AddUnitsToArmy(UnitsForArmyDto unitsForArmyDto); 

    }
}
