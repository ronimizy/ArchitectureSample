using Domain.Transfers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configurations;

public class TransferOperationConfiguration : IEntityTypeConfiguration<TransferOperation>
{
    public void Configure(EntityTypeBuilder<TransferOperation> builder)
    {
        builder.HasOne(x => x.Order);
        builder.HasOne(x => x.Group);
        builder.HasOne(x => x.Student);
    }
}