using Warehouse.Data.Infrastructure;

namespace Warehouse.Data.Model
{
    public class BuildingComponent : DbEntity
    {
        public int ComponentTypeId { get; set; }
        public BuildingComponentType ComponentType { get; set; }

        public Guid CatalogNumber { get; set; }
    }
}
