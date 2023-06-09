using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepository
{
    public interface IRepositoryBankAccount
    {
        BankAccount GetBankAccountByAccountNumber(string bankAccountNumber);
        (bool, BankAccount) HasBankAccount(string bankAccountNumber);
        bool IncomeOutcomeMoney(BankAccount bankAccountId, decimal amount);
        bool ChangePinAccount(string bankAccountId, string newPin);
        bool CreateBankAccount(BankAccount bankAccount);
        bool DeleteBankAccount(int bankAccountId);
        List<BankAccount> GetAllAccounts();

    }
}
