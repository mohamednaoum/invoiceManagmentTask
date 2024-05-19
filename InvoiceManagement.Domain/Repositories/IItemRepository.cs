using InvoiceManagement.Domain.Entities;

namespace InvoiceManagement.Domain.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IItemRepository
{
    Task<IEnumerable<Item>> GetAllAsync();
    Task<Item> GetByIdAsync(int id);
    Task AddAsync(Item item);
    Task UpdateAsync(Item item);
    Task DeleteAsync(Item item);
}
