using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Repositories;
using LibraryManagementSystem.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Persistence.Repositories;

public class GenreRepository(
    LibraryDbContext context
) : IGenreRepository
{
    public async Task<Genre?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Genres
            .FirstOrDefaultAsync(g => g.Id == id && !g.IsDeleted, cancellationToken);
    }

    public async Task AddAsync(Genre genre, CancellationToken cancellationToken = default)
    {
        await context.Genres.AddAsync(genre, cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await context.Genres
            .AnyAsync(g => g.Name.ToLower() == name.ToLower() && !g.IsDeleted, cancellationToken);
    }

    public async Task<IEnumerable<Genre>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Genres
            .Where(g => !g.IsDeleted)
            .ToListAsync(cancellationToken);
    }
}