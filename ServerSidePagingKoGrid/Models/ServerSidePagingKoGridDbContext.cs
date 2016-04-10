using System.Data.Entity;

namespace ServerSidePagingKoGrid.Models
{
    public class ServerSidePagingKoGridDbContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
