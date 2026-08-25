using IDP.Application.GrpcService;
using IDP.Ioc;
var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddControllers();//
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();//
builder.Services.AddSwaggerGen();// 
builder.Services.AddGrpc();

builder.RegisterService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.MapGrpcService<UserGrpcService>();
app.Run();
