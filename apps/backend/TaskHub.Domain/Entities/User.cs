namespace TaskHub.Domain.Entities;

using TaskHub.Domain.ValueObjects;
using TaskHub.Domain.Enums;

public class User
{
    private User()
    {
    }

    private User(Email email, string passwordHash)
    {
        Id = Guid.NewGuid();
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = DateTime.UtcNow;
        Role = UserRole.User;
    }

    public Guid Id { get; private set; }
    public Email Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public UserRole Role { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public static User Create(Email email, string passwordHash)
    {
        if (email is null)
            throw new ArgumentNullException(nameof(email));

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash cannot be empty");

        return new User(email, passwordHash);
    }

    public void PromoteToAdmin()
    {
        if (Role == UserRole.Admin)
            return;

        Role = UserRole.Admin;
    }
}