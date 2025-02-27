using MediatR;
using MoviesRental.Application.Contracts;

namespace MoviesRental.Application.UseCases.Dvds.Commands.RentDvd;
public class RentDvdCommandHandler : IRequestHandler<RentDvdCommand, RentDvdResponse>
{
    private readonly IDvdWriteRepository _repository;

    public async Task<RentDvdResponse> Handle(RentDvdCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
            return default;

        var dvd = await _repository.Get(request.Id);

        if (dvd is null)
            return default;

        dvd.RentCopy();

        var result = await _repository.Update(dvd);

        if (!result)
            return default;

        return new RentDvdResponse(dvd.Id.ToString(), dvd.UpdatedAt);
    }
}
