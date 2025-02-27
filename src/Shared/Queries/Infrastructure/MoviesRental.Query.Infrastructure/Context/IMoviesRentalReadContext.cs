using MongoDB.Driver;
using MoviesRental.Query.Domain.Models;

namespace MoviesRental.Query.Infrastructure.Context;
public interface IMoviesRentalReadContext
{
    IMongoCollection<Dvd> Dvds { get; }
    IMongoCollection<Director> Directors { get; }
}
