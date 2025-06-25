namespace Warehouse.Data.Model
{
    public partial class WarehouseStateChange
    {
        public static WarehouseStateChange ComponentAdded(BuildingComponent component, DateTimeOffset now)
            => new()
            {
                Component = component,
                Direction = StateChangeDirection.In,
                ChangeTimestamp = now,
                ComponentCatalogNumber = component.CatalogNumber
            };

        public static WarehouseStateChange ComponentSoftRemoved(BuildingComponent component, DateTimeOffset now)
           => new()
           {
               Component = component,
               Direction = StateChangeDirection.Out,
               ChangeTimestamp = now,
               ComponentCatalogNumber = component.CatalogNumber
           };
        public static WarehouseStateChange ComponentTypeAdded(BuildingComponentType type, DateTimeOffset now)
            => new()
            {
                ComponentType = type,
                Direction = StateChangeDirection.In,
                ChangeTimestamp = now
            };

        public static WarehouseStateChange ComponentTypeSoftRemoved(BuildingComponentType type, DateTimeOffset now)
           => new()
           {
               ComponentType = type,
               Direction = StateChangeDirection.Out,
               ChangeTimestamp = now
           };
    }
}
