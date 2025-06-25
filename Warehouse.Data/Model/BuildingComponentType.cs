using System.ComponentModel.DataAnnotations;
using Warehouse.Data.Infrastructure;

namespace Warehouse.Data.Model
{
    public class BuildingComponentType : DbEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal PriceInHungarianForints { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public float MassInGrams { get; set; }

        public List<BuildingComponent> Components { get; set; }
    }
}
