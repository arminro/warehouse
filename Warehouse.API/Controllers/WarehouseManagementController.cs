using Microsoft.AspNetCore.Mvc;
using Warehouse.Service.DTO;
using Warehouse.Service.Interfaces;

namespace Warehouse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseManagementController(
        ISimpleWarehouseService<BuildingComponentDto> componentService,
        IWarehouseStatisticsService statService,
        IWarehouseStateChangeService stateService,
        ISimpleWarehouseService<BuildingComponentTypeDto> componentTypeService) : Controller
    {
        private readonly ISimpleWarehouseService<BuildingComponentDto> _componentService = componentService;
        private readonly ISimpleWarehouseService<BuildingComponentTypeDto> _componentTypeService = componentTypeService;
        private readonly IWarehouseStatisticsService _statService = statService;
        private readonly IWarehouseStateChangeService _stateService = stateService;

        [HttpPost("componenttype/new")]
        public async Task AddComponent([FromBody] BuildingComponentTypeDto model)
        {
            await _componentTypeService
                .AddAsync(model);
        }

        [HttpPost("component/new")]
        public async Task AddComponent([FromBody] BuildingComponentDto model)
        {
            await _componentService
                .AddAsync(model);
        }

        [HttpPut("componenttype/change")]
        public async Task<BuildingComponentTypeDto> UpdateComponent([FromBody] BuildingComponentTypeDto model)
        {
            return await _componentTypeService
              .UpdateAsync(model);
        }

        [HttpGet("all")]
        public async Task<IReadOnlyCollection<BuildingComponentTypeDto>> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return await _componentTypeService
              .GetPaginatedListAsync(pageNumber, pageSize);
        }

        [HttpDelete("delete-component/soft")]
        public async Task SoftDelete([FromQuery] int idToDeletee)
        {
            await _componentService
              .SoftDeleteAsync(idToDeletee);
        }

        [HttpDelete("delete-componenttype/soft")]
        public async Task SoftDeleteComponentType([FromQuery] int idToDeletee)
        {
            await _componentTypeService
              .SoftDeleteAsync(idToDeletee);
        }

        [HttpGet("stat")]
        public async Task<WarehouseStatisticsResponseDto> GetWarehouseStatistics()
        {
            return await _statService
                .GetWarehouseStatisticsAsync();
        }

        [HttpGet("incoming")]
        public async Task<IReadOnlyCollection<WarehouseStateChangeResponseDto>> GetWarehouseIncomingTransactions([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return await _stateService
                .GetAllIncomingAsync(pageNumber, pageSize);
        }

        [HttpGet("outgoing")]
        public async Task<IReadOnlyCollection<WarehouseStateChangeResponseDto>> GetWarehouseOutgoingTransactions([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return await _stateService
                .GetAllOutgoingAsync(pageNumber, pageSize);
        }
    }
}
