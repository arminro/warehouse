using System.Diagnostics.CodeAnalysis;

namespace Warehouse.Service.DTO
{
    public class BuildingComponentDto
    {
        [NotNull]
        public int ComponentId { get; set; }

        [NotNull]
        public int ComponentTypeId { get; set; }

        [NotNull]
        public Guid CatalogId { get; set; }
    }
}
