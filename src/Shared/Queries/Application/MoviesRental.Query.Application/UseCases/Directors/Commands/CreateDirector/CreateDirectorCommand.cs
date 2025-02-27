using MediatR;

namespace MoviesRental.Query.Application.UseCases.Directors.Commands.CreateDirector;
public record CreateDirectorCommand(
    string Id,
    string FullName,
    DateTime CreatedAt,
    DateTime UpdatedAt) : IRequest<bool>;
