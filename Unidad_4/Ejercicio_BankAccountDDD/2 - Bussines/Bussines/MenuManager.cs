using Bussines.IService;
using Infrastructure.Entities;
using Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bussines
{
    public class MenuManager : IMenuManager
    {
        private readonly IRepositoryBankAccount _repositoryBankAccount;
        private readonly IRepositoryMovements _repositoryMovements;

        public MenuManager(IRepositoryMovements repoMovs, IRepositoryBankAccount repoBank)
        {
            _repositoryBankAccount = repoBank;
            _repositoryMovements = repoMovs;
        }
        public string GenerateInput(string accountNumber, decimal amount)
        {
            BankAccount account = _repositoryBankAccount.GetBankAccountByAccountNumber(accountNumber);

            account.Money += amount; 
            
            if(_repositoryBankAccount.IncomeOutcomeMoney(account, amount))
            {
                return "Operation has been done successfully.";
            }

            return $"Something was wrong inserting money in {accountNumber} account.";
        }        
        public string GenerateOutput(string accountNumber, decimal amount)
        {
            BankAccount account = _repositoryBankAccount.GetBankAccountByAccountNumber(accountNumber);

            if(account.Money < amount)
            {
                return "Unallow operation, you havent enought money.";
            }
            account.Money += amount; 
            
            if(_repositoryBankAccount.IncomeOutcomeMoney(account, amount))
            {
                return "Operation has been done successfully.";
            }

            return $"Something was wrong outcoming money in {accountNumber} account.";
        }

        public string ChangePinAccount(string accountNumber, string newPin)
        {
            Regex validPin = new Regex("^[1-9]{4}$");

            if (!validPin.IsMatch(newPin))
            {
                return $"Pin must to have 4 numeric digits, your pin {newPin} it's not allow"; 
            }
            if(_repositoryBankAccount.ChangePinAccount(accountNumber, newPin))
            {

                return $"Pin has been changed for the account {accountNumber}.";
            }
            else
            {
                return "Someting was wrong updating the pin account.";
            }
        }
    }
}
