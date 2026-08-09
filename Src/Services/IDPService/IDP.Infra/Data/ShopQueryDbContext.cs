using IDP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IDP.Infra.Data
{
    public class ShopQueryDbContext(IConfiguration _configuration) : DbContext
    {
       
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseSqlServer(_configuration.GetConnectionString("QueryDBConnection"));
        }
        public DbSet<User>  Users { get; set; }
    }
}
