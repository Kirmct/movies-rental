using MediatR;
using MoviesRental.Application.Contracts;

namespace MoviesRental.Application.UseCases.Dvds.Commands.UpdateDvd;
public class UpdateDvdCommandHandler : IRequestHandler<UpdateDvdCommand, UpdateDvdResponse>
{
    private readonly IDvdWriteRepository _repository;
    private readonly UpdateDvdCommandValidator _validator;

    public UpdateDvdCommandHandler(IDvdWriteRepository repository, UpdateDvdCommandValidator validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<UpdateDvdResponse> Handle(UpdateDvdCommand request, CancellationToken cancellationToken)
    {
        var validation = _validator.Validate(request);

        if (!validation.IsValid)
            return default;

        var dvd = await _repository.Get(request.Id);

        if (dvd is null)
            return default;

        dvd.UpdateTitle(request.Title);
        dvd.UpdateCopies(request.Copies);
        dvd.UpdatePublishedDate(request.Published);
        dvd.UpdateGenre(request.Genre);
        dvd.UpdateDirector(request.DirectorId);

        var result = await _repository.Update(dvd);

        if (!result)
            return default;

        return new UpdateDvdResponse(
            dvd.Id.ToString(),
            dvd.Title,
            dvd.Genre.ToString(),
            dvd.Published,
            dvd.DirectorId.ToString(),
            dvd.Copies,
            dvd.UpdatedAt);

    }
}
