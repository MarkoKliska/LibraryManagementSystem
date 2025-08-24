using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.DeleteAuthor;

public sealed class DeleteAuthorCommandHandler(
    IAuthorRepository authorRepository,
    IBookRepository bookRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<DeleteAuthorCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteAuthorCommand command, CancellationToken ct)
    {
        var author = await authorRepository.GetByIdAsync(command.Request.AuthorId, ct);
        if (author == null || author.IsDeleted)
            return Result<string>.Failure("Author not found");

        var hasBooks = await bookRepository.GetAllAsync(ct)
            .ContinueWith(t => t.Result.Any(b => b.AuthorId == command.Request.AuthorId && !b.IsDeleted), ct);
        if (hasBooks)
            return Result<string>.Failure("Cannot delete author because they have associated books");

        author.SetDeleted();
        await unitOfWork.SaveChangesAsync(ct);

        return Result<string>.Success("Author deleted successfully");
    }
}
