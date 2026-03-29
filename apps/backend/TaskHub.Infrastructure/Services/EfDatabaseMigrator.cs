namespace TaskHub.Infrastructure.Services;

using TaskHub.Infrastructure.Persistence;
using TaskHub.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

public class EfDatabaseMigrator : IDatabaseMigrator
{
    private readonly TaskHubDbContext _db;

    public EfDatabaseMigrator(TaskHubDbContext db)
    {
        _db = db;
    }

    public async Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        await _db.Database.MigrateAsync(cancellationToken);
    }
}