using TaskHub.Application.Abstractions;
using TaskHub.Domain.Entities;

namespace TaskHub.Application.UserTasks.Create;

public class CreateUserTaskHandler
{
    private readonly IUserTaskRepository _repository;
    private readonly ICurrentUser _currentUser;

    public CreateUserTaskHandler(
        IUserTaskRepository repository,
        ICurrentUser currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<Guid> Handle(CreateUserTaskCommand command, CancellationToken ct)
    {
        var task = UserTask.Create(
            command.Title,
            command.Description,
            _currentUser.UserId
        );

        await _repository.Add(task, ct);
        await _repository.SaveChanges(ct);

        return task.Id;
    }
}