using Application.Exceptions.NotFound;
using Microsoft.EntityFrameworkCore;

namespace Application.Extensions;

public static class DbSetExtensions
{
    public static async Task<T> GetEntityAsync<T>(this DbSet<T> set, Guid id, CancellationToken cancellationToken)
        where T : class
    {
        var entity = await set.FindAsync(new object[] { id }, cancellationToken);

        if (entity is null)
            throw EntityNotFoundException<T>.Create(id);

        return entity;
    }
}