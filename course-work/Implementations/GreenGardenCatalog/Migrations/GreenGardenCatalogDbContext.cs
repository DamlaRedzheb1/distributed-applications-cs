using Microsoft.EntityFrameworkCore;
using GreenGardenCatalog.Entities;

namespace GreenGardenCatalog.Migrations
{
    public class GreenGardenCatalogDbContext : DbContext
    {
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Soil> Soils { get; set; }
        public DbSet<Fertilizer> Fertilizers { get; set; }


        public GreenGardenCatalogDbContext(DbContextOptions<GreenGardenCatalogDbContext> options) : base(options) { }
    }
}
