using Domain.Users;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.DataAccess.ValueConverters;

public class StudentCountConverter : ValueConverter<StudentCount, int>
{
    public StudentCountConverter() : base(x => x.Value, x => new StudentCount(x)) { }
}