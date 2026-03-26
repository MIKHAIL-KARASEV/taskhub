namespace TaskHub.Application.Users.Register;

public sealed record RegisterUserCommand(
    string email,
    string password);