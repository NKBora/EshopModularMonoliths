using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Behaviors;
using Shared.Data.Interceptors;


namespace Catalog;

public static class CatalogModule
{
  public static IServiceCollection AddCatalogModule(this IServiceCollection services,
      IConfiguration configuration)
  {
    // Add services to the container

    // Api Endpoint services

    // Application Use Case services

    // Data - Infrastructure service
    var connectionString = configuration.GetConnectionString("Database");

    services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
    services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
    services.AddDbContext<CatalogDbContext>((sp, options) =>
    {
      options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
      options.UseNpgsql(connectionString);

    });
    services.AddScoped<IDataSeeder, CatalogDataSeeder>();
    return services;
  }

  public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
  {
    // Use Api Endpoint services

    // Use Application Use Case services

    // Use Data - Infrastructure services
    app.UseMigration<CatalogDbContext>();

    return app;
  }
}


