using MediatR;
using MoviesRental.Query.Application.Contracts;

namespace MoviesRental.Query.Application.UseCases.Dvds.Commands.RentDvd;
public class RentDvdCommandHandler : IRequestHandler<RentDvdCommand, bool>
{
    private readonly IDvdsQueryRepository _repository;

    public RentDvdCommandHandler(IDvdsQueryRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(RentDvdCommand request, CancellationToken cancellationToken)
    {
        if (request.Id is null || request.UpdatedAt > DateTime.Now)
            return false;

        var dvd = await _repository.Get(request.Id);

        if (dvd is null || dvd is { Copies: 0 }) // Pattern Matching - (dvd?.Copies == 0)
            return false;

        dvd.Copies -= 1;

        return await _repository.Update(dvd);
    }
}
