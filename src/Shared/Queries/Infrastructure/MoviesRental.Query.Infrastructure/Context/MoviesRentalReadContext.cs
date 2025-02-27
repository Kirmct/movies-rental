using MongoDB.Driver;
using MoviesRental.Query.Domain.Models;
using MoviesRental.Query.Infrastructure.Settings;

namespace MoviesRental.Query.Infrastructure.Context;
public class MoviesRentalReadContext : IMoviesRentalReadContext
{
    public MoviesRentalReadContext(MongoDbSettings settings)
    {
        var cliente = new MongoClient(settings.ConnectionString);
        var database = cliente.GetDatabase(settings.DatabaseName);
        Directors = database.GetCollection<Director>(settings.DirectorsCollection);
        Dvds = database.GetCollection<Dvd>(settings.DvdsCollection);
    }
    public IMongoCollection<Dvd> Dvds { get; }

    public IMongoCollection<Director> Directors { get; }
}
