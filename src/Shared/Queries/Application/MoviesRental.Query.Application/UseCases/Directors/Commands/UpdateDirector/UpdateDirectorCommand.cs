using MediatR;

namespace MoviesRental.Query.Application.UseCases.Directors.Commands.UpdateDirector;
public record UpdateDirectorCommand(
    string Id,
    string FullName,
    DateTime UpdatedAt): IRequest<bool>;
