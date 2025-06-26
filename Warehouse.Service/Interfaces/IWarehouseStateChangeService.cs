using Warehouse.Data.Model;
using Warehouse.Service.DTO;

namespace Warehouse.Service.Interfaces
{
    /// <summary>
    /// This service is responsible for managing the state changes of warehouse items.
    /// </summary>
    public interface IWarehouseStateChangeService
    {
        Task<CollectionDto<WarehouseStateChangeResponseDto>> GetAllOutgoingAsync(int pageNumber = 1, int pageSize = 10);

        Task<CollectionDto<WarehouseStateChangeResponseDto>> GetAllIncomingAsync(int pageNumber = 1, int pageSize = 10);
    }
}
