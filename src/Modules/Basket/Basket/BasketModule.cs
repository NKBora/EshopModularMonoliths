using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data;
using Shared.Data.Interceptors;

namespace Basket;

public static class BasketModule
{
  public static IServiceCollection AddBasketModule(this IServiceCollection services,
      IConfiguration configuration)
  {
    // Api Endpoint services

    // Application Use Case services
    services.AddScoped<IBasketRepository, BasketRepository>();
    services.Decorate<IBasketRepository, CachedBasketRepository>();
    //services.AddScoped<IBasketRepository>(provider =>
    //{
    //  return new CachedBasketRepository(
    //    provider.GetRequiredService<BasketRepository>(),
    //    provider.GetRequiredService<IDistributedCache>());
    //});

    // data - Infrastructure services
    var connectionString = configuration.GetConnectionString("Database");

    services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
    services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

    services.AddDbContext<BasketDbContext>((sp, options) =>
    {
      options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
      options.UseNpgsql(connectionString);

    });



    return services;
  }
  public static IApplicationBuilder UseBasketModule(this IApplicationBuilder app)
  {
    // Use Api Endpoint services

    // Use application Use Case services

    // Usa Data - Infrastructure services
    app.UseMigration<BasketDbContext>();

    return app;
  }

}


