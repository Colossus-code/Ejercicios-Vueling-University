using Domain.DomainContracts;
using Domain.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.DomainEntities
{
    public class StayDomainEntity : IValidatable
    {
        public string LocationId { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime LeaveDate { get; set; }

        // Preguntar por esta validacion... por ahora la dejamos

        public bool Validate()
        {                    
            return DomainHelper.ValidateId(LocationId);
        }
    }
}
