using MoviesRental.Query.Domain.Models;

namespace MoviesRental.Query.Application.Contracts;
public interface IDirectorsQueryRepository : IQueryRepository<Director>
{
    Task<Director> GetByName(string name);
}
