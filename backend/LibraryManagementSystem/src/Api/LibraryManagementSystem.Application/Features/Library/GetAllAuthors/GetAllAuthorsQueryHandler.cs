using LibraryManagementSystem.Application.DTOs.Library.AddAuthor;
using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.GetAllAuthors;

public sealed class GetAllAuthorsQueryHandler(
    IAuthorRepository authorRepository
) : IRequestHandler<GetAllAuthorsQuery, Result<IEnumerable<AddAuthorResponseDto>>>
{
    public async Task<Result<IEnumerable<AddAuthorResponseDto>>> Handle(GetAllAuthorsQuery query, CancellationToken ct)
    {
        var authors = await authorRepository.GetAllAsync(ct);

        var response = authors.Select(a => new AddAuthorResponseDto
        {
            Id = a.Id,
            FirstName = a.FirstName,
            LastName = a.LastName
        }).ToList();

        return Result<IEnumerable<AddAuthorResponseDto>>.Success(response);
    }
}