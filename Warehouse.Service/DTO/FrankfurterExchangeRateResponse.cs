using System.Dynamic;

namespace Warehouse.Service.DTO
{
    public class FrankfurterExchangeRateResponse
    {
        public float Amount { get; set; }
        public string Base { get; set; }
        public string Date { get; set; }
        public ExpandoObject Rates { get; set; } // Frankfurter does not have a well-behaving API
    }
}

