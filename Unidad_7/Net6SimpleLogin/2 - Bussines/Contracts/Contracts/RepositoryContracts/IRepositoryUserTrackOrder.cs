using Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RepositoryContracts
{
    public interface IRepositoryUserTrackOrder
    {
        void GenerateOrder(OrderDto orderDto, string userName);
        List<OrderDto> GetOrderByUsername(string userName);
        ProductDto ? GetProductByProductName(string productName);
        void RemoveFromStock(string productName);
    }
}
