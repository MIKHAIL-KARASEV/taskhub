namespace TaskHub.Application;

using TaskHub.Application.Users.Register;

using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<RegisterUserHandler>();
        // позже добавим:
        // - MediatR
        // - Validators
        // - Pipeline behaviors

        return services;
    }
}