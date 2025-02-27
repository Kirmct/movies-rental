using MediatR;
using MoviesRental.Application.Contracts;

namespace MoviesRental.Application.UseCases.Dvds.Commands.DeleteDvd;
public class DeleteDvdCommandHandler : IRequestHandler<DeleteDvcCommand, DeleteDvdResponse>
{
    private readonly IDvdWriteRepository _repository;

    public DeleteDvdCommandHandler(IDvdWriteRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteDvdResponse> Handle(DeleteDvcCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
            return default;

        var dvd = await _repository.Get(request.Id);

        if (dvd is null)
            return default;

        dvd.DeleteDvd();

        var result = await _repository.Update(dvd);

        if (!result)
            return default;

        return new DeleteDvdResponse(dvd.Id.ToString(), (DateTime)dvd.DeletedAt!);
    }
}
