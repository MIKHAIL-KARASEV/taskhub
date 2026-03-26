namespace TaskHub.Application.Users.Register;

using TaskHub.Domain.Entities;
using TaskHub.Domain.ValueObjects;
using TaskHub.Application.Common.Exceptions;
using TaskHub.Application.Abstractions;

public sealed class RegisterUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(RegisterUserCommand command, CancellationToken ct)
    {
        // 1. Простейшая валидация (Application уровень)
        if (string.IsNullOrWhiteSpace(command.Password))
            throw new ArgumentException("Password is required");

        if (command.Password.Length < 6)
            throw new ArgumentException("Password too short");

        // 2. Создаём Value Object (Domain уровень)
        var email = Email.Create(command.Email);

        // 3. Проверка (UX, не гарантия)
        var exists = await _userRepository.ExistsByEmailAsync(email, ct);
        if (exists)
            throw new UserAlreadyExistsException();

        // 4. Хешируем пароль
        var passwordHash = _passwordHasher.Hash(command.Password);

        // 5. Создаём пользователя (через Domain!)
        var user = User.Create(email, passwordHash);

        // 6. Сохраняем
        await _userRepository.AddAsync(user, ct);

        // 7. Возвращаем результат
        return user.Id;
    }
}