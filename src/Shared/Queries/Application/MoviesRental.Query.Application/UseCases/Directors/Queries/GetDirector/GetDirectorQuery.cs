using MediatR;

namespace MoviesRental.Query.Application.UseCases.Directors.Queries.GetDirector;
public record GetDirectorQuery(
    string FullName) : IRequest<GetDirectorResponse>;
