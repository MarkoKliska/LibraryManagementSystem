using LibraryManagementSystem.Application.DTOs.Library.AddBook;
using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.GetBookDetails;

public sealed class GetBookDetailsQueryHandler(
    IBookRepository bookRepository,
    IAuthorRepository authorRepository,
    IGenreRepository genreRepository
) : IRequestHandler<GetBookDetailsQuery, Result<AddBookResponseDto>>
{
    public async Task<Result<AddBookResponseDto>> Handle(GetBookDetailsQuery query, CancellationToken ct)
    {
        var book = await bookRepository.GetByIdAsync(query.BookId, ct);
        if (book == null || book.IsDeleted)
            return Result<AddBookResponseDto>.Failure("Book not found");

        var author = await authorRepository.GetByIdAsync(book.AuthorId, ct);
        if (author == null || author.IsDeleted)
            return Result<AddBookResponseDto>.Failure("Author not found");

        var genre = await genreRepository.GetByIdAsync(book.GenreId, ct);
        if (genre == null || genre.IsDeleted)
            return Result<AddBookResponseDto>.Failure("Genre not found");

        var response = new AddBookResponseDto
        {
            Id = book.Id,
            Title = book.Title,
            AuthorId = book.AuthorId,
            AuthorName = author.FirstName != null ? $"{author.LastName}, {author.FirstName}" : author.LastName,
            GenreId = book.GenreId,
            GenreName = genre.Name,
            Isbn13 = book.Isbn13,
            NumberOfCopies = book.Copies.Count
        };

        return Result<AddBookResponseDto>.Success(response);
    }
}
