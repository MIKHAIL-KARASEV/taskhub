using TaskHub.Application.Abstractions;

namespace TaskHub.Application.UserTasks.Delete;

public class DeleteUserTaskHandler
{
    private readonly IUserTaskRepository _repository;
    private readonly ICurrentUser _currentUser;

    public DeleteUserTaskHandler(
        IUserTaskRepository repository,
        ICurrentUser currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task Handle(DeleteUserTaskCommand command, CancellationToken ct)
    {
        var task = await _repository.GetById(command.TaskId, ct);

        if (task is null)
            throw new Exception("Task not found");

        // SECURITY
        if (task.UserId != _currentUser.UserId)
            throw new Exception("Forbidden");

        _repository.Delete(task, ct);

        await _repository.SaveChanges(ct);
    }
}