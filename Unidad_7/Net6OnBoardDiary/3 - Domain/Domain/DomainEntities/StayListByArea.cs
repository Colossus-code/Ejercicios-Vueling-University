using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainEntities
{
    public class StayListByArea
    {

        public List<StayListDomainEntity> VisitedLocations {  get; set; }

        public string AreaName { get; set; }


    }
}
