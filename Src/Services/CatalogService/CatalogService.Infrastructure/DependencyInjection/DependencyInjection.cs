using CatalogService.Application.Abstractions;
using CatalogService.Infrastructure.Messaging;
using CatalogService.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<CatalogDbContext>(o =>
            o.UseSqlServer(config.GetConnectionString("Default")));
        services.AddScoped<ICatalogDbContext>(sp => sp.GetRequiredService<CatalogDbContext>());

        services.AddMassTransit(x =>
        {
            x.AddConsumer<OrderPlacedConsumer>();
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("localhost", "/", h => { h.Username("guest"); h.Password("guest"); });
                cfg.ConfigureEndpoints(ctx);
            });
        });

        return services;
    }
}