using Contracts;
using Contracts.CustomExceptions;
using Contracts.Dto;
using Contracts.RepositoryContracts;
using DomainEntity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using SimpleLoginRepository.RepositoryImplementations;
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
        private readonly IRepositoryCache _repositoryCache; 
        

        public TrackOrderService(IRepositoryUserTrackOrder repoUserTrack, IRepositoryCache repositoryCache)
        {
            _repositoryUserTrack = repoUserTrack;
            _repositoryCache = repositoryCache;
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
            List<OrdersDomainEntity> orderDomainCache = _repositoryCache.GetCache<OrdersDomainEntity>(userName);

            if (orderDomainCache == null)
            {
                List<OrderDto> orderDto = _repositoryUserTrack.GetOrderByUsername(userName);

                if(orderDto == null)
                {
                    return null; 
                }

                List<OrdersDomainEntity> orderDomain = TransformHelper.TransformOrderDtosToEntity(orderDto);

                _repositoryCache.SetCache(userName, orderDomain);

                string response = JsonSerializer.Serialize(orderDomain);

                return response;
            }

            string responseCache = JsonSerializer.Serialize(orderDomainCache);

            return responseCache;

            // refactorizar
        }
    }
}
