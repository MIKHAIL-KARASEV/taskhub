namespace TaskHub.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using TaskHub.Application.Abstractions;
using TaskHub.Domain.Entities;
using TaskHub.Domain.ValueObjects;
using TaskHub.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly TaskHubDbContext context;

    public UserRepository(TaskHubDbContext context)
    {
        this.context = context;
    }

    public async Task<User?> GetByEmailAsync(Email email, CancellationToken ct)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.Email.Value == email.Value, ct);
    }

    public async Task AddAsync(User user, CancellationToken ct)
    {
        await context.Users.AddAsync(user, ct);
        await context.SaveChangesAsync(ct);
    }
}