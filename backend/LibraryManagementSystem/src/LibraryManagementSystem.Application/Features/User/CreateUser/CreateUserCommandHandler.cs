using LibraryManagementSystem.Application.Authentication;
using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.User.CreateUser;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Application.Utils;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.User.CreateUser;

public sealed class CreateUserCommandHandler(
    IUserRepository users,
    IUnitOfWork uow,
    IJwtTokenService jwtService
)
    : IRequestHandler<CreateUserCommand, Result<CreateUserResponseDto>>
{
    public async Task<Result<CreateUserResponseDto>> Handle(
        CreateUserCommand command,
        CancellationToken ct)
    {
        var req = command.Request;

        var exists = await users.GetByEmailAsync(req.Email, ct);
        if (exists is not null)
            return Result<CreateUserResponseDto>.Failure("Email already exists.");

        var passwordHash = PasswordHasher.HashPassword(req.Password);

        var roleEnum = Enum.TryParse<UserRole>(req.Role, ignoreCase: true, out var parsed)
            ? parsed
            : UserRole.User;

        var user = new LibraryManagementSystem.Domain.Entities.User(
            firstName: req.FirstName,
            lastName: req.LastName,
            email: req.Email,
            passwordHash: passwordHash,
            role: roleEnum
        );

        await users.AddAsync(user, ct);
        await uow.SaveChangesAsync(ct);

        var token = jwtService.GenerateToken(user.Id, user.Email, user.Role);

        var response = new CreateUserResponseDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role.ToString(),
            Token = token
        };

        return Result<CreateUserResponseDto>.Success(response);
    }
}
