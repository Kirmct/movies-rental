using MoviesRental.Core.DomainObjects;

namespace MoviesRental.Application.Contracts;
public interface IWriteRepository<T> where T : Entity
{
    Task<bool> Create(T entity);
    Task<bool> Update(T entity);
    Task<bool> Delete(Guid id);
    Task<T> Get(Guid id);
}
