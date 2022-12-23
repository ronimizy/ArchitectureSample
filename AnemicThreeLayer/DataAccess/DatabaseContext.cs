using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public sealed class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<StudentGroup> StudentGroups { get; set; }
    public DbSet<TransferOperation> TransferOperations { get; set; }
    public DbSet<TransferOrder> TransferOrders { get; set; }
    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(builder =>
        {
            builder.HasOne(x => x.Group).WithMany(x => x.Students);
        });

        modelBuilder.Entity<StudentGroup>(builder =>
        {
            builder.HasMany(x => x.Students).WithOne(x => x.Group);
        });

        modelBuilder.Entity<TransferOperation>(builder =>
        {
            builder.HasOne(x => x.Student);
            builder.HasOne(x => x.Group);
            builder.HasOne(x => x.Order).WithMany(x => x.Operations);
        });

        modelBuilder.Entity<TransferOrder>(builder =>
        {
            builder.HasMany(x => x.Operations).WithOne(x => x.Order);
        });

        modelBuilder.Entity<Account>();
    }
}