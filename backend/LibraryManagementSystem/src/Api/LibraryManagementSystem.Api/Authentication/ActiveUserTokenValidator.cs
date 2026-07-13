using LibraryManagementSystem.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace LibraryManagementSystem.Api.Authentication;

public static class ActiveUserTokenValidator
{
    public static async Task ValidateAsync(TokenValidatedContext context)
    {
        var userIdClaim = context.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            context.Fail("Invalid token.");
            return;
        }

        var users = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
        var user = await users.GetByIdAsync(userId, context.HttpContext.RequestAborted);

        if (user is null)
        {
            context.Fail("User no longer exists.");
        }
    }
}