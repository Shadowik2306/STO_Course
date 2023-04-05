using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace STODatabaseImplement.Models
{
    public class CarMaintenance
    {
        public int Id { get; set; }

        [Required]
        public int ReinforcedId { get; set; }

        [Required]
        public int ComponentId { get; set; }

        public virtual Car Car { get; set; } = new();

        public virtual Maintenance Maintenance { get; set; } = new();
    }
}
