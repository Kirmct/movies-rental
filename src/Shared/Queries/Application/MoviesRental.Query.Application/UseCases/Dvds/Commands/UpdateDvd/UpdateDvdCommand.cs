using MediatR;

namespace MoviesRental.Query.Application.UseCases.Dvds.Commands.UpdateDvd;
public record UpdateDvdCommand(
    string Id,
    string Title,
    string Genre,
    DateTime Published,
    int Copies,
    string DirectorId,
    DateTime UpdatedAt): IRequest<bool>;
