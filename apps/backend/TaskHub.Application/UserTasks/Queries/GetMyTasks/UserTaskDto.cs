namespace TaskHub.Application.UserTasks.Queries.GetMyTasks;

public record UserTaskDto(
    Guid Id,
    string Title,
    string Description,
    bool IsCompleted,
    DateTime? CompletedAt,
    DateTime CreatedAt
);