namespace Warehouse.Service.DTO
{
    public class WarehouseStatisticsResponseDto
    {
        public float TotalMassInGramms { get; set; }

        public decimal TotalValueInForints { get; set; }

        public decimal TotalValueInEuros { get; set; }

        public BuildingComponentTypeDto ProductWithLargestSum { get; set; }

        public BuildingComponentTypeDto HeaviestProduct { get; set; }
    }
}
