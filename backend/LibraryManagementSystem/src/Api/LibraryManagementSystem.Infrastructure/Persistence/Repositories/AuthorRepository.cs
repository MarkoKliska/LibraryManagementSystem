using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Repositories;
using LibraryManagementSystem.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Persistence.Repositories;

public class AuthorRepository(
    LibraryDbContext context
) : IAuthorRepository
{
    public async Task<Author?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Authors
            .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted, cancellationToken);
    }

    public async Task AddAsync(Author author, CancellationToken cancellationToken = default)
    {
        await context.Authors.AddAsync(author, cancellationToken);
    }

    public async Task<bool> ExistsAsync(string? firstName, string lastName, CancellationToken cancellationToken = default)
    {
        return await context.Authors.AnyAsync(
            a => !a.IsDeleted &&
                 a.LastName.ToLower() == lastName.ToLower().Trim() &&
                 (string.IsNullOrEmpty(firstName) || a.FirstName!.ToLower() == firstName.ToLower().Trim()),
            cancellationToken
        );
    }

    public async Task<IEnumerable<Author>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Authors
            .Where(a => !a.IsDeleted)
            .ToListAsync(cancellationToken);
    }
    public async Task<(IEnumerable<Author> Authors, int TotalCount)> GetAllPagedAsync(int page, int pageSize, string? search, CancellationToken cancellationToken = default)
    {
        var query = context.Authors
            .Where(a => !a.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(a =>
                EF.Functions.ILike(a.FirstName ?? "", $"%{term}%") ||
                EF.Functions.ILike(a.LastName, $"%{term}%"));
        }

        var ordered = query
            .OrderBy(a => a.LastName)
            .ThenBy(a => a.FirstName);

        var totalCount = await ordered.CountAsync(cancellationToken);

        var authors = await ordered
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (authors, totalCount);
    }
}
