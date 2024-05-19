using InvoiceManagement.Application.DTOs;

namespace InvoiceManagement.Application.Interfaces;

public interface IItemService
{
    Task<IEnumerable<ItemDto>> GetAllItemsAsync();
    Task<ItemDto> GetItemByIdAsync(int id);
    Task<ItemDto> CreateItemAsync(ItemDto itemDto);
    Task<bool> UpdateItemAsync(ItemDto itemDto);
    Task<bool> DeleteItemAsync(int id);
}
