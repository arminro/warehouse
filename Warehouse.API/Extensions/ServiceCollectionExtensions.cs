using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Warehouse.API.ExceptionHandling;
using Warehouse.Common.Configuration;
using Warehouse.Data;
using Warehouse.Data.Model;
using Warehouse.Data.Repository.Implementations;
using Warehouse.Data.Repository.Interfaces;
using Warehouse.Service.DTO;
using Warehouse.Service.Implementations;
using Warehouse.Service.Interfaces;

namespace Warehouse.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddWarehouseDepenendencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddConfiguration(configuration);
            services.AddWarehouseRepositories();
            services.AddWarehouseServices();
            services.AddSystemDependencies();
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.
                AddDbContext<WarehouseContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("WarehouseDb")));
        }

        public static void AddWarehouseRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));
            services.AddScoped<EfCoreTransactionProvider>();
        }

        public static void AddWarehouseServices(this IServiceCollection services)
        {
            services.AddScoped<ISimpleWarehouseService<BuildingComponentDto>, SimpleWarehouseService>();
            services.AddScoped<ISimpleWarehouseService<BuildingComponentTypeDto>, SimpleWarehouseService>();
            services.AddScoped<IWarehouseStateChangeService, WarehouseStateChangeService>();
            services.AddScoped<IWarehouseStatisticsService, WarehouseStatisticsService>();

            services.AddScoped<IBusinessModelToEntityModelMapper<BuildingComponentDto, BuildingComponent>, WarehouseMapper>();
            services.AddScoped<IBusinessModelToEntityModelMapper<BuildingComponentTypeDto, BuildingComponentType>, WarehouseMapper>();
            services.AddScoped<IBusinessModelToEntityModelMapper<WarehouseStateChangeResponseDto, WarehouseStateChange>, WarehouseMapper>();

            services.AddSingleton<ICurrencyExchangeService, CurrencyExchangeService>();

            services.AddExceptionHandler<WarehouseGlobalExceptionHandler>();
        }

        public static void AddSystemDependencies(this IServiceCollection services)
        {
            services.AddSingleton(TimeProvider.System);
            services.AddHttpClient();
            services.AddMemoryCache();
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // so that we can return parent-child elements
                });
        }

        public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CurrencyExchangeSettings>(
                    configuration.GetSection(nameof(CurrencyExchangeSettings)));

        }
    }
}
