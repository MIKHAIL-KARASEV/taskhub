namespace TaskHub.Application.Abstractions;

using TaskHub.Domain.Enums;

public interface IJwtProvider
{
    string GenerateToken(Guid userId, string email, UserRole role);
}