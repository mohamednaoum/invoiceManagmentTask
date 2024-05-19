using InvoiceManagement.Application.DTOs;

namespace InvoiceManagement.Application.Interfaces;

public interface IStoreService
{
    Task<IEnumerable<StoreDto>> GetAllStoresAsync();
    Task<StoreDto> GetStoreByIdAsync(int id);
}
