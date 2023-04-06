using STODataModels.Models;
using System.ComponentModel;

namespace STOContracts.ViewModels
{
    public class MaintenanceViewModel : IMaintenanceModel
    {
        public int Id { get; set; }
        public int EmployerId { get; set; }
        [DisplayName("Стоимость")]
        public double Cost { get; set; }
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
        public Dictionary<int, (ICarModel, int)> MaintenanceCars { get; set; } = new();
    }
}
