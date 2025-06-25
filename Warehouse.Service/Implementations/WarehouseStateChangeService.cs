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

        public async Task<IReadOnlyCollection<WarehouseStateChangeResponseDto>> GetAllIncomingAsync(int pageNumber = 1, int pageSize = 10)
        {
            var entries = await _repository.GetMultipleReadOnly()
                .Include(sc => sc.Component)
                .Include(sc => sc.ComponentType)
                .Where(sc => sc.Direction == StateChangeDirection.In)
                .Paginate(pageNumber, pageSize)
                .Select(e => _mapper.Map(e))
                .ToListAsync();

            return entries.AsReadOnly();
        }

        public async Task<IReadOnlyCollection<WarehouseStateChangeResponseDto>> GetAllOutgoingAsync(int pageNumber = 1, int pageSize = 10)
        {
            var entries = await _repository.GetMultipleReadOnly()
               .Include(sc => sc.Component)
               .Include(sc => sc.ComponentType)
               .IgnoreQueryFilters() // we are referencing soft deleted elements
               .Where(sc => sc.Direction == StateChangeDirection.Out)
               .Paginate(pageNumber, pageSize)
               .Select(e => _mapper.Map(e))
               .ToListAsync();

            return entries.AsReadOnly();
        }

    }
}
