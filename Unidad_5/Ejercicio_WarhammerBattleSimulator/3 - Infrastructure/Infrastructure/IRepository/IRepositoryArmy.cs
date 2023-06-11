using Infrastructure.DomainEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepository
{
    public interface IRepositoryArmy {

        (string, bool) SaveArmy(Army domainEntityArmy);
    }
}
