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
        // 1. Простейшая валидация (Application уровень)
        if (string.IsNullOrWhiteSpace(command.password))
        {
            throw new ArgumentException("Password is required");
        }

        if (command.password.Length < 6)
        {
            throw new ArgumentException("Password too short");
        }

        // 2. Создаём Value Object (Domain уровень)
        var email = Email.Create(command.email);

        // 3. Проверка (UX, не гарантия)
        var exists = await userRepository.ExistsByEmailAsync(email, ct);
        if (exists)
        {
            throw new UserAlreadyExistsException();
        }

        // 4. Хешируем пароль
        var passwordHash = passwordHasher.Hash(command.password);

        // 5. Создаём пользователя (через Domain!)
        var user = User.Create(email, passwordHash);

        // 6. Сохраняем
        await userRepository.AddAsync(user, ct);

        // 7. Возвращаем результат
        return user.Id;
    }
}