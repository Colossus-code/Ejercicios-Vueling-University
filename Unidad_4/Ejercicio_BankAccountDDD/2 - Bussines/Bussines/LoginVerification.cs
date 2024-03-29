﻿using Bussines.IService;
using Infrastructure.Entities;
using Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines
{
    public class LoginVerification : ILoginVerification
    {
        private IRepositoryBankAccount _repoBankAccount;

        private const string _ADMIN_MODE = "admin_mode_03525";

        public LoginVerification(IRepositoryBankAccount repoAccount)
        {

            _repoBankAccount = repoAccount;

        }

        public string ValidateLogin(string accountNumber,int pinAccount)
        {
            bool validatedAccount = false;

            if (accountNumber.Equals(_ADMIN_MODE))
            {
                return "Wellcome administrator, select one choice: \n1. Create new account. \n2. Delete one account. \n3. Exit.";
            }
            BankAccount foundBankAccount = new BankAccount();

            (validatedAccount, foundBankAccount) = _repoBankAccount.HasBankAccount(accountNumber);

            if (!validatedAccount && foundBankAccount == null)
            {
                return "The data entry has mistakes.";
            }

            if (foundBankAccount.AccountPin == pinAccount)
            {
                return "Select one choice: \n1. Show account movements. \n2. Show income movements. \n3. Show outcome movements. \n4. Set a income movement. \n5. Set a outcome movement. \n6. Change the pin number. \n7. Exit account.";
            }
            else
            {
                return "The data entry has mistakes.";
            }
        }
    }
}
