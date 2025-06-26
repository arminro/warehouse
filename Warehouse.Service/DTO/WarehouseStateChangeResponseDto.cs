namespace Warehouse.Service.DTO
{
    public class WarehouseStateChangeResponseDto
    {
        public DateTimeOffset ChangeTimestamp { get; set; }

        public BuildingComponentDto? ComponentChanged { get; set; }

        public BuildingComponentTypeDto? ComponentTypeChanged { get; set; }
    }
}
