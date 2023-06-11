using Bussines.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IUnitsService
    {

        List<UnitsProfileDto> FindUnitsProfiles(string race);
    }
}
