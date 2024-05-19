using InvoiceManagement.Application.DTOs;

namespace InvoiceManagement.Application.Interfaces;

public interface IInvoiceService
{
    Task<InvoiceDto> GetByIdAsync(int id);
    Task<IEnumerable<InvoiceDto>> GetAllAsync();
    Task AddAsync(InvoiceDto invoiceDto);
    Task UpdateAsync(InvoiceDto invoiceDto);
    Task DeleteAsync(int id);
}
