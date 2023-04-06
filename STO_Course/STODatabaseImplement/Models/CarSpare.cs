using System.ComponentModel.DataAnnotations;

namespace STODatabaseImplement.Models
{
    public class CarSpare
    {
        public int Id { get; set; }

        [Required]
        public int SpareId { get; set; }

        [Required]
        public int CarId { get; set; }
        [Required]
        public int Count { get; set; }

        public virtual Car Car { get; set; } = new();

        public virtual Spare Spare { get; set; } = new();
    }
}
