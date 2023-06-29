using Domain.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainEntities
{
    public class LocationListDomainEntity
    {
        public List<LocationDomainEntity> locationDomain { get; set; }

        public int RemoveInvalidElements()
        {
            return DomainHelper.RemoveInvalidElements(locationDomain);
        }
    }
}
