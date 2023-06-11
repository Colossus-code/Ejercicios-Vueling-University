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


        public BankAccountRepository()
        {
            _dbConnection = new BankAccountsEntities1();

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

            try
            {
                Accounts bankAccountDataEntity = null;


                bankAccountDataEntity = _dbConnection.Accounts.FirstOrDefault(e => e.Id == bankAccountDomainEntity.Id);

                if (bankAccountDataEntity != null)
                {
                    bankAccountDataEntity.AccountId = bankAccountDomainEntity.AccountId;
                    bankAccountDataEntity.AccountPin = bankAccountDomainEntity.AccountPin.ToString();
                    bankAccountDataEntity.Money = bankAccountDomainEntity.Money;
                    bankAccountDataEntity.Movements = new List<Movements>();

                }

                List<Movements> movements = _dbConnection.Movements.ToList().Where(e => e.AccountId == bankAccountDomainEntity.Id).ToList();

                if (movements != null)
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

            }
            catch (Exception) { return false; }
        } 
        public bool ChangePinAccount(string bankAccountId, string newPin)
        {
            Accounts account = _dbConnection.Accounts.FirstOrDefault(e => e.AccountId == bankAccountId);

            if (account != null)
            {
                account.AccountPin = newPin;

                _dbConnection.SaveChanges();

                return true;
            }

            return false;
        }

        public bool CreateBankAccount(BankAccount bankAccount)
        {
            Accounts accountDataEntity = null;
            try
            {
                accountDataEntity = new Accounts
                {
                    AccountId = bankAccount.AccountId,
                    AccountPin = bankAccount.AccountPin.ToString(),
                    Money = bankAccount.Money,
                };
                _dbConnection.Accounts.Add(accountDataEntity);
                _dbConnection.SaveChanges();

            }
            catch (Exception)
            {
                return false;
            }

            if (accountDataEntity.Money != 0)
            {
                Movements initialMovementDataEntity = new Movements
                {
                    AccountId = accountDataEntity.Id,
                    Amount = accountDataEntity.Money,
                    Date = DateTime.UtcNow,

                };
                _dbConnection.Movements.Add(initialMovementDataEntity);
                _dbConnection.SaveChanges();
            }

            if (accountDataEntity != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteBankAccount(int idAccount)
        {
            Accounts dataDomainBankAccountDelete = _dbConnection.Accounts.Find(idAccount);
            List<Movements> dataDomainMovementsDelete = null;

            if (dataDomainBankAccountDelete != null && dataDomainBankAccountDelete.Movements.Count > 0)
            {
                dataDomainMovementsDelete = _dbConnection.Movements.Where(e => e.AccountId == idAccount).ToList();
            }

            if (dataDomainMovementsDelete != null)
            {
                foreach (Movements move in dataDomainMovementsDelete)
                {
                    _dbConnection.Movements.Remove(move);
                }

                _dbConnection.SaveChanges();
            }

            if (dataDomainBankAccountDelete != null)
            {
                _dbConnection.Accounts.Remove(dataDomainBankAccountDelete);
                _dbConnection.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public List<BankAccount> GetAllAccounts()
        {
            List<Accounts> dataEntityAccounts = _dbConnection.Accounts.ToList();

            List<BankAccount> bankAccounts = new List<BankAccount>();

            if (dataEntityAccounts == null)
            {
                return bankAccounts;
            }

            foreach (Accounts account in dataEntityAccounts)
            {
                bankAccounts.Add(new BankAccount
                {
                    Id = account.Id,
                    AccountId = account.AccountId,
                    AccountPin = Convert.ToInt32(account.AccountPin),
                    Money = account.Money

                });

            }

            return bankAccounts;
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



    }
}

