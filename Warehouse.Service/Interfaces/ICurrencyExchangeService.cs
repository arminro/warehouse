namespace Warehouse.Service.Interfaces
{
    public interface ICurrencyExchangeService
    {
        Task<IDictionary<string, decimal>> GetExchangeRatesAsync();
    }
}