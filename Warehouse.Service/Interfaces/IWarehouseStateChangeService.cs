using Warehouse.Service.DTO;

namespace Warehouse.Service.Interfaces
{
    /// <summary>
    /// This service is responsible for managing the state changes of warehouse items.
    /// </summary>
    public interface IWarehouseStateChangeService
    {
        Task<IReadOnlyCollection<WarehouseStateChangeResponseDto>> GetAllOutgoingAsync(int pageNumber = 1, int pageSize = 10);

        Task<IReadOnlyCollection<WarehouseStateChangeResponseDto>> GetAllIncomingAsync(int pageNumber = 1, int pageSize = 10);
    }
}
