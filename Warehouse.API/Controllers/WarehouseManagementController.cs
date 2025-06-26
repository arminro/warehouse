using Microsoft.AspNetCore.Mvc;
using Warehouse.Data.Model;
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

        [HttpPost("component-type/new")]
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

        [HttpPut("component-type/change")]
        public async Task<BuildingComponentTypeDto> UpdateComponent([FromBody] BuildingComponentTypeDto model)
        {
            return await _componentTypeService
              .UpdateAsync(model);
        }

        [HttpGet("all")]
        public async Task<CollectionDto<BuildingComponentTypeDto>> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return await _componentTypeService
              .GetPaginatedListAsync(pageNumber, pageSize);
        }

        [HttpDelete("component/delete")]
        public async Task SoftDelete([FromQuery] int id)
        {
            await _componentService
              .SoftDeleteAsync(id);
        }

        [HttpDelete("component-type/delete")]
        public async Task SoftDeleteComponentType([FromQuery] int id)
        {
            await _componentTypeService
              .SoftDeleteAsync(id);
        }

        [HttpGet("stat")]
        public async Task<WarehouseStatisticsResponseDto> GetWarehouseStatistics()
        {
            return await _statService
                .GetWarehouseStatisticsAsync();
        }

        [HttpGet("incoming")]
        public async Task<CollectionDto<WarehouseStateChangeResponseDto>> GetWarehouseIncomingTransactions([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return await _stateService
                .GetAllIncomingAsync(pageNumber, pageSize);
        }

        [HttpGet("outgoing")]
        public async Task<CollectionDto<WarehouseStateChangeResponseDto>> GetWarehouseOutgoingTransactions([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return await _stateService
                .GetAllOutgoingAsync(pageNumber, pageSize);
        }
    }
}
