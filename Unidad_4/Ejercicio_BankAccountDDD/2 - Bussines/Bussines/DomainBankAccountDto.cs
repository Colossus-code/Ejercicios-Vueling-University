using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines
{
    public class DomainBankAccountDto
    {
        public string AccountIdentficator { get; set; }
        public int AccountPin { get; set; }
        public decimal AccountMoney { get; set; }

        public DomainBankAccountDto(string accountIden, int accountPin, decimal accountMoney)
        {
            AccountIdentficator = accountIden;
            AccountPin = accountPin;
            AccountMoney = accountMoney;
        }
    }
}
