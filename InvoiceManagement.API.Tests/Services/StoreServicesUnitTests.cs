using AutoMapper;
using FluentAssertions;
using InvoiceManagement.Application.DTOs;
using InvoiceManagement.Application.Services;
using InvoiceManagement.Domain.Entities;
using InvoiceManagement.Domain.Repositories;
using Moq;

namespace InvoiceManagement.API.Tests.Services
{
    public class StoreServiceTests
    {
        private readonly Mock<IStoreRepository> _storeRepositoryMock;
        private readonly IMapper _mapper;
        private readonly StoreService _storeService;

        public StoreServiceTests()
        {
            _storeRepositoryMock = new Mock<IStoreRepository>();
            
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Store, StoreDto>();
                cfg.CreateMap<StoreDto, Store>();
            });
            _mapper = config.CreateMapper();

            _storeService = new StoreService(_storeRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllStoresAsync_ShouldReturnAllStores()
        {
            // Arrange
            var stores = new List<Store>
            {
                new Store (1, "Store 1")  ,
                new Store ( 2,  "Store 2")
            };

            _storeRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(stores);

            // Act
            var result = await _storeService.GetAllStoresAsync();

            // Assert
            result.Should().HaveCount(2);
        }
    }
}
