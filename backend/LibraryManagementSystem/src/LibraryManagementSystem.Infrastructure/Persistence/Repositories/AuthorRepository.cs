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
                 (string.IsNullOrEmpty(firstName) || a.FirstName.ToLower() == firstName.ToLower().Trim()),
            cancellationToken
        );
    }
}
