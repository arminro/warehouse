namespace Warehouse.Data.Infrastructure
{
    public interface IDbEntry
    {
        int Id { get; set; }

        bool IsDeleted { get; set; }
    }
}
