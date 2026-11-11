using CatalogService.Application.Features.Products;
using EventMessages.Events;
using MassTransit;
using MediatR;

namespace CatalogService.Infrastructure.Messaging;

public class OrderPlacedConsumer(ISender sender) : IConsumer<OrderPlacedEvent>
{
    public Task Consume(ConsumeContext<OrderPlacedEvent> context) =>
        sender.Send(new DecreaseStock.Command(
            context.Message.ProductId, context.Message.Quantity), context.CancellationToken);
}