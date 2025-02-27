using MediatR;

namespace MoviesRental.Query.Application.UseCases.Dvds.Commands.DeleteDvd;
public record DeleteDvdCommand(
    string Id,
    DateTime DeletedAt) : IRequest<bool>;
