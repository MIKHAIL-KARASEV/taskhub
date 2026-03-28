using TaskHub.Application.Abstractions;

namespace TaskHub.Application.UserTasks.Queries.GetMyTasks;

public class GetMyTasksHandler
{
    private readonly IUserTaskRepository _repository;
    private readonly ICurrentUser _currentUser;

    public GetMyTasksHandler(
        IUserTaskRepository repository,
        ICurrentUser currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<List<UserTaskDto>> Handle(CancellationToken ct)
    {
        var tasks = await _repository.GetByUserId(_currentUser.UserId, ct);

        return tasks
            .Select(t => new UserTaskDto(
                t.Id,
                t.Title,
                t.Description,
                t.IsCompleted,
                t.CompletedAt,
                t.CreatedAt
            ))
            .ToList();
    }
}