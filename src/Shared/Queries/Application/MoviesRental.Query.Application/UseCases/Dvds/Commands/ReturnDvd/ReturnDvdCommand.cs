using MediatR;

namespace MoviesRental.Query.Application.UseCases.Dvds.Commands.ReturnDvd;
public record ReturnDvdCommand(
    string Id,
    DateTime UpdatedAt) : IRequest<bool>;
