using Infrastructure.Entities;
using Infrastructure.IRepository;
using Repository.DataBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BankAccountRepository : IRepositoryBankAccount
    {
        private readonly BankAccountsEntities1 _dbConnection;
        private readonly IRepositoryMovements _repositoryMovements;

        public BankAccountRepository(IRepositoryMovements repoMovements)
        {
            _dbConnection = new BankAccountsEntities1();
            _repositoryMovements = repoMovements;
        }

        public (bool, BankAccount) HasBankAccount(string bankAccountNumber)
        {

            BankAccount foundBankAccount = GetBankAccountByAccountNumber(bankAccountNumber);

            if (foundBankAccount != null)
            {
                return (true, foundBankAccount);
            }
            else
            {
                return (false, null);
            }

        }
        public bool IncomeOutcomeMoney(BankAccount bankAccountDomainEntity, decimal amount)
        {
            
            Accounts bankAccountDataEntity = _dbConnection.Accounts.FirstOrDefault(e=> e.Id == bankAccountDomainEntity.Id);
            
            if(bankAccountDataEntity != null)
            {
                bankAccountDataEntity.AccountId = bankAccountDomainEntity.AccountId;
                bankAccountDataEntity.AccountPin = bankAccountDomainEntity.AccountPin.ToString();
                bankAccountDataEntity.Money = bankAccountDomainEntity.Money;
                bankAccountDataEntity.Movements = new List<Movements>();

            }
           
            List<Movements> movements = _dbConnection.Movements.ToList().Where(e => e.AccountId == bankAccountDomainEntity.Id).ToList();

            if(movements != null)
            {
                if (amount > 0)
                {
                    movements.Add(new Movements()
                    {
                        Amount = amount,
                        Date = DateTime.Now,
                        AccountId = bankAccountDomainEntity.Id,
                    });
                }
                else
                {
                    movements.Add(new Movements()
                    {
                        Amount = amount,
                        Date = DateTime.Now,
                        AccountId = bankAccountDomainEntity.Id,
                    });
                }
            bankAccountDataEntity.Movements = movements;

                _dbConnection.SaveChanges();

                return true; 
            }

            return false;
            //TODO 0806 añadir movimientos
        }
        public BankAccount GetBankAccountByAccountNumber(string bankAccountNumber)
        {

            Accounts dbAccount = _dbConnection.Accounts.FirstOrDefault(e => e.AccountId.Equals(bankAccountNumber));
            BankAccount domainAccount = null;

            if (dbAccount != null)
            {
                domainAccount = new BankAccount
                {
                    Id = dbAccount.Id,
                    AccountId = dbAccount.AccountId,
                    AccountPin = Convert.ToInt32(dbAccount.AccountPin),
                    Money = dbAccount.Money,
                    MovementId = new List<int>()
                };
            }
            else
            {
                return null;
            }

            List<Movements> moves = _dbConnection.Movements.Where(e => e.AccountId == domainAccount.Id).ToList();

            if (moves != null)
            {
                foreach (Movements movement in moves)
                {
                    domainAccount.MovementId.Add(movement.Id);
                }
            }

            return domainAccount;
        }

        public bool ChangePinAccount(string bankAccountId, string newPin)
        {
            Accounts account = _dbConnection.Accounts.FirstOrDefault(e => e.AccountId ==  bankAccountId);

            if (account != null)
            {
                account.AccountPin = newPin;

                _dbConnection.SaveChanges();

                return true;
            }

            return false;
        }
    }
}

