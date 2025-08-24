using LibraryManagementSystem.Application.DTOs.Library.AddBook;
using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.UpdateBook;

public sealed class UpdateBookCommandHandler(
    IBookRepository bookRepository,
    IBookCopyRepository bookCopyRepository,
    IAuthorRepository authorRepository,
    IGenreRepository genreRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<UpdateBookCommand, Result<AddBookResponseDto>>
{
    public async Task<Result<AddBookResponseDto>> Handle(UpdateBookCommand command, CancellationToken ct)
    {
        var request = command.Request;

        if (string.IsNullOrWhiteSpace(request.Title))
            return Result<AddBookResponseDto>.Failure("Title is required");
        if (string.IsNullOrWhiteSpace(request.Isbn13) || request.Isbn13.Length != 13)
            return Result<AddBookResponseDto>.Failure("Invalid ISBN13");

        var book = await bookRepository.GetByIdAsync(request.BookId, ct);
        if (book == null || book.IsDeleted)
            return Result<AddBookResponseDto>.Failure("Book not found");

        var author = await authorRepository.GetByIdAsync(request.AuthorId, ct);
        if (author == null || author.IsDeleted)
            return Result<AddBookResponseDto>.Failure("Author not found");

        var genre = await genreRepository.GetByIdAsync(request.GenreId, ct);
        if (genre == null || genre.IsDeleted)
            return Result<AddBookResponseDto>.Failure("Genre not found");

        var existingBook = await bookRepository.GetByIsbn13Async(request.Isbn13, ct);
        if (existingBook != null && existingBook.Id != book.Id)
            return Result<AddBookResponseDto>.Failure("Book with this ISBN13 already exists");

        book.SetTitle(request.Title.Trim());
        book.SetAuthorId(request.AuthorId);
        book.SetGenreId(request.GenreId);
        book.SetIsbn13(request.Isbn13.Trim());

        var currentCopies = book.Copies.Count;
        if (request.NumberOfCopies < currentCopies)
        {
            var availableCopies = await bookCopyRepository.GetAvailableCopiesAsync(book.Id, ct);
            if (availableCopies.Count() < currentCopies)
                return Result<AddBookResponseDto>.Failure("Cannot reduce copies while some are rented");

            var copiesToRemove = book.Copies.OrderBy(c => c.Id).Skip(request.NumberOfCopies).ToList();
            foreach (var copy in copiesToRemove)
            {
                copy.SetDeleted();
            }
        }
        else if (request.NumberOfCopies > currentCopies)
        {
            for (int i = currentCopies; i < request.NumberOfCopies; i++)
            {
                var copy = new BookCopy(book.Id);
                await bookCopyRepository.AddAsync(copy, ct);
            }
        }

        await unitOfWork.SaveChangesAsync(ct);

        var response = new AddBookResponseDto
        {
            Id = book.Id,
            Title = book.Title,
            AuthorId = book.AuthorId,
            AuthorName = author.FirstName != null ? $"{author.LastName}, {author.FirstName}" : author.LastName,
            GenreId = book.GenreId,
            GenreName = genre.Name,
            Isbn13 = book.Isbn13,
            NumberOfCopies = request.NumberOfCopies
        };

        return Result<AddBookResponseDto>.Success(response);
    }
}
