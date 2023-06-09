using Infrastructure.Entities;
using Infrastructure.IRepository;
using Moq;
using Repository;
using Repository.DataBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BankAccountTest
{
    public class BankAccountRepositorySuitTest
    {
        //First of all, creating a object to test CRUD -> 
        private BankAccount _testBankAccountCreate;
        private BankAccount _testBankAccountDelete;

        private readonly BankAccountRepository _bankAccountRepo;

        //private readonly Mock<IRepositoryBankAccount> _repoBankAccount = new Mock<IRepositoryBankAccount>();

        public BankAccountRepositorySuitTest()
        {
            _bankAccountRepo = new BankAccountRepository();
        }
        private void InitializeObject()
        {
            _testBankAccountCreate = new BankAccount()
            {
                AccountId = "TSTBANK02",
                AccountPin = 1234,
                Money = 400
            };

            _testBankAccountDelete = new BankAccount()
            {
                AccountId = "TSTBANKDEL01",
                AccountPin = 1234,
                Money = 400
            };
        }

        #region Create region
        // CREATE BANK ACCOUNT CASE 
        [Fact]
        public void InputCorrectData_CreateBankAccount_AssertTrue()
        {
            InitializeObject();

            Assert.True(_bankAccountRepo.CreateBankAccount(_testBankAccountCreate));

        }

        [Fact]
        public void InputWrongData_CreateBankAccount_AssertFalse()
        {
            InitializeObject(); // DUPLICATED ACCOUNT

            Assert.False(_bankAccountRepo.CreateBankAccount(_testBankAccountCreate));


        }
        [Fact]
        public void InputNullData_CreateBankAccount_AssertFalse()
        {

            Assert.False(_bankAccountRepo.CreateBankAccount(null)); // NULL OBJECT 

        }

        #endregion

        #region Read region
        [Fact]
        public void InputCorrectData_FindAccount_AssertNotNull()
        {
            InitializeObject();

            Assert.NotNull(_bankAccountRepo.GetBankAccountByAccountNumber("TSTBANK02"));

        }

        [Fact]
        public void InputWrongData_FindAccount_AssertNotNull()
        {

            Assert.Null(_bankAccountRepo.GetBankAccountByAccountNumber("ES-1999"));


        }
        #endregion

        #region Update region
        [Fact]
        public void InputCorrectData_UpdateMoneyPositiveAmount_AssertTrue()
        {
            InitializeObject();

            BankAccountsEntities1 dataEntityAcc = new BankAccountsEntities1();

            _testBankAccountCreate.Id = dataEntityAcc.Accounts.FirstOrDefault(e => e.AccountId.Equals(_testBankAccountCreate.AccountId)).Id;

            Assert.True(_bankAccountRepo.IncomeOutcomeMoney(_testBankAccountCreate, 600)); // POSITIVE 

        }

        [Fact]
        public void InputCorrectData_UpdateMoneyNegativeAmount_AssertTrue()
        {
            InitializeObject();

            BankAccountsEntities1 dataEntityAcc = new BankAccountsEntities1();

            _testBankAccountCreate.Id = dataEntityAcc.Accounts.FirstOrDefault(e => e.AccountId.Equals(_testBankAccountCreate.AccountId)).Id;

            Assert.True(_bankAccountRepo.IncomeOutcomeMoney(_testBankAccountCreate, -400)); // NEGATIVE 
        }


        [Fact]
        public void InputWrongData_UpdateMoneyPositiveMoney_AssertFalse()
        {
            BankAccount wrongBankAccount = new BankAccount
            {
                Id = -5,
                AccountId = "ES002",
                AccountPin = 1234,
                Money = 0,
            };

            Assert.False(_bankAccountRepo.IncomeOutcomeMoney(wrongBankAccount,1000));

        }

        [Fact]
        public void InputWrongData_UpdateMoneyNegativeMoney_AssertFalse()
        {
            BankAccount wrongBankAccount = new BankAccount
            {
                Id = -5,
                AccountId = "ES002",
                AccountPin = 1234,
                Money = 0,
            };

            Assert.False(_bankAccountRepo.IncomeOutcomeMoney(wrongBankAccount, -1000));

        }
        //[Fact]
        //public void InputWrongData_UpdateMoneyNegativeMoney_AssertString()
        //{
        //    BankAccount wrongBankAccount = new BankAccount
        //    {
        //        Id = -5,
        //        AccountId = "ES002",
        //        AccountPin = 1234,
        //        Money = 0,
        //    };

        //    Assert.False(_bankAccountRepo.IncomeOutcomeMoney(wrongBankAccount, -1000));

        //}

        [Fact]
        public void InputCorrectData_UpdatePinAccount_AssertTrue()
        {
            InitializeObject();

            Assert.True(_bankAccountRepo.ChangePinAccount(_testBankAccountCreate.AccountId, "1234"));

        }

        [Fact]
        public void InputWrongData_UpdatePinAccount_AssertFalse()
        {

            Assert.False(_bankAccountRepo.ChangePinAccount("ES-1999", "12345"));


        }

        #endregion

        #region Delete region
        [Fact]
        public void InputCorrectData_DeleteBankAccount_AssertTrue()
        {
            InitializeObject();
            
            BankAccountsEntities1 _dbConn = new BankAccountsEntities1();

            _dbConn.Accounts.Add(new Accounts
            {
                AccountId = _testBankAccountDelete.AccountId,
                AccountPin = _testBankAccountDelete.AccountPin.ToString(),
                Money = _testBankAccountDelete.Money,

            });

            _dbConn.SaveChanges();

                Assert.True(_bankAccountRepo.DeleteBankAccount(_dbConn.Accounts.FirstOrDefault(e => e.AccountId.Equals(_testBankAccountDelete.AccountId)).Id));

        }

        [Fact]
        public void InputWrongData_DeleteBankAccount_AssertFalse()
        {

            Assert.False(_bankAccountRepo.DeleteBankAccount(-1999));


        }
        #endregion
    }
}
