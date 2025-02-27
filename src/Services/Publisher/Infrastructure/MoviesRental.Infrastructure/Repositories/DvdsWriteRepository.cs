using Microsoft.EntityFrameworkCore;
using MoviesRental.Application.Contracts;
using MoviesRental.Domain.Entities;
using MoviesRental.Infrastructure.Context;

namespace MoviesRental.Infrastructure.Repositories;
public class DvdsWriteRepository : IDvdWriteRepository
{
    private readonly MoviesRentalWriteContext _context;

    public DvdsWriteRepository(MoviesRentalWriteContext context)
        => _context = context;

    public async Task<bool> Create(Dvd entity)
    {
        await _context.Dvds.AddAsync(entity);

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(Guid id)
        => await _context.Dvds
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync() > 0;

    public async Task<Dvd> Get(Guid id)
        => await _context.Dvds.FindAsync(id);

    public async Task<bool> Update(Dvd entity)
    {
        _context.Dvds.Update(entity);

        return await _context.SaveChangesAsync() > 0;
    }
}
