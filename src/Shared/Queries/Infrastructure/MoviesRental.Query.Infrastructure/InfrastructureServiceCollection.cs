using Microsoft.Extensions.DependencyInjection;
using MoviesRental.Query.Application.Contracts;
using MoviesRental.Query.Infrastructure.Context;
using MoviesRental.Query.Infrastructure.Repositories;

namespace MoviesRental.Query.Infrastructure;
public static class InfrastructureServiceCollection
{
    public static IServiceCollection AddReadInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IMoviesRentalReadContext, MoviesRentalReadContext>();
        services.AddScoped<IDirectorsQueryRepository, DirectorsQueryRepository>();
        services.AddScoped<IDvdsQueryRepository, DvdsQueryRepository>();

        return services;
    }
}
