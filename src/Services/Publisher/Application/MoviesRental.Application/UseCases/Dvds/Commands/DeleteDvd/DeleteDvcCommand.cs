using MediatR;

namespace MoviesRental.Application.UseCases.Dvds.Commands.DeleteDvd;
public record DeleteDvcCommand(Guid Id) : IRequest<DeleteDvdResponse>;