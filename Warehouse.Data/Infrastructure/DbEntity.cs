using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Data.Infrastructure
{
    public abstract class DbEntity : IDbEntry
    {
        // separating infrastructure-reated code this way better allows for generating entity classes
        // those can focus on the actual model data, while everything required of the db is stored in a separate file
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
