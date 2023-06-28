using Contracts;
using Contracts.CustomExceptions;
using Contracts.Dto;
using Contracts.RepositoryContracts;
using DomainEntity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Implementations
{
    public class TrackOrderService : ITrackOrderService
    {
        private readonly IRepositoryUserTrackOrder _repositoryUserTrack;
        
        private readonly IMemoryCache _memoryCacheService; 
        public TrackOrderService(IRepositoryUserTrackOrder repoUserTrack, IMemoryCache cacheService)
        {
            _repositoryUserTrack = repoUserTrack;
            _memoryCacheService = cacheService;
        }

        public string AddProduct(string userName, string productName)
        {
            
            ProductDto productDto = _repositoryUserTrack.GetProductByProductName(productName);
            
            if(productDto == null )
            {
                throw new DataIntroducedErrorException("Product don't found");
            }
            if(productDto.ProductStock == 0) 
            {

                throw new NotEnoughtStockException("Not actually haves stock for this product."); 
            
            }

            OrderDto orderDto = new OrderDto
            {
                OrderName = productDto.ProductName,
                OrderDescription = productDto.ProductDescription,
                DeliverTime = DateTime.Now.AddDays(3)
            };

            _repositoryUserTrack.RemoveFromStock(productDto.ProductName);

            _repositoryUserTrack.GenerateOrder(orderDto, userName);

            OrdersDomainEntity orderDomain = TransformHelper.TransformOrderDtoToEntity(orderDto);

            string response = JsonSerializer.Serialize(orderDomain);

            return response;
        }

        public string GetTrack(string userName)
        {
            List<OrdersDomainEntity> orderDomainCache = (List <OrdersDomainEntity>) _memoryCacheService.Get(userName);

            if(orderDomainCache == null)
            {
                List<OrderDto> orderDto = _repositoryUserTrack.GetOrderByUsername(userName);

                List<OrdersDomainEntity> orderDomain = TransformHelper.TransformOrderDtosToEntity(orderDto);

                _memoryCacheService.Set(userName, orderDomain);

                string response = JsonSerializer.Serialize(orderDomain);

                return response;
            }

            string responseCache = JsonSerializer.Serialize(orderDomainCache);

            return responseCache;

            // refactorizar
        }
    }
}
