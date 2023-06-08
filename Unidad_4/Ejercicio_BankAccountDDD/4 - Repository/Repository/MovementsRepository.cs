using Infrastructure.Entities;
using Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DataBaseModel
{
    public class MovementsRepository : IRepositoryMovements
    {
        private readonly BankAccountsEntities1 _bankAccountEntities;

        public MovementsRepository()
        {
            _bankAccountEntities = new BankAccountsEntities1();
        }
        public List<Movement> getMovementsByAccountId(BankAccount bankAccount)
        {
            List<Movements> movementsFromDataEntity = _bankAccountEntities.Movements.Where(e=> e.AccountId == bankAccount.Id).ToList();

            List<Movement> movementsEntityDomain = new List<Movement>();

            if (movementsFromDataEntity != null)
            {
                foreach (Movements move in movementsFromDataEntity)
                {
                    movementsEntityDomain.Add(new Movement
                    {
                        AccountId = bankAccount.Id,
                        Amount = move.Amount,
                        DateMovement = move.Date
                    });
                }
            }

            return movementsEntityDomain;

        }
            

    }
}

