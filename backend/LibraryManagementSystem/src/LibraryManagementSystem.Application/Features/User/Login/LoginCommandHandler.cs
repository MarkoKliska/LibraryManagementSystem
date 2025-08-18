using LibraryManagementSystem.Application.Authentication;
using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.User.Login;
using LibraryManagementSystem.Application.Utils;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.User.Login;

public sealed class LoginCommandHandler(
    IUserRepository users,
    IJwtTokenService jwtService
) : IRequestHandler<LoginCommand, Result<LoginResponseDto>>
{
    public async Task<Result<LoginResponseDto>> Handle(LoginCommand command, CancellationToken ct)
    {
        var req = command.Request;

        var user = await users.GetByEmailAsync(req.Email, ct);
        if (user is null)
            return Result<LoginResponseDto>.Failure("Invalid email or password.");

        var valid = PasswordHasher.VerifyPassword(req.Password, user.PasswordHash);
        if (!valid)
            return Result<LoginResponseDto>.Failure("Icorrect password.");

        var token = jwtService.GenerateToken(user.Id, user.Email);

        var response = new LoginResponseDto { Token = token };
        return Result<LoginResponseDto>.Success(response);
    }
}
