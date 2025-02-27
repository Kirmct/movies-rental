using MediatR;

namespace MoviesRental.Query.Application.UseCases.Directors.Commands.DeleteDirector;
public record DeleteDirectorCommand(string Id) : IRequest<bool>;
