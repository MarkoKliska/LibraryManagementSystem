using LibraryManagementSystem.Application.Authentication;
using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.User.CreateUser;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Application.Utils;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;
using System.Net.Mail;

namespace LibraryManagementSystem.Application.Features.User.CreateUser;

public sealed class CreateUserCommandHandler(
    IUserRepository users,
    IUnitOfWork uow,
    IJwtTokenService jwtService,
    IDomainEventDispatcher domainEventDispatcher
)
    : IRequestHandler<CreateUserCommand, Result<CreateUserResponseDto>>
{
    public async Task<Result<CreateUserResponseDto>> Handle(
        CreateUserCommand command,
        CancellationToken ct)
    {
        var req = command.Request;

        if (string.IsNullOrWhiteSpace(req.FirstName) ||
            string.IsNullOrWhiteSpace(req.LastName) ||
            string.IsNullOrWhiteSpace(req.Email))
            return Result<CreateUserResponseDto>.Failure("Invalid input");

        if (!MailAddress.TryCreate(req.Email, out _))
            return Result<CreateUserResponseDto>.Failure("Invalid email format.");

        if (string.IsNullOrWhiteSpace(req.Password) || req.Password.Length < 6)
            return Result<CreateUserResponseDto>.Failure("Password needs to be at least 6 characters long.");

        var exists = await users.GetByEmailAsync(req.Email, ct);
        if (exists is not null)
            return Result<CreateUserResponseDto>.Failure("Email already exists.");

        var passwordHash = PasswordHasher.HashPassword(req.Password);

        var user = new LibraryManagementSystem.Domain.Entities.User(
            firstName: req.FirstName,
            lastName: req.LastName,
            email: req.Email,
            passwordHash: passwordHash
        );

        await users.AddAsync(user, ct);
        await uow.SaveChangesAsync(ct);
        await domainEventDispatcher.DispatchEventsAsync(ct);

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