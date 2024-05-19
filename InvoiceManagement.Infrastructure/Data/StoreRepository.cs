using Microsoft.EntityFrameworkCore;
using InvoiceManagement.Domain.Entities;
using InvoiceManagement.Domain.Repositories;
using InvoiceManagement.Infrastructure.Data;

public class StoreRepository : IStoreRepository
{
    private readonly ApplicationDbContext _context;

    public StoreRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Store>> GetAllAsync()
    {
        return await _context.Stores.ToListAsync();
    }

    public async Task<Store> GetByIdAsync(int id)
    {
        return await _context.Stores.FindAsync(id);
    }

    public async Task AddAsync(Store store)
    {
        await _context.Stores.AddAsync(store);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Store store)
    {
        _context.Stores.Update(store);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Store store)
    {
        _context.Stores.Remove(store);
        await _context.SaveChangesAsync();
    }
}