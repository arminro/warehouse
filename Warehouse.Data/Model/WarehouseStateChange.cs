using Warehouse.Data.Infrastructure;

namespace Warehouse.Data.Model
{
    public partial class WarehouseStateChange : DbEntity
    {
        public DateTimeOffset ChangeTimestamp { get; set; }

        // we store the data as a static snapshot rather than as a reference to the entity
        public BuildingComponent? Component { get; set; }

        public BuildingComponentType? ComponentType { get; set; }

        public Guid ComponentCatalogNumber { get; set; }

        public StateChangeDirection Direction { get; set; }
    }
}
