using TaskHub.Application.Abstractions;
using TaskHub.Domain.Entities;
using TaskHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace TaskHub.Infrastructure.Repositories;

public class UserTaskRepository : IUserTaskRepository
{
    private readonly TaskHubDbContext _context;

    public UserTaskRepository(TaskHubDbContext context)
    {
        _context = context;
    }

    public async Task Add(UserTask task, CancellationToken ct)
    {
        await _context.UserTasks.AddAsync(task, ct);
    }

    public async Task<UserTask?> GetById(Guid id, CancellationToken ct)
    {
        return await _context.UserTasks
            .FirstOrDefaultAsync(t => t.Id == id, ct);
    }

    public async Task<List<UserTask>> GetByUserId(Guid userId, CancellationToken ct)
    {
        return await _context.UserTasks
            .Where(t => t.UserId == userId)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public Task Delete(UserTask task, CancellationToken ct)
    {
        _context.UserTasks.Remove(task);
        return Task.CompletedTask;
    }

    public async Task SaveChanges(CancellationToken ct)
    {
        await _context.SaveChangesAsync(ct);
    }

}