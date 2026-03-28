using TaskHub.Application.Abstractions;

namespace TaskHub.Application.UserTasks.Complete;

public class CompleteUserTaskHandler
{
    private readonly IUserTaskRepository _repository;
    private readonly ICurrentUser _currentUser;

    public CompleteUserTaskHandler(
        IUserTaskRepository repository,
        ICurrentUser currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task Handle(CompleteUserTaskCommand command, CancellationToken ct)
    {
        var task = await _repository.GetById(command.TaskId, ct);

        if (task is null)
            throw new Exception("Task not found");

        // SECURITY CHECK (ВАЖНО)
        if (task.UserId != _currentUser.UserId)
            throw new Exception("Forbidden");

        // DOMAIN LOGIC
        task.Complete();
        await _repository.SaveChanges(ct);
    }
}