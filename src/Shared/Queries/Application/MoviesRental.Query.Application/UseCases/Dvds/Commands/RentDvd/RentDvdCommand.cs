using MediatR;

namespace MoviesRental.Query.Application.UseCases.Dvds.Commands.RentDvd;
public record RentDvdCommand(
   string Id,
   DateTime UpdatedAt) : IRequest<bool>;
