using MediatR;
using MoviesRental.Application.Contracts;
using MoviesRental.Domain.Entities;

namespace MoviesRental.Application.UseCases.Dvds.Commands.CreateDvd;
public class CreateDvdCommandHandler : IRequestHandler<CreateDvdCommand, CreateDvdResponse>
{
    private readonly IDvdWriteRepository _repository;
    private readonly CreateDvdCommandValidator _validator;

    public CreateDvdCommandHandler(IDvdWriteRepository repository, CreateDvdCommandValidator validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<CreateDvdResponse> Handle(CreateDvdCommand request, CancellationToken cancellationToken)
    {
        var valid = _validator.Validate(request);

        if (!valid.IsValid)
            return default;

        var dvd = new Dvd(request.Title, request.Genre, request.Published, request.Copies, request.DirectorId);

        var result = await _repository.Create(dvd);

        if (!result)
            return default;

        return new CreateDvdResponse(
            dvd.Id.ToString(),
            dvd.Title,
            dvd.Genre.ToString(),
            dvd.Published,
            dvd.Available,
            dvd.Copies,
            dvd.DirectorId.ToString(),
            dvd.CreatedAt,
            dvd.UpdatedAt);
    }
}
