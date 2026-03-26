namespace TaskHub.Domain.Interfaces;

using TaskHub.Domain.Entities;
using TaskHub.Domain.ValueObjects;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken);
    Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken);
    Task AddAsync(User user, CancellationToken cancellationToken);
}