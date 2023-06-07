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
        bool IncomeMoney(BankAccount bankAccountId, decimal amount);
        bool OutcomeMoney(int bankAccountId, decimal amount);

    }
}
