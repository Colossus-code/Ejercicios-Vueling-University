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

            if(account.Money - amount < 0)
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

        public string GetMovements(string accountNumber, string type)
        {
            BankAccount bankAccountDomainEntity = _repositoryBankAccount.GetBankAccountByAccountNumber(accountNumber);

            List<Movement> movementsAccountDomainEntity = _repositoryMovements.getMovementsByAccountId(bankAccountDomainEntity);

            if(movementsAccountDomainEntity.Count > 0)
            {
                string domainMovementsListToString = null;

                if (type.Equals("all")){

                    foreach (Movement movement in movementsAccountDomainEntity)
                    {
                        domainMovementsListToString += $"\nMovement amount: {movement.Amount}. \nMovement date: {movement.DateMovement}.";
                    }
                
                }else if (type.Equals("positive"))
                {
                    foreach (Movement movement in movementsAccountDomainEntity.Where(e => e.Amount > 0))
                    {
                        domainMovementsListToString += $"\nMovement ID: {movement.Id}. \nMovement amount: {movement.Amount}. \nMovement date: {movement.DateMovement}.";
                    }
                }
                else
                {
                    foreach (Movement movement in movementsAccountDomainEntity.Where(e => e.Amount < 0))
                    {
                        domainMovementsListToString += $"\nMovement ID: {movement.Id}. \nMovement amount: {movement.Amount}. \nMovement date: {movement.DateMovement}.";
                    }
                }
                if (domainMovementsListToString == null)
                {
                    return $"Not {type} found movements for {accountNumber} account.";
                }
                return domainMovementsListToString;
            }
            else
            {
                return $"Not found movements for {accountNumber} account.";
            }
        }

        public string CreateAccount(DomainBankAccountDto bankAccountDto)
        {
            if(_repositoryBankAccount.CreateBankAccount(new BankAccount
            {
                AccountId = bankAccountDto.AccountIdentficator,
                AccountPin = bankAccountDto.AccountPin,
                Money = bankAccountDto.AccountMoney
            }))
            {
                return "Account has been created succesfully.";
            }
            else
            {
                return "Something was wrong creating the account.";
            }

        }
        
        public string DeleteAccount(int accountId)
        {
            if (_repositoryBankAccount.DeleteBankAccount(accountId))
            {
                return "Account and all of his moves has been deleted.";
            }
            else
            {
                return "Something was wrong deleting the account.";
            }
        }

        public string GetAllAccounts()
        {

            List<BankAccount> bankAccounts = _repositoryBankAccount.GetAllAccounts();

            string domainEntityBAListToString = null;

            if(bankAccounts.Count > 0)
            {
                foreach(BankAccount bankAccount in bankAccounts)
                {
                    domainEntityBAListToString += $"\nAccount ID: {bankAccount.Id}.\nAccount identificator: {bankAccount.AccountId}.";
                }
            }
            else
            {
                return "No bank accounts were found.";
            }

            return domainEntityBAListToString;
        }
    }
}
