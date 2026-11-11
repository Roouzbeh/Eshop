using CatalogService.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogService.Application.Features.Products
{
    public static class DecreaseStock
    {

        public record Command(Guid ProductId, int Quantity) : IRequest;

        public sealed class Handler(ICatalogDbContext db) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken ct)
            {
                var product = await db.Products.FindAsync([request.ProductId], ct)
                    ?? throw new KeyNotFoundException($"Product {request.ProductId} not found");

                product.DecreaseStock(request.Quantity);
                await db.SaveChangesAsync(ct);
            }
        }
    }
}
