using MediatR;

namespace MoviesRental.Query.Application.UseCases.Dvds.Commands.CreateDvd;
public record CreateDvdCommand(
    string Id,
    string Title,
    string Genre,
    DateTime Published,
    bool Available,
    int Copies,
    string DirectorId,
    DateTime CreatedAt,
    DateTime UpdatedAt) : IRequest<bool>;
