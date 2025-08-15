using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Infrastructure.Persistence.Contexts;

namespace LibraryManagementSystem.Infrastructure.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly LibraryDbContext _context;
    public UnitOfWork(LibraryDbContext context)
    {
        _context = context;
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
