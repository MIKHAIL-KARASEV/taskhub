namespace TaskHub.Application.Abstractions;

using TaskHub.Domain.Entities;
using TaskHub.Domain.ValueObjects;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(Email email, CancellationToken ct);
    Task AddAsync(User user, CancellationToken ct);
}