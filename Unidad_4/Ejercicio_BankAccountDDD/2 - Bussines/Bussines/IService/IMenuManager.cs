using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.IService
{
    public interface IMenuManager
    {       
        string GenerateInput(string accountNumber, decimal amount);
        string GenerateOutput(string accountNumber, decimal amount);
        string ChangePinAccount(string accountNumber, string newPin);
        string GetMovements(string accountNumber, string type);
        string CreateAccount(DomainBankAccountDto bankAccountDto);
        string DeleteAccount(int accountId);
        string GetAllAccounts();
    }
}
