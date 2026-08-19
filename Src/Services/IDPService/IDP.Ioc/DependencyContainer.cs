using IDP.Application.Handlers.Command.User;
using IDP.Domain.IRepositories.Commands;
using IDP.Domain.IRepositories.Commands.Base;
using IDP.Domain.IRepositories.Queries;
using IDP.Infra.Data;
using IDP.Infra.Repositories.Commands;
using IDP.Infra.Repositories.Commands.Base;
using IDP.Infra.Repositories.Queries;
using Mapster;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
 
namespace IDP.Ioc
{
    public static class DependencyContainer
    {
        public static void RegisterService(this WebApplicationBuilder builder)
        {
            #region Redis
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetValue<string>("CacheSetting:RedisUrl");
            });
            #endregion

            #region DbContexts
            builder.Services.AddDbContext<ShopCommandDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ShopCommandConnection")));

            builder.Services.AddDbContext<ShopQueryDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ShopQueryConnection")));
            #endregion
             
            #region MediatR
            builder.Services.AddMediatR(typeof(UserHandler).GetTypeInfo().Assembly);
    //        builder.Services.AddMediatR(cfg =>
    //cfg.RegisterServicesFromAssembly(typeof(UserHandler).Assembly));
            #endregion

            #region Repositories
            builder.Services.AddScoped<IOtpRedisRepository, OtpRedisRepository>();
            builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();
            builder.Services.AddScoped<IUserQueryRepository, UserQueryRepository>();
            builder.Services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
            #endregion

            builder.Services.AddMapster();

            #region Versioning
            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = Asp.Versioning.ApiVersionReader.Combine(
                    new Asp.Versioning.UrlSegmentApiVersionReader(),
                    new Asp.Versioning.HeaderApiVersionReader("X-Api-Version"));
            }).AddMvc()
              .AddApiExplorer(options =>
              {
                  options.GroupNameFormat = "'v'V";
                  options.SubstituteApiVersionInUrl = true;
              });
            #endregion

            //  AddCap  
            //builder.Services.AddCap(options =>
            //{
            //    options.UseEntityFramework<ShopCommandDbContext>();
            //    options.UseDashboard(path => path.PathMatch = "/cap");
            //    options.UseRabbitMQ(options =>
            //    {
            //        options.ConnectionFactoryOptions = options =>
            //        {
            //            options.Ssl.Enabled = false;
            //            options.HostName = "localhost";
            //            options.UserName = "guest";
            //            options.Password = "guest";
            //            options.Port = 5672;
            //        };
            //    });
            //    options.FailedRetryCount = 10;
            //    options.FailedRetryInterval = 5; //second
            //});

            #region MassTransit 
            builder.Services.AddMassTransit(busConfig =>
            {
                busConfig.AddEntityFrameworkOutbox<ShopCommandDbContext>(o =>
                {
                    o.QueryDelay = TimeSpan.FromSeconds(30);
                    o.UseSqlServer();
                    o.UseBusOutbox();
                });
            //    busConfig.AddConsumer<GetEvent>();

                busConfig.SetKebabCaseEndpointNameFormatter();

                // busConfig.AddConsumer<SomeConsumer>();

                busConfig.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(builder.Configuration.GetValue<string>("Rabbit:Host")), h =>
                    {
                        h.Username(builder.Configuration.GetValue<string>("Rabbit:UserName"));
                        h.Password(builder.Configuration.GetValue<string>("Rabbit:Password"));
                    });

                    cfg.UseMessageRetry(r => r.Exponential(
                        10,
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(60),
                        TimeSpan.FromSeconds(10)));

                    cfg.ConfigureEndpoints(context);
                });
            });
            #endregion

            #region JWT Auth
            Auth.Extensions.AddJwt(builder.Services, builder.Configuration);
            #endregion
        }
    }
}
