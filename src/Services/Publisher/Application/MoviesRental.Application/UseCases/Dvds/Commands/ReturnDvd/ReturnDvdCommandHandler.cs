using MediatR;
using MoviesRental.Application.Contracts;

namespace MoviesRental.Application.UseCases.Dvds.Commands.ReturnDvd;
public class ReturnDvdCommandHandler : IRequestHandler<ReturnDvdCommand, ReturnDvdResponse>
{
    private readonly IDvdWriteRepository _repository;

    public ReturnDvdCommandHandler(IDvdWriteRepository repository)
    {
        _repository = repository;
    }

    public async Task<ReturnDvdResponse> Handle(ReturnDvdCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
            return default;

        var dvd = await _repository.Get(request.Id);

        if (dvd is null)
            return default;

        dvd.ReturnCopy();

        var result = await _repository.Update(dvd);

        if (!result)
            return default;

        return new ReturnDvdResponse(dvd.Id.ToString(), dvd.UpdatedAt);
    }
}
