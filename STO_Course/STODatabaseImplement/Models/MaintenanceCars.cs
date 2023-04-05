using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace STODatabaseImplement.Models
{
    public class MaintenanceCars
    {
        public int Id { get; set; }

        [Required]
        public int MaintenanceId { get; set; }

        [Required]
        public int CarId { get; set; }
        [Required]
        public int Count { get; set; }

        public virtual Car Car { get; set; } = new();

        public virtual Maintenance Maintenance { get; set; } = new();
    }
}
