using MoviesRental.Query.Application.UseCases.Dvds.Queries.GetDvd;

namespace MoviesRental.WebApi.Cache;

public interface ICacheRepository
{
    Task<GetDvdResponse> Get(string title);
    Task Update(GetDvdResponse response);
}
