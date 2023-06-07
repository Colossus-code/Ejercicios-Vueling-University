using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string AccountId { get; set; }
        public int AccountPin { get; set; }
        public decimal Money { get; set; }
        public List <int> MovementId { get; set; }

    }
}
