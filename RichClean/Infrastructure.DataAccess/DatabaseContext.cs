using Application.Abstractions.DataAccess;
using Domain.Transfers;
using Domain.Users;
using Infrastructure.DataAccess.ValueConverters;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess;

internal class DatabaseContext : DbContext, IDatabaseContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Student> Students { get; private init; } = null!;
    public DbSet<StudentGroup> StudentGroups { get; private init; } = null!;
    public DbSet<TransferOperation> TransferOperations { get; private init; } = null!;
    public DbSet<TransferOrder> TransferOrders { get; private init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<StudentCount>().HaveConversion<StudentCountConverter>();
    }
}