using Microsoft.EntityFrameworkCore;
using Warehouse.Data.Model;
using Warehouse.Data.Repository.Interfaces;
using Warehouse.Service.DTO;
using Warehouse.Service.Interfaces;

namespace Warehouse.Service.Implementations
{
    public class WarehouseStatisticsService(
        IRepository<BuildingComponent> componentRepo,
        IRepository<BuildingComponentType> componentTypeRepo,
        IBusinessModelToEntityModelMapper<BuildingComponentTypeDto, BuildingComponentType> componentTypeMapper) : IWarehouseStatisticsService
    {
        private readonly IRepository<BuildingComponent> _componentRepo = componentRepo;
        private readonly IRepository<BuildingComponentType> _componentTypeRepo = componentTypeRepo;
        private readonly IBusinessModelToEntityModelMapper<BuildingComponentTypeDto, BuildingComponentType> _componentTypeMapper = componentTypeMapper;

        public async Task<WarehouseStatisticsResponseDto> GetWarehouseStatisticsAsync()
        {
            var elements = _componentRepo
             .GetMultipleReadOnly()
             .Include(e => e.ComponentType);

            var sumOfMass = await elements
             .SumAsync(e => e.ComponentType.MassInGrams);

            var sumOfValue = await elements
                .SumAsync(e => e.ComponentType.PriceInHungarianForints);

            var componentTypeGrouping = elements
                .GroupBy(e => e.ComponentType);


            var mostNumberousComponentType = await componentTypeGrouping
                .Select(g => new { g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .Select(g => g.Key)
                .FirstOrDefaultAsync();


            var heaviestComponentType = await componentTypeGrouping
                .OrderByDescending(ct => ct.Key.MassInGrams)
                .Select(ct => ct.Key)
                .FirstOrDefaultAsync();

            return new WarehouseStatisticsResponseDto
            {
                TotalMassInGramms = sumOfMass,
                HeaviestProduct = _componentTypeMapper.Map(heaviestComponentType),
                TotalValueInForints = sumOfValue,
                ProductWithLargestSum = _componentTypeMapper.Map(mostNumberousComponentType),
            };
        }
    }
}
