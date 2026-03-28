namespace TaskHub.Application.Users.Login;

using TaskHub.Application.Abstractions;
using TaskHub.Domain.ValueObjects;

public class LoginUserHandler
{
    private readonly IUserRepository userRepository;
    private readonly IPasswordHasher passwordHasher;
    private readonly IJwtProvider jwtProvider;

    public LoginUserHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
    {
        this.userRepository = userRepository;
        this.passwordHasher = passwordHasher;
        this.jwtProvider = jwtProvider;
    }

    public async Task<string> Handle(LoginUserCommand command, CancellationToken ct)
    {
        var email = Email.Create(command.Email);

        var user = await userRepository.GetByEmailAsync(email, ct);

        if (user is null)
            throw new Exception("Invalid credentials");

        var passwordHash = passwordHasher.Hash(command.Password);

        if (user.PasswordHash != passwordHash)
            throw new Exception("Invalid credentials");

        return jwtProvider.GenerateToken(user.Id, user.Email.Value);
    }
}