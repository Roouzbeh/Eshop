using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Grpc;
using static OrderService.Features.Order.AddOrder;

namespace OrderService.Features.Order
{
    public static class AddOrder
    {

        public class Command : IRequest<bool>
        {
            public string OrderId { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }
        public sealed class Handler(UserGrpc.UserGrpcClient _userClient) : IRequestHandler<Command, bool>
        {
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {

                    var user = await _userClient.GetUserByIdAsync(new GetUserByIdRequest { Id = 551215 });
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); // doesn't throw or return → compiler error

                }
                return false;
            }
        }

    }
    public class AddOrderEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/getuserdetail", async (  ISender sender) =>
            
            {
                var result = await sender.Send(new AddOrder.Command());
                return result ? Results.Ok(result) : Results.BadRequest();
            }).WithTags("order");
        }
    }
}
