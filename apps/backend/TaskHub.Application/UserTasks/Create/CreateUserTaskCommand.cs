namespace TaskHub.Application.UserTasks.Create;

public record CreateUserTaskCommand(
    string Title,
    string Description
);