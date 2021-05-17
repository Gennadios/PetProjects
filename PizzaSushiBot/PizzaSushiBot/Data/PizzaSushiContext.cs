using PizzaSushiBot.Models;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace PizzaSushiBot.Data
{
    sealed class PizzaSushiContext : DbContext
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["PizzaSushiDB"].ConnectionString;

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
