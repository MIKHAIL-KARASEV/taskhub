namespace TaskHub.Domain.Entities;

using TaskHub.Domain.ValueObjects;
using TaskHub.Domain.Enums;
using TaskHub.Domain.Common.Exceptions;

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

    public static User CreateUser(Email email, string passwordHash)
    {
        if (email is null)
        {
            throw new DomainException("Email is required");
        }

        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            throw new DomainException("Password hash is required");
        }

        if (passwordHash.Length < 20)
        {
            throw new DomainException("Invalid password hash");
        }

        return new User(email, passwordHash);
    }

    public void PromoteToAdmin()
    {
        if (Role == UserRole.Admin)
        {
            throw new DomainException("User is already admin");
        }

        Role = UserRole.Admin;
    }
}
