using Bussines.IService;
using Infrastructure.Entities;
using Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            
            _repositoryBankAccount.IncomeMoney(account, amount);

            return null; //TODO 0806 recoger desde repo
        }
    }
}
