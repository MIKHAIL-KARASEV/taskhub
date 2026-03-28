using TaskHub.Application.Abstractions;

namespace TaskHub.Application.UserTasks.Uncomplete;

public class UncompleteUserTaskHandler
{
    private readonly IUserTaskRepository _repository;
    private readonly ICurrentUser _currentUser;

    public UncompleteUserTaskHandler(
        IUserTaskRepository repository,
        ICurrentUser currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task Handle(UncompleteUserTaskCommand command, CancellationToken ct)
    {
        var task = await _repository.GetById(command.TaskId, ct);

        if (task is null)
            throw new Exception("Task not found");

        // SECURITY
        if (task.UserId != _currentUser.UserId)
            throw new Exception("Access denied");

        // DOMAIN LOGIC
        task.Uncomplete();

        await _repository.SaveChanges(ct);
    }
}