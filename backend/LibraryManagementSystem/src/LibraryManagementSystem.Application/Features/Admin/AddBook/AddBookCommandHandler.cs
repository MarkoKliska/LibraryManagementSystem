using LibraryManagementSystem.Application.DTOs.Admin.AddBook;
using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Features.Admin.AddBook;

public sealed class AddBookCommandHandler(
    IBookRepository bookRepository,
    IBookCopyRepository bookCopyRepository,
    IAuthorRepository authorRepository,
    IGenreRepository genreRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<AddBookCommand, Result<AddBookResponseDto>>
{
    public async Task<Result<AddBookResponseDto>> Handle(AddBookCommand command, CancellationToken ct)
    {
        var request = command.Request;

        if (string.IsNullOrWhiteSpace(request.Title))
            return Result<AddBookResponseDto>.Failure("Title is required");
        if (string.IsNullOrWhiteSpace(request.Isbn13) || request.Isbn13.Length != 13)
            return Result<AddBookResponseDto>.Failure("Invalid ISBN13");
        if (request.NumberOfCopies < 1)
            return Result<AddBookResponseDto>.Failure("At least one copy is required");

        var author = await authorRepository.GetByIdAsync(request.AuthorId, ct);
        if (author == null || author.IsDeleted)
            return Result<AddBookResponseDto>.Failure("Author not found");

        var genre = await genreRepository.GetByIdAsync(request.GenreId, ct);
        if (genre == null || genre.IsDeleted)
            return Result<AddBookResponseDto>.Failure("Genre not found");

        var existingBook = await bookRepository.GetByIsbn13Async(request.Isbn13, ct);
        if (existingBook != null)
            return Result<AddBookResponseDto>.Failure("Book with this ISBN13 already exists");

        var book = new Book(request.Title.Trim(), request.AuthorId, request.GenreId, request.Isbn13.Trim());
        await bookRepository.AddAsync(book, ct);

        for (int i = 0; i < request.NumberOfCopies; i++)
        {
            var copy = new BookCopy(book.Id);
            await bookCopyRepository.AddAsync(copy, ct);
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
