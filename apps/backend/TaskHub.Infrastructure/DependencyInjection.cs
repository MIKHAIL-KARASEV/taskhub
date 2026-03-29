namespace TaskHub.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskHub.Application.Abstractions;
using TaskHub.Infrastructure.Persistence;
using TaskHub.Infrastructure.Repositories;
using TaskHub.Infrastructure.Security;
using TaskHub.Infrastructure.Services;
using TaskHub.Application.Auth;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<TaskHubDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default")));

        services.Configure<JwtOptions>(
            configuration.GetSection("Jwt"));

        services.AddHttpContextAccessor();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IUserTaskRepository, UserTaskRepository>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<IDatabaseMigrator, EfDatabaseMigrator>();

        return services;
    }
}