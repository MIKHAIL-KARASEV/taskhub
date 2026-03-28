public class UserTask
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Title { get; private set; }
    public string Description { get; private set; }
    public Guid UserId { get; private set; }

    public bool IsCompleted { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    private UserTask() { }

    private UserTask(string title, string description, Guid userId)
    {
        Title = title;
        Description = description ?? string.Empty;
        UserId = userId;
    }

    public static UserTask Create(string title, string description, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required");

        if (title.Length > 200)
            throw new ArgumentException("Title too long");

        return new UserTask(title, description, userId);
    }

    public void Update(string title, string description)
    {
        if (IsCompleted)
            throw new InvalidOperationException("Cannot update completed task");

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required");

        Title = title;
        Description = description ?? string.Empty;
    }

    public void Complete()
    {
        if (IsCompleted)
            throw new InvalidOperationException("Task already completed");

        IsCompleted = true;
        CompletedAt = DateTime.UtcNow;
    }

    public void Uncomplete()
    {
        if (!IsCompleted)
            throw new InvalidOperationException("Task is not completed");

        IsCompleted = false;
        CompletedAt = null;
    }
}