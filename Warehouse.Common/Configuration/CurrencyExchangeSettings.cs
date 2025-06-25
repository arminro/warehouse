namespace Warehouse.Common.Configuration
{
    public class CurrencyExchangeSettings
    {
        public FrankfurterOptions Frankfurter { get; set; }

        public List<string> CurrenciesToPull { get; set; }

        public string CurrentCurrency { get; set; }

        public string CurrencyCacheKey { get; set; }
    }
}
