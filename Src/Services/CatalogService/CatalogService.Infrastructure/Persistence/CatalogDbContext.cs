using CatalogService.Application.Abstractions;
using CatalogService.Domain.Product;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Persistence
{
    public class CatalogDbContext(DbContextOptions<CatalogDbContext> options)
    : DbContext(options), ICatalogDbContext
    {
        public DbSet<Product> Products => Set<Product>();
    }
}
