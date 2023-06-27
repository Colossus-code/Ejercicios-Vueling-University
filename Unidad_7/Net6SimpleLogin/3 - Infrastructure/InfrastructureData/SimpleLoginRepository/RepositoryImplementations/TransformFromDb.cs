using Contracts.Dto;
using DomainEntity;
using SimpleLoginRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLoginRepository.RepositoryImplementations
{
    public class TransformFromDb
    {


        public static UserDomainEntity TransformDataToEntity(Users userFromDb)
        {

            UserDomainEntity userFound = new UserDomainEntity
            {
                Username = userFromDb.UserName,
                Password = new PasswordEncryptDomainEntity
                {
                    SaltPassword = userFromDb.UserPassword.UserSalt,
                    HashPassword = userFromDb.UserPassword.UserHash
                },
                Orders = new List<OrdersDomainEntity>()
            };

            foreach (var orders in userFromDb.Orders)
            {
                userFound.Orders.Add
                (
                    new OrdersDomainEntity
                    {
                        OrderDescription = orders.OrderDescription,
                        OrderName = orders.OrderProduct
                    }
                );
            }

            return userFound; 
        }
    }
}
