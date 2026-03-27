namespace TaskHub.Application.Users.Register;

public sealed class RegisterUserCommand
{
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
}