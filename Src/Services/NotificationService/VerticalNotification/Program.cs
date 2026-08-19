using MassTransit;
using MediatR;
using VerticalNotification.Features.SMS;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Sms).Assembly));  

//test
#region MassTransit
builder.Services.AddMassTransit(busConfig =>
{
    busConfig.AddConsumer<GetEvent>();

    busConfig.SetKebabCaseEndpointNameFormatter();

    busConfig.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri(builder.Configuration.GetValue<string>("Rabbit:Host")), h =>
        {
            h.Username(builder.Configuration.GetValue<string>("Rabbit:UserName"));
            h.Password(builder.Configuration.GetValue<string>("Rabbit:Password"));
        });

        cfg.ConfigureEndpoints(context);
    });
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
