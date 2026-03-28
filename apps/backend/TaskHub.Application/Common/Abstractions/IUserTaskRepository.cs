using TaskHub.Domain.Entities;

namespace TaskHub.Application.Abstractions;

public interface IUserTaskRepository
{
    Task Add(UserTask task, CancellationToken ct);
    Task<List<UserTask>> GetByUserId(Guid userId, CancellationToken ct);
    Task<UserTask?> GetById(Guid id, CancellationToken ct);
    Task Delete(UserTask task, CancellationToken ct);
    Task SaveChanges(CancellationToken ct);
}