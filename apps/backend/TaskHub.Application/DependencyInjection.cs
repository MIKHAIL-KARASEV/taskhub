namespace TaskHub.Application;

using TaskHub.Application.Users.Login;
using TaskHub.Application.Users.Register;
using TaskHub.Application.UserTasks.Create;
using TaskHub.Application.UserTasks.Complete;
using TaskHub.Application.UserTasks.Uncomplete;
using TaskHub.Application.UserTasks.Delete;
using TaskHub.Application.UserTasks.Update;
using TaskHub.Application.UserTasks.Queries.GetMyTasks;

using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<RegisterUserHandler>();
        services.AddScoped<LoginUserHandler>();
        services.AddScoped<CreateUserTaskHandler>();
        services.AddScoped<UpdateUserTaskHandler>();
        services.AddScoped<CompleteUserTaskHandler>();
        services.AddScoped<UncompleteUserTaskHandler>();
        services.AddScoped<DeleteUserTaskHandler>();
        services.AddScoped<GetMyTasksHandler>();
        // позже добавим:
        // - MediatR
        // - Validators
        // - Pipeline behaviors

        return services;
    }
}