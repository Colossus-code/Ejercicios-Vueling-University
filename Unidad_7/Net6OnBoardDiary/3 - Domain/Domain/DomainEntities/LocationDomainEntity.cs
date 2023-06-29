using Domain.DomainContracts;
using Domain.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainEntities
{
    public class LocationDomainEntity : IValidatable
    {

        public string IdLocation { get; set; }
        public string NameLocation { get; set; }


        public bool Validate()
        {
            return DomainHelper.ValidateId(IdLocation) && 
                NameLocation.Length <= 100;
        }
    }
}
