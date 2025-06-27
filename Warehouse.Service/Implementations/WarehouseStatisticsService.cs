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
            var elements = _componentTypeRepo
                .GetMultipleReadOnly()
                .Include(e => e.Components);

            var sumOfMass = await elements
             .SumAsync(e => e.MassInGrams * e.Components.Count);

            var sumOfValue = await elements
                .SumAsync(e => e.PriceInHungarianForints * e.Components.Count);


            var mostNumerousComponentType = await elements
                .OrderByDescending(g => g.Components.Count)
                .FirstOrDefaultAsync();

            var heaviestComponentType = await elements
                .OrderByDescending(ct => ct.MassInGrams)
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
