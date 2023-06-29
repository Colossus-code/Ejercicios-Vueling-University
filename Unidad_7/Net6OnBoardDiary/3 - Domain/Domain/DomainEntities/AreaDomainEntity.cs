using Domain.DomainContracts;
using Domain.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainEntities
{
    public class AreaDomainEntity : IValidatable
    {
        public string AreaId { get; set; }
        public string AreaName { get; set; }
        public List<string> LocationIdList { get; set; }

        public bool Validate()
        {
            return DomainHelper.ValidateId(AreaId) && 
                AreaName.Length <= 50 &&
                LocationIdList.Count > 0;
        }
    }
}
