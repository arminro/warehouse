namespace Warehouse.Service.DTO
{
    public class WarehouseStatisticsResponseDto
    {
        public float TotalMassInGrams { get; set; }

        public decimal TotalValueInHungarianForints { get; set; }

        public decimal TotalValueInEuros { get; set; }

        public BuildingComponentTypeDto ProductWithLargestSum { get; set; }

        public BuildingComponentTypeDto HeaviestProduct { get; set; }
    }
}
