using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.DeleteBook;

public sealed class DeleteBookCommandHandler(
    IBookRepository bookRepository,
    IBookCopyRepository bookCopyRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<DeleteBookCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteBookCommand command, CancellationToken ct)
    {
        var book = await bookRepository.GetByIdAsync(command.Request.BookId, ct);
        if (book == null || book.IsDeleted)
            return Result<string>.Failure("Book not found");

        var copies = await bookCopyRepository.GetAvailableCopiesAsync(book.Id, ct);
        if (copies.Count() < book.Copies.Count)
            return Result<string>.Failure("Cannot delete book with active rentals");

        book.SetDeleted();
        foreach (var copy in book.Copies)
        {
            copy.SetDeleted();
        }

        await unitOfWork.SaveChangesAsync(ct);

        return Result<string>.Success("Book deleted successfully");
    }
}