using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STODatabaseImplement.Models
{
    public class WorkSpare
    {
        public int Id { get; set; }

        [Required]
        public int WorkId { get; set; }

        [Required]
        public int SpareId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual Work Work { get; set; } = new();

        public virtual Spare Spare { get; set; } = new();

    }
}
