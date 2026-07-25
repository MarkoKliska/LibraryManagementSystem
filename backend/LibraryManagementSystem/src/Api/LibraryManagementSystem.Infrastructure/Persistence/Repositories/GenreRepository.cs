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
    public async Task<(IEnumerable<Genre> Genres, int TotalCount)> GetAllPagedAsync(int page, int pageSize, string? search, CancellationToken cancellationToken = default)
    {
        var query = context.Genres
            .Where(g => !g.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(g => EF.Functions.ILike(g.Name, $"%{term}%"));
        }

        var ordered = query.OrderBy(g => g.Name);

        var totalCount = await ordered.CountAsync(cancellationToken);

        var genres = await ordered
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (genres, totalCount);
    }
}