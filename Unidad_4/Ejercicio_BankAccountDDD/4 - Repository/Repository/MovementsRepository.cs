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
        public List<Movement> getMovementsByAccountId(int accountId)
        {
            try
            {
                BankAccountsEntities1 dbConnection = new BankAccountsEntities1();
                List<Movements> movementsFromDb = dbConnection.Movements.Where(e => e.AccountId == accountId).ToList();
                List<Movement> movements = new List<Movement>();
                // QUIZA AQUI HABRIA ALGUN TIPO DE MAPEO IMPLICITO 
                foreach (var movement in movementsFromDb)
                {
                    new Movement()
                    {
                        Id = movement.Id,
                        AccountId = movement.AccountId,
                        Amount = movement.Amount,
                        DateMovement = movement.Date
                    };
                };

                return movements;
            }catch (Exception) {return null;}
        }
            

    }
}

