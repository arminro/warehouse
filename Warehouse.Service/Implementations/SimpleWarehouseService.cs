using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Warehouse.Common.Configuration;
using Warehouse.Common.Extensions;
using Warehouse.Data;
using Warehouse.Data.Model;
using Warehouse.Data.Repository.Interfaces;
using Warehouse.Service.DTO;
using Warehouse.Service.Interfaces;

namespace Warehouse.Service.Implementations
{
    public class SimpleWarehouseService(
            IRepository<BuildingComponent> repository,
            IBusinessModelToEntityModelMapper<BuildingComponentDto, BuildingComponent> buildingComponentMapper,
            IRepository<WarehouseStateChange> stateRepository,
            TimeProvider timeProvider,
            EfCoreTransactionProvider transactionProvider,
            IRepository<BuildingComponentType> componentTypeRepository,
            IBusinessModelToEntityModelMapper<BuildingComponentTypeDto, BuildingComponentType> buildingComponentTypeMapper,
            ICurrencyExchangeService currencyExchangeService,
            IOptions<CurrencyExchangeSettings> settings) : ISimpleWarehouseService<BuildingComponentDto>, ISimpleWarehouseService<BuildingComponentTypeDto>
    {
        private readonly IRepository<BuildingComponent> _repository = repository;
        private readonly IRepository<BuildingComponentType> _componentTypeRepository = componentTypeRepository;
        private readonly IBusinessModelToEntityModelMapper<BuildingComponentDto, BuildingComponent> _buildingComponentMapper = buildingComponentMapper;
        private readonly IBusinessModelToEntityModelMapper<BuildingComponentTypeDto, BuildingComponentType> _buildingComponentTypeMapper = buildingComponentTypeMapper;
        private readonly IRepository<WarehouseStateChange> _stateRepository = stateRepository;
        private readonly TimeProvider _timeProvider = timeProvider;
        private readonly EfCoreTransactionProvider _transactionProvider = transactionProvider;
        private readonly ICurrencyExchangeService _currencyExchangeService = currencyExchangeService;
        private readonly string _currentCurrency = settings.Value.CurrentCurrency;

        private const string EXCHANGED_CURRENCY = "EUR";

        public async Task AddAsync(BuildingComponentDto model)
        {
            var entityModel = _buildingComponentMapper.Map(model);
            _repository.Add(entityModel);

            var integrationEvent = WarehouseStateChange
                .ComponentAdded(
                    entityModel,
                    _timeProvider.GetUtcNow());

            _stateRepository.Add(integrationEvent);

            await _transactionProvider.SaveAsync();
        }

        public async Task AddAsync(BuildingComponentTypeDto model)
        {
            var entityModel = _buildingComponentTypeMapper.Map(model);
            _componentTypeRepository.Add(entityModel);

            var integrationEvent = WarehouseStateChange
                .ComponentTypeAdded(
                    entityModel,
                    _timeProvider.GetUtcNow());

            _stateRepository.Add(integrationEvent);

            await _transactionProvider.SaveAsync();
        }


        public async Task<IReadOnlyCollection<BuildingComponentDto>> GetPaginatedListAsync(int pageNumber = 1, int pageSize = 10)
        {
            var entries = await _repository.GetMultipleReadOnly()
                .Include(e => e.ComponentType)
                .Paginate(pageNumber, pageSize)
                .Select(e => _buildingComponentMapper.Map(e))
                .ToListAsync();

            return entries.AsReadOnly();
        }

        async Task<IReadOnlyCollection<BuildingComponentTypeDto>> ISimpleWarehouseService<BuildingComponentTypeDto>.GetPaginatedListAsync(int pageNumber, int pageSize)
        {
            var rates = await _currencyExchangeService
               .GetExchangeRatesAsync();

            var rate = rates[EXCHANGED_CURRENCY];

            var entries = await _componentTypeRepository.GetMultipleReadOnly()
               .Include(ct => ct.Components)
               .Paginate(pageNumber, pageSize)
               .Select(e => _buildingComponentTypeMapper.Map(e))
               .ToListAsync();

            entries.ForEach(e => e.PriceInEuros = e.PriceInHungarianForints * rate);

            return entries.AsReadOnly();
        }

        public async Task<BuildingComponentDto> UpdateAsync(BuildingComponentDto model)
        {
            var entityModel = _buildingComponentMapper.Map(model);
            await _repository.UpdateAsync(entityModel);
            await _transactionProvider.SaveAsync();

            return _buildingComponentMapper.Map(entityModel);
        }

        public async Task<BuildingComponentTypeDto> UpdateAsync(BuildingComponentTypeDto model)
        {
            var entityModel = _buildingComponentTypeMapper.Map(model);
            await _componentTypeRepository.UpdateAsync(entityModel);
            await _transactionProvider.SaveAsync();

            return _buildingComponentTypeMapper.Map(entityModel);
        }

        public async Task SoftDeleteAsync(int id)
        {
            var deletee = await _repository.SoftDeleteAsync(id);

            var integrationEvent = WarehouseStateChange
                .ComponentSoftRemoved(
                    deletee,
                    _timeProvider.GetUtcNow());

            _stateRepository.Add(integrationEvent);
            await _transactionProvider.SaveAsync();
        }

        async Task ISimpleWarehouseService<BuildingComponentTypeDto>.SoftDeleteAsync(int id)
        {
            var deletee = await _componentTypeRepository.SoftDeleteAsync(id);

            var elements = await _repository
                .BulkSoftDelete(deletee.Components.Select(c => c.Id));

            elements.ForEach(e =>
            {
                var integrationEvent = WarehouseStateChange
                    .ComponentSoftRemoved(
                        e,
                        _timeProvider.GetUtcNow());

                _stateRepository.Add(integrationEvent);
            });


            var integrationEvent = WarehouseStateChange
                .ComponentTypeSoftRemoved(
                    deletee,
                    _timeProvider.GetUtcNow());

            _stateRepository.Add(integrationEvent);
            await _transactionProvider.SaveAsync();
        }
    }
}
