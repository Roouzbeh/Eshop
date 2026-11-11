using CatalogService.Application.Abstractions;
using CatalogService.Domain.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogService.Application.Features.Products
{
    public static class SeedProduct
    {
        public record Command : IRequest<Result>;
        public record Result(Guid Id, int Stock);

        public sealed class Handler(ICatalogDbContext db) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken ct)
            {
                var product = await db.Products.FirstOrDefaultAsync(ct);
                if (product is null)
                {
                    product = Product.Create("Test", 10);
                    db.Products.Add(product);
                }
                else product.ResetStock(10);

                await db.SaveChangesAsync(ct);
                return new Result(product.Id, product.Stock);
            }
        }
    }
}
