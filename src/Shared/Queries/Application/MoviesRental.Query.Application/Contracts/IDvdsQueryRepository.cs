using MoviesRental.Query.Domain.Models;

namespace MoviesRental.Query.Application.Contracts;
public interface IDvdsQueryRepository : IQueryRepository<Dvd>
{
    Task<Dvd> GetByTitle(string title);
}
