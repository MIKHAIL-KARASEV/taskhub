using TaskHub.Application.Abstractions;

namespace TaskHub.Application.UserTasks.Update;

public class UpdateUserTaskHandler
{
    private readonly IUserTaskRepository _repository;
    private readonly ICurrentUser _currentUser;

    public UpdateUserTaskHandler(
        IUserTaskRepository repository,
        ICurrentUser currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task Handle(UpdateUserTaskCommand command, CancellationToken ct)
    {
        var task = await _repository.GetById(command.TaskId, ct);

        if (task is null)
            throw new Exception("Task not found");

        //SECURITY
        if (task.UserId != _currentUser.UserId)
            throw new Exception("Forbidden");

        //DOMAIN LOGIC
        task.Update(command.Title, command.Description);

        await _repository.SaveChanges(ct);
    }
}