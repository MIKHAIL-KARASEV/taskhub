namespace TaskHub.Application;

using TaskHub.Application.Users.Login;
using TaskHub.Application.Users.Register;

using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<RegisterUserHandler>();
        services.AddScoped<LoginUserHandler>();
        // позже добавим:
        // - MediatR
        // - Validators
        // - Pipeline behaviors

        return services;
    }
}