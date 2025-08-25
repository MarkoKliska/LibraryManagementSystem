using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.Library.RentBook;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.RentBook;

public sealed record RentBookCommand(RentBookRequestDto Request)
    : IRequest<Result<RentBookResponseDto>>;
