namespace Warehouse.Service.DTO
{
    public class WarehouseStateChangeResponseDto
    {
        public DateTimeOffset ChangeTimestamp { get; set; }

        public object ChangedElement { get; set; }
    }
}
