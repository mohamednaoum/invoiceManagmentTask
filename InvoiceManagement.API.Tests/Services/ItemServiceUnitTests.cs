using AutoMapper;
using FluentAssertions;
using InvoiceManagement.Application.DTOs;
using InvoiceManagement.Domain.Entities;
using InvoiceManagement.Domain.Repositories;
using Moq;

namespace InvoiceManagement.API.Tests.Services
{
    public class ItemServiceTests
    {
        private readonly Mock<IItemRepository> _itemRepositoryMock;
        private readonly IMapper _mapper;
        private readonly ItemService _itemService;

        public ItemServiceTests()
        {
            _itemRepositoryMock = new Mock<IItemRepository>();
            
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Item, ItemDto>();
                cfg.CreateMap<ItemDto, Item>();
            });
            _mapper = config.CreateMapper();

            _itemService = new ItemService(_itemRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllItemsAsync_ShouldReturnAllItems()
        {
            // Arrange
            var items = new List<Item>
            {
                new Item (  1, "Item 1",  10.0m ),
                new Item ( 2, "Item 2",  20.0m )
            };

            _itemRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(items);

            // Act
            var result = await _itemService.GetAllItemsAsync();

            // Assert
            result.Should().HaveCount(2);
        }
    }
}