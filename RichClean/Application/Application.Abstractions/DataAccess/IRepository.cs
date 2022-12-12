namespace Application.Abstractions.DataAccess;

public interface IRepository<T> : IQueryable<T>
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}