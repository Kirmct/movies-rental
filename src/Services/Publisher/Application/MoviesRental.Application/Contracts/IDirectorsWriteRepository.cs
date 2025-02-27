using MoviesRental.Domain.Entities;

namespace MoviesRental.Application.Contracts;
public interface IDirectorsWriteRepository : IWriteRepository<Director>
{
    Task<Director> GetDirectorWithMovies(Guid id);
}
