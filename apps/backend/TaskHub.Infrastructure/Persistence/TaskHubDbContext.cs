namespace TaskHub.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using TaskHub.Domain.Entities;

public class TaskHubDbContext : DbContext
{
    public TaskHubDbContext(DbContextOptions<TaskHubDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskHubDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}