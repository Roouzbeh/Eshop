using Carter;
using EventMessages.Events;
using MassTransit;

namespace OrderService.Features
{
    public class DuplicateTestEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/v1/test/duplicate", async (IPublishEndpoint bus) =>
            {
                var messageId = Guid.NewGuid();
                var evt = new OrderPlacedEvent
                {
                    ProductId = Guid.Parse("آیدی محصول تستی"),
                    Quantity = 2
                };

                await bus.Publish(evt, ctx => ctx.MessageId = messageId);
                await bus.Publish(evt, ctx => ctx.MessageId = messageId);

                return Results.Ok(new { messageId });
            }).WithTags("test");
           
        }
    }
}
