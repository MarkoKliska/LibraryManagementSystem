using LibraryManagementSystem.Application.DTOs.Library.AddAuthor;
using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.AddAuthor;

public sealed class AddAuthorCommandHandler(
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<AddAuthorCommand, Result<AddAuthorResponseDto>>
{
    public async Task<Result<AddAuthorResponseDto>> Handle(AddAuthorCommand command, CancellationToken ct)
    {
        var request = command.Request;

        if (string.IsNullOrWhiteSpace(request.LastName))
            return Result<AddAuthorResponseDto>.Failure("Last name is required");

        var exists = await authorRepository.ExistsAsync(request.FirstName, request.LastName, ct);
        if (exists)
            return Result<AddAuthorResponseDto>.Failure("Author already exists");

        var author = new Author(request.FirstName?.Trim(), request.LastName.Trim());
        await authorRepository.AddAsync(author, ct);
        await unitOfWork.SaveChangesAsync(ct);

        var response = new AddAuthorResponseDto
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName
        };

        return Result<AddAuthorResponseDto>.Success(response);
    }
}
