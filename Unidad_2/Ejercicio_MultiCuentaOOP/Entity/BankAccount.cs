using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_MultiCuentaOOP.Entity
{
    public class BankAccount
    {
        public string IBAN { get; set; }
        public int pin { get; set; }
        public decimal moneyAccount { get; set; }

        public List<decimal> allMovements = new List<decimal>();



        public BankAccount(string id, int pin, decimal moneyAccount)
        {
            this.IBAN = id;
            this.pin = pin;
            this.moneyAccount = moneyAccount;
        }
    }
}
