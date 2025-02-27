using MoviesRental.Query.Application.Contracts;
using MoviesRental.Query.Domain.Models;
using MoviesRental.Query.Infrastructure.Context;
using MongoDB.Driver;

namespace MoviesRental.Query.Infrastructure.Repositories;
partial class DirectorsQueryRepository : IDirectorsQueryRepository
{
    private readonly IMoviesRentalReadContext _context;

    public DirectorsQueryRepository(IMoviesRentalReadContext context)
    {
        _context = context;
    }

    public async Task<Director> Create(Director entity)
    {
        await _context
            .Directors
            .InsertOneAsync(entity);

        return entity;
    }

    public async Task<bool> Delete(string id)
    {
        var result = await _context
            .Directors
            .DeleteOneAsync(x => x.Id == id);

        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public async Task<Director> Get(string id)
        => await _context
            .Directors
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync();

    public async Task<Director> GetByName(string name)
        => await _context
            .Directors
            .Find(x => x.FullName == name)
            .FirstOrDefaultAsync();

    public async Task<bool> Update(Director entity)
    {
        var result = await _context
                        .Directors
                        .ReplaceOneAsync(x => x.Id == entity.Id, entity);

        return result.IsAcknowledged && result.ModifiedCount > 0;
    }
}
