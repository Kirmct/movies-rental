using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using MoviesRental.Infrastructure.Context;
using MoviesRental.Query.Infrastructure.Settings;

namespace MoviesRental.WebApi.Setup;
public static class ApiConfig
{
    public static IServiceCollection AddApiConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDependencyInjection();
        services.AddDbContext<MoviesRentalWriteContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlConnection"), opt =>
            {
                opt.EnableRetryOnFailure();
            });
        });

        //add cache
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
        });

        //add message bus - mass transit
        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(configuration["EventBusSettings:HostAddress"]);
            });
        });

        //taking the class and put data from appsettings
        services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
        //add MongoDbSettings as a service
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

        services.AddApiVersioning();
        //add health checks
        services.AddHealthChecks()
            .AddRedis(configuration["CacheSettings:ConnectionString"], "Cache HealthCheck", HealthStatus.Degraded)
            .AddMongoDb(configuration["MongoDbSettings:ConnectionString"], "MoviesRentalDb HealthCheck", HealthStatus.Degraded)
            .AddSqlServer(configuration.GetConnectionString("SqlConnection"));

        services.AddExceptionHandler<GlobalExceptionHandler>();
        return services;
    }
}
