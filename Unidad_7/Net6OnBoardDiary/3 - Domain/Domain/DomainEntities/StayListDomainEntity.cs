using Domain.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainEntities
{
    public class StayListDomainEntity
    {
        public List<StayDomainEntity> ListOfDomain { get; set; }    

        public int RemoveInvalidElements()
        {
            return DomainHelper.RemoveInvalidElements(ListOfDomain);

        }

        public DateTime GetEarliestDay()
        {
            return ListOfDomain.Select(e => e.ArrivalDate).ToList().Min();
        }
        public int GetNumberDays()
        {
            DateTime earliestDay = GetEarliestDay();
            DateTime lastestDay = ListOfDomain.Select(e => e.LeaveDate).ToList().Max();

            return (earliestDay - lastestDay).Days;

            
        }    
    }
}
