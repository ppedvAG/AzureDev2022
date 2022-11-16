using Microsoft.EntityFrameworkCore;
using ProductApi.Model;

namespace ProductApi
{
    public class EfContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public EfContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos("AccountEndpoint=https://mycosmosdb876.documents.azure.com:443/;AccountKey=gSRGRaV8cfoN8b74WZBIjyUVYobni6bwXUuq9P9e1dOq73bFypQHPy5S1IhIRGPPpmWw6K26HsjZACDbR87DJQ==;", "Products");
        }

    }

}
