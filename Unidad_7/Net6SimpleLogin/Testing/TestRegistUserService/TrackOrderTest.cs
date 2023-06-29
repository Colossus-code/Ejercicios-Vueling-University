﻿using Contracts.Dto;
using Contracts.RepositoryContracts;
using DomainEntity;
using Implementations;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestUserService
{
    public class TrackOrderTest
    {
        private readonly Mock<IRepositoryUserTrackOrder> _repositoryUserTrackOrder = new Mock<IRepositoryUserTrackOrder>();
        private readonly Mock<IRepositoryCache> _repositoryCache = new Mock<IRepositoryCache>();

        private readonly TrackOrderService _trackOrderService;

        public TrackOrderTest()
        {
            _trackOrderService = new TrackOrderService(_repositoryUserTrackOrder.Object,_repositoryCache.Object);
        }

        [Fact]
        public void Assert_Equals_When_HavesCache()
        {
            string username = "Nannini Malini";

            List<OrdersDomainEntity>? ordersCache = new List<OrdersDomainEntity>
            {
                new OrdersDomainEntity
                {
                    OrderName = "This order",
                    OrderDescription = "This is the order",
                    DeliverTime = DateTime.Now.AddDays(1)
                }
            }; 

            string jsonOrderMock = JsonSerializer.Serialize(ordersCache);
            
            _repositoryCache.Setup(e => e.GetCache<OrdersDomainEntity>(username)).Returns(ordersCache);

            string jsonOrder = _trackOrderService.GetTrack(username);

            Assert.Equal(jsonOrder, jsonOrderMock);

        }
        [Fact]
        public void Assert_Equals_When_NotHavesCacheButDbFound()
        {
            string username = "Nannini Malini";

            List<OrdersDomainEntity>? ordersDb = new List<OrdersDomainEntity>
            {
                new OrdersDomainEntity
                {
                    OrderName = "This order",
                    OrderDescription = "This is the order",
                    DeliverTime = DateTime.Now.AddDays(1)
                }
            };

            List<OrderDto> ordersDto = new List<OrderDto>
            {
                new OrderDto
                {
                    OrderName = "This order",
                    OrderDescription = "This is the order",
                    DeliverTime = DateTime.Now.AddDays(1)
                }
            };

            List<OrdersDomainEntity> ordersCache = null; 

            _repositoryCache.Setup(e => e.GetCache<OrdersDomainEntity>(username)).Returns(ordersCache);
            
            _repositoryUserTrackOrder.Setup(e => e.GetOrderByUsername(username)).Returns(ordersDto);

            string jsonOrder = _trackOrderService.GetTrack(username);

            Assert.NotNull(jsonOrder);

        }
        [Fact]
        public void Assert_NotOrdersFound_When_NotHavesOrders()
        {
            string username = "Nannini Malini";

            List<OrdersDomainEntity>? ordersDb = null;

            List<OrderDto> ordersDto = null;

            List<OrdersDomainEntity> ordersCache = null;

            _repositoryCache.Setup(e => e.GetCache<OrdersDomainEntity>(username)).Returns(ordersCache);

            _repositoryUserTrackOrder.Setup(e => e.GetOrderByUsername(username)).Returns(ordersDto);

            string jsonOrder = _trackOrderService.GetTrack(username);

            Assert.Null(jsonOrder);
        }
       
    }
}
