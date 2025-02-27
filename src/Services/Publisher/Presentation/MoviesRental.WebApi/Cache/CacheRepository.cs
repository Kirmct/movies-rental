using Microsoft.Extensions.Caching.Distributed;
using MoviesRental.Query.Application.UseCases.Dvds.Queries.GetDvd;
using System.Text.Json;

namespace MoviesRental.WebApi.Cache;

public class CacheRepository : ICacheRepository
{
    private readonly IDistributedCache _redisCache;
    private readonly DistributedCacheEntryOptions _cacheEntryOptions;

    public CacheRepository(
        IDistributedCache redisCache)
    {
        _redisCache = redisCache;
        _cacheEntryOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30),
            SlidingExpiration = TimeSpan.FromSeconds(20),
        };
    }

    public async Task<GetDvdResponse> Get(string title)
    {
        var response = await _redisCache.GetStringAsync(title);

        if (response is null)
            return default;

        return JsonSerializer.Deserialize<GetDvdResponse>(response);
    }

    public async Task Update(GetDvdResponse response)
    {
        await _redisCache.SetStringAsync(response.Title, JsonSerializer.Serialize(response), _cacheEntryOptions);
    }
}
