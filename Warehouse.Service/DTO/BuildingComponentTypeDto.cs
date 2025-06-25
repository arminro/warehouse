using System.Diagnostics.CodeAnalysis;

namespace Warehouse.Service.DTO
{
    public class BuildingComponentTypeDto
    {
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public decimal PriceInHungarianForints { get; set; }

        public decimal PriceInEuros { get; set; }

        [NotNull]
        public string Description { get; set; }

        [NotNull]
        public float MassInGrams { get; set; }

        public List<BuildingComponentDto>? Components { get; set; }
    }
}
