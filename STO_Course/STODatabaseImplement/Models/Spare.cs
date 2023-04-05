using STODataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STODatabaseImplement.Models
{
    public class Spare : ISpareModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public double Price { get; set; }

        
    }
}
