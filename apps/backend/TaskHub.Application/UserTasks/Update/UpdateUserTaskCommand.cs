namespace TaskHub.Application.UserTasks.Update;

public record UpdateUserTaskCommand(
    Guid TaskId,
    string Title,
    string Description
);