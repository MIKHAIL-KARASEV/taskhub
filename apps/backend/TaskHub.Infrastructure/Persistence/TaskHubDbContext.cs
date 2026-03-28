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
    public DbSet<UserTask> UserTasks => Set<UserTask>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskHubDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserTask>(builder =>
                {
                    builder.HasKey(x => x.Id);

                    builder.Property(x => x.Title)
                        .IsRequired()
                        .HasMaxLength(200);

                    builder.Property(x => x.UserId)
                        .IsRequired();
                });
    }
}