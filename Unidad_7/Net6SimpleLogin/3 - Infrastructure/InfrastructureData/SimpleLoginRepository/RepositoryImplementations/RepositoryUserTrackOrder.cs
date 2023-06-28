using Contracts.Dto;
using SimpleLoginRepository.Models; 
using Contracts.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.CustomExceptions;

namespace SimpleLoginRepository.RepositoryImplementations
{
    public class RepositoryUserTrackOrder : IRepositoryUserTrackOrder
    {
        private OrderTrackerContext _dbConnection;

        public RepositoryUserTrackOrder()
        {
            _dbConnection = new OrderTrackerContext();
        }

        public void GenerateOrder(OrderDto orderDto, string userName)
        {
            Users user = _dbConnection.Users.FirstOrDefault(u => u.UserName == userName);

            Orders order = new Orders
            {
                Customer = user,
                CustomerId = user.UserId,
                OrderProduct = orderDto.OrderName,
                OrderDescription = orderDto.OrderDescription,
                OrderDateDeliver = orderDto.DeliverTime
            };

            _dbConnection.Orders.Add(order);    

             _dbConnection.SaveChanges();
        }

        public List<OrderDto> GetOrderByUsername(string userName)
        {
            List<Orders> orders = _dbConnection.Orders.Where(e => e.Customer.UserName == userName).ToList();

            if(orders.Count == 0) 
            {

                throw new NotOrdersException("Not found orders for this user");


            }
            List<OrderDto> ordersDto = new List<OrderDto>();

            foreach(Orders order in orders) 
            {
                ordersDto.Add(
                    new OrderDto
                    {
                        OrderName = order.OrderProduct,
                        OrderDescription = order.OrderDescription,
                        DeliverTime = order.OrderDateDeliver
                    });
            
            }

            return ordersDto;
        }

        public ProductDto ? GetProductByProductName(string productName)
        {
            StockPoduct ? stockProduct = _dbConnection.StockPoduct.Where(e => e.ProductName == productName).FirstOrDefault();

            if(stockProduct == null)
            {
                return null;
            }

            ProductDto productDto = new ProductDto
            {
                ProductName = stockProduct.ProductName,
                ProductDescription = stockProduct.ProductDescription,
                ProductStock = stockProduct.ProductStock,
            };

            return productDto;
        }

        public void RemoveFromStock(string productName)
        {
            StockPoduct? stockProduct = _dbConnection.StockPoduct.Where(e => e.ProductName == productName).FirstOrDefault();

            --stockProduct.ProductStock;

            _dbConnection.SaveChanges();
        }
    }
}
