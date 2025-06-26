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
        IBusinessModelToEntityModelMapper<BuildingComponentTypeDto, BuildingComponentType> componentTypeMapper,
        ICurrencyExchangeService currencyExchangeService) : IWarehouseStatisticsService
    {
        private readonly IRepository<BuildingComponent> _componentRepo = componentRepo;
        private readonly IRepository<BuildingComponentType> _componentTypeRepo = componentTypeRepo;
        private readonly IBusinessModelToEntityModelMapper<BuildingComponentTypeDto, BuildingComponentType> _componentTypeMapper = componentTypeMapper;
        private readonly ICurrencyExchangeService _currencyExchangeService = currencyExchangeService;

        private const string EXCHANGED_CURRENCY = "EUR";

        public async Task<WarehouseStatisticsResponseDto> GetWarehouseStatisticsAsync()
        {
            var elements = _componentRepo
             .GetMultipleReadOnly()
             .Include(e => e.ComponentType);

            var sumOfMass = await elements
             .SumAsync(e => e.ComponentType.MassInGrams);

            var sumOfValue = await elements
                .SumAsync(e => e.ComponentType.PriceInHungarianForints * e.ComponentType.Components.Count());

            var componentTypeGrouping = elements
                .GroupBy(e => e.ComponentType);


            var mostNumerousComponentType = await componentTypeGrouping
                .Select(g => new { g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .Select(g => g.Key)
                .FirstOrDefaultAsync();


            var heaviestComponentType = await componentTypeGrouping
                .OrderByDescending(ct => ct.Key.MassInGrams)
                .Select(ct => ct.Key)
                .FirstOrDefaultAsync();



            var rates = await _currencyExchangeService
              .GetExchangeRatesAsync();

            var rate = rates[EXCHANGED_CURRENCY];

            var heaviest = _componentTypeMapper.Map(heaviestComponentType);
            heaviest.PriceInEuros = heaviest.PriceInHungarianForints * rate;

            var numerous = _componentTypeMapper.Map(mostNumerousComponentType);
            numerous.PriceInEuros = numerous.PriceInHungarianForints * rate;


            return new WarehouseStatisticsResponseDto
            {
                TotalMassInGrams = sumOfMass,
                HeaviestProduct = heaviest,
                TotalValueInHungarianForints = sumOfValue,
                TotalValueInEuros = sumOfValue * rate,
                ProductWithLargestSum = numerous,
            };
        }
    }
}
