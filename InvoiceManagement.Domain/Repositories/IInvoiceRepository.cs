using InvoiceManagement.Domain.Entities;

namespace InvoiceManagement.Domain.Repositories;

public interface IInvoiceRepository
{
    Task<Invoice> GetByIdAsync(int id);
    Task<IEnumerable<Invoice>> GetAllAsync();
    Task AddAsync(Invoice invoice);
    Task UpdateAsync(Invoice invoice);
    Task DeleteAsync(Invoice invoice);
}
