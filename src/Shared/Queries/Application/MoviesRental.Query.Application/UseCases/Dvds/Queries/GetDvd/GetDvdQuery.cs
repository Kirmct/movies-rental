using MediatR;

namespace MoviesRental.Query.Application.UseCases.Dvds.Queries.GetDvd;
public record GetDvdQuery(
    string Title): IRequest<GetDvdResponse>;
