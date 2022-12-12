using Domain.Transfers;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.DataAccess;

public interface IDatabaseContext
{
    DbSet<Student> Students { get; }
    DbSet<StudentGroup> StudentGroups { get; }
    DbSet<TransferOperation> TransferOperations { get; }
    DbSet<TransferOrder> TransferOrders { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}