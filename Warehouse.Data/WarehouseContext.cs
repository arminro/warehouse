using Microsoft.EntityFrameworkCore;
using Warehouse.Data.Model;

namespace Warehouse.Data
{
    public class WarehouseContext(DbContextOptions<WarehouseContext> options) : DbContext(options)
    {
        public DbSet<BuildingComponentType> ComponentTypes { get; set; }

        public DbSet<BuildingComponent> Components { get; set; }

        public DbSet<WarehouseStateChange> StateChanges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //we automatically attach a filter to every query so that deleted elements will not be returned
            //we could also attach this as a shadow property, but directly building this into the entitybase allows for a lot less cumbersome use
            // currently, the filter can only be appied to root entities, which is why DbEntry cannot be used here
            // https://learn.microsoft.com/en-us/ef/core/querying/filters#limitations


            //modelBuilder.Entity<BuildingComponentType>()
            //    .Navigation(x => x.Components).AutoInclude();

            modelBuilder.Entity<BuildingComponentType>()
                .HasQueryFilter(bct => !bct.IsDeleted);

            modelBuilder.Entity<BuildingComponent>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<WarehouseStateChange>()
                .HasQueryFilter(e => !e.IsDeleted);
        }

    }
}

