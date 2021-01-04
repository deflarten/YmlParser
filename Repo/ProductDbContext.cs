using Microsoft.EntityFrameworkCore;
using YmlParser.Models;

namespace YmlParser.Repo
{
    public class ProductDbContext : DbContext
    {
        private const string DefaultConnectionString = @"Server=(localdb)\mssqllocaldb;Database=productsdb;Trusted_Connection=True;";

        public ProductDbContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DefaultConnectionString);
        }
    }
}
