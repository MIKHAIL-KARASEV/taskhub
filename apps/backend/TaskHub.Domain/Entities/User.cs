namespace TaskHub.Domain.Entities;

using TaskHub.Domain.ValueObjects;

public class User
{
    private User(Email email, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            throw new ArgumentException("Password hash cannot be empty");
        }

        Id = Guid.NewGuid();
        Email = email;
        PasswordHash = passwordHash;
    }

    public Guid Id { get; private set; }
    public Email Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public UserRole Role { get; private set; } = UserRole.User;
    private User()
    {
    }

    public static User Create(Email email, string passwordHash)
    {
        return new User(email, passwordHash);
    }
}

public enum UserRole
{
    User,
    Admin
}