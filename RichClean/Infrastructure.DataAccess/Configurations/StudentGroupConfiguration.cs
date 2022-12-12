using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configurations;

public class StudentGroupConfiguration : IEntityTypeConfiguration<StudentGroup>
{
    public void Configure(EntityTypeBuilder<StudentGroup> builder)
    {
        builder.Navigation(x => x.Students).HasField("_students").UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Property(x => x.StudentLimit).HasField("_studentLimit");
    }
}