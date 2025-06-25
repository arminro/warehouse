using Warehouse.Service.DTO;

namespace Warehouse.Service.Interfaces
{
    /// <summary>
    /// Interface for a service that provides warehouse statistics.
    /// </summary>
    /// <typeparam name="TBusinessObject"></typeparam>
    public interface IWarehouseStatisticsService
    {
        Task<WarehouseStatisticsResponseDto> GetWarehouseStatisticsAsync();
    }
}
