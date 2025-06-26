namespace Warehouse.Service.DTO
{
    public class CollectionDto<TPayload>
    {
        public IReadOnlyCollection<TPayload> Payload { get; set; }
        public int TotalNumber { get; set; }
    }
}
