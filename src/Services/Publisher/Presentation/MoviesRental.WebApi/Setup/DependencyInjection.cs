using MoviesRental.Application;
using MoviesRental.Infrastructure;
using MoviesRental.Query.Application;
using MoviesRental.Query.Infrastructure;

namespace MoviesRental.WebApi.Setup;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddWriteApplication();
        services.AddWriteInfrastructure();
        services.AddReadApplication();
        services.AddReadInfrastructure();
        //services.AddScoped<ICacheRepository, CacheRepository>();

        return services;
    }
}
