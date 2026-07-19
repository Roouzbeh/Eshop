using IDP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IDP.Infra.Data
{
    public class ShopDbContext(IConfiguration _configuration):DbContext
    {
         
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_configuration.GetConnectionString("CommandDbConnection"));
        }

        public DbSet<User> Users { get; set; }
    }
}
