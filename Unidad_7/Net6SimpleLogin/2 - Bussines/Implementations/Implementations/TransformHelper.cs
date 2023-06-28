using Contracts.Dto;
using DomainEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations
{
    public class TransformHelper
    {

        public static UserDomainEntity TransformDtoToEntityWithoutOrders(UserDto userDto)
        {
            UserDomainEntity userDomain = new UserDomainEntity
            {

                Orders = null,
                Username = userDto.Username,
                Password = new PasswordEncryptDomainEntity
                {
                    SaltPassword = userDto.PasswordSalt,
                    HashPassword = userDto.PasswordHash
                }
            };

            return userDomain;
        }

        public static List<OrdersDomainEntity> TransformOrderDtosToEntity(List<OrderDto> orderDto)
        {
            List<OrdersDomainEntity> ordersDomain = new List<OrdersDomainEntity>();

            foreach(OrderDto order in orderDto)
            {

                ordersDomain.Add(new OrdersDomainEntity
                {
                    OrderName = order.OrderName,
                    OrderDescription = order.OrderDescription,
                    DeliverTime = order.DeliverTime
                });
            }

            return ordersDomain; 
        }

        internal static OrdersDomainEntity TransformOrderDtoToEntity(OrderDto orderDto)
        {
            OrdersDomainEntity newOrder = new OrdersDomainEntity
            {
                OrderName = orderDto.OrderName,
                OrderDescription = orderDto.OrderDescription,
                DeliverTime = orderDto.DeliverTime
            };

            return newOrder;
        }
    }
}
