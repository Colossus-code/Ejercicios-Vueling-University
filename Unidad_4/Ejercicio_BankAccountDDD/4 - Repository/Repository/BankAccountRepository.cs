using Infrastructure.Entities;
using Repository.DataBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BankAccountRepository
    {
        public List<BankAccount> GetBankAccounts() // METODO QUE EMPLEAREMOS PARA EL LOGIN INICIAL INTRODUCIENDO PIN Y NUMERO DE CUENTA
        {
            BankAccountsEntities dataBaseConnection = new BankAccountsEntities();

            List<Accounts> bank = dataBaseConnection.Accounts.ToList();

            List<BankAccount> bankAccounts = new List<BankAccount>(); // TODO sgarciam 0706 añadir propiedades a bankAccount para poder recorrer en un foreach

            return null; 

        }
    }
}
