using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Authentication;

public interface IJwtTokenService
{
    string GenerateToken(Guid userId, string email, UserRole role);
}
