namespace TaskHub.Application.Users.Register;

using TaskHub.Domain.Entities;
using TaskHub.Domain.ValueObjects;
using TaskHub.Application.Common.Exceptions;
using TaskHub.Application.Abstractions;

public sealed class RegisterUserHandler
{
    private readonly IUserRepository userRepository;
    private readonly IPasswordHasher passwordHasher;

    public RegisterUserHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        this.userRepository = userRepository;
        this.passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(RegisterUserCommand command, CancellationToken ct)
    {
        // 1. Создаём Value Object (Domain validation)
        var email = Email.Create(command.Email);

        // 2. Проверяем существование
        var existingUser = await userRepository.GetByEmailAsync(email, ct);
        if (existingUser is not null)
        {
            throw new UserAlreadyExistsException();
        }

        // 3. Хешируем пароль
        var passwordHash = passwordHasher.Hash(command.Password);

        // 4. Создаём пользователя (Domain)
        var user = User.CreateUser(email, passwordHash);

        // 5. Сохраняем (с защитой от race condition)
        try
        {
            await userRepository.AddAsync(user, ct);
        }
        catch (Exception)
        {
            // позже заменим на конкретный DB exception
            throw new UserAlreadyExistsException();
        }

        return user.Id;
    }
}
