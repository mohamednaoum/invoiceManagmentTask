using InvoiceManagement.Domain.Entities;

namespace InvoiceManagement.Domain.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IStoreRepository
{
    Task<IEnumerable<Store>> GetAllAsync();
    Task<Store> GetByIdAsync(int id);
    Task AddAsync(Store store);
    Task UpdateAsync(Store store);
    Task DeleteAsync(Store store);
}
