using System.ComponentModel.DataAnnotations;
using Warehouse.Data.Infrastructure;

namespace Warehouse.Data.Model
{
    public class BuildingComponent : DbEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int PriceInHungarianForints { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public float MassInGrams { get; set; }
    }
}
