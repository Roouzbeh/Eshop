using IDP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IDP.Infra.Data
{
    public class ShopCommandDbContext(IConfiguration _configuration):DbContext
    {
         
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseSqlServer(_configuration.GetConnectionString("CommandDBConnection"));
        }
        public DbSet<User>  Users { get; set; }
        public DbSet<Outbox>  Outbox { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}

    }
}
