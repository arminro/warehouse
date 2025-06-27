using Microsoft.EntityFrameworkCore;
using Warehouse.Common.Extensions;
using Warehouse.Data.Model;
using Warehouse.Data.Repository.Interfaces;
using Warehouse.Service.DTO;
using Warehouse.Service.Interfaces;

namespace Warehouse.Service.Implementations
{
    public class WarehouseStateChangeService(
        IRepository<WarehouseStateChange> repository,
        IBusinessModelToEntityModelMapper<WarehouseStateChangeResponseDto, WarehouseStateChange> mapper) : IWarehouseStateChangeService
    {
        private readonly IRepository<WarehouseStateChange> _repository = repository;
        private readonly IBusinessModelToEntityModelMapper<WarehouseStateChangeResponseDto, WarehouseStateChange> _mapper = mapper;

        public async Task<CollectionDto<WarehouseStateChangeResponseDto>> GetAllIncomingAsync(int pageNumber = 1, int pageSize = 10)
        {
            var query = _repository.GetMultipleReadOnly()
                .Include(sc => sc.Component)
                .Include(sc => sc.ComponentType)
                .IgnoreQueryFilters() // we may reference deleted elements
                .Where(sc => sc.Direction == StateChangeDirection.In);

            var entries = await query
                .Paginate(pageNumber, pageSize)
                .Select(e => _mapper.Map(e))
                .ToListAsync();

            int total = await query
               .CountAsync();

            return new CollectionDto<WarehouseStateChangeResponseDto>()
            {
                Payload = entries.AsReadOnly(),
                TotalNumber = total
            };
        }

        public async Task<CollectionDto<WarehouseStateChangeResponseDto>> GetAllOutgoingAsync(int pageNumber = 1, int pageSize = 10)
        {
            var query = _repository.GetMultipleReadOnly()
               .Include(sc => sc.Component)
               .Include(sc => sc.ComponentType)
               .IgnoreQueryFilters()
               .Where(sc => sc.Direction == StateChangeDirection.Out);

            var entries = await query
               .Paginate(pageNumber, pageSize)
               .Select(e => _mapper.Map(e))
               .ToListAsync();

            int total = await query
              .CountAsync();

            return new CollectionDto<WarehouseStateChangeResponseDto>()
            {
                Payload = entries.AsReadOnly(),
                TotalNumber = total
            };
        }

    }
}
