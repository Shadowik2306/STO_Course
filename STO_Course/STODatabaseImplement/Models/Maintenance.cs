using STODataModels.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STODatabaseImplement.Models
{
    public class Maintenance : IMaintenanceModel
    {
        public int Id { get; set; }
        [Required]
        public int EmployerId { get; set; }
        [Required]
        public double Cost { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }

        private Dictionary<int, (ICarModel, int)> _maintenanceCars = null;
        [NotMapped]
        public Dictionary<int, (ICarModel, int)> MaintenanceCars => throw new NotImplementedException();
        
    }
}
