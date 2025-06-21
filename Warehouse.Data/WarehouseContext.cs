using Microsoft.EntityFrameworkCore;
using Warehouse.Data.Model;

namespace Warehouse.Data
{
    public class WarehouseContext : DbContext
    {
        public DbSet<BuildingComponent> Components { get; set; }

        public WarehouseContext(DbContextOptions<WarehouseContext> options)
            : base(options)
        {
        }
    }
}
