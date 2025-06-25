using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Warehouse.Common.Configuration;
using Warehouse.Service.Interfaces;

namespace Warehouse.Service.Implementations
{
    public class CurrencyExchangeService(
        IHttpClientFactory factory,
        IOptions<CurrencyExchangeSettings> settings,
        IMemoryCache cache) : ICurrencyExchangeService
    {
        private readonly IHttpClientFactory _factory = factory;
        private readonly CurrencyExchangeSettings _settings = settings.Value;

        private readonly IMemoryCache _cache = cache;

        public async Task<IDictionary<string, decimal>> GetExchangeRatesAsync()
        {
            if (!_cache.TryGetValue(_settings.CurrencyCacheKey, out IDictionary<string, decimal> exchangeRates))
            {
                var value = await RetrieveExchangeRateAsync(_settings.CurrentCurrency, _settings.CurrenciesToPull);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(5));

                _cache.Set(_settings.CurrencyCacheKey, value, cacheEntryOptions);

                return value;
            }
            return exchangeRates;
        }

        private async Task<IDictionary<string, decimal>> RetrieveExchangeRateAsync(string fromCurrency, List<string> toCurrencies)
        {
            var currencies = string.Join(",", toCurrencies);

            // https://api.frankfurter.dev/v1/latest?base=EUR&symbols=HUF
            var url = $"{_settings.Frankfurter.BaseUrl}?base={fromCurrency}&symbols={currencies}";

            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to fetch exchange rates: {response.ReasonPhrase}");
            }

            var content = await response.Content.ReadAsStringAsync();

            if (content == null)
            {
                throw new HttpRequestException($"Failed to fetch exchange rates: {response.ReasonPhrase}");
            }


            var rates = JsonDocument.Parse(content)
                   .RootElement
                   .GetProperty("rates");

            return toCurrencies.ToDictionary(
                c => c,
                c =>
                {
                    var rate = rates
                        .GetProperty(c);

                    decimal decimalValue = 1;
                    if (rate.TryGetDecimal(out decimalValue) == false)
                    {
                        throw new ApplicationException($"Failed to parse exchange rates!");
                    }

                    return decimalValue;
                });
        }
    }
}
