using STODataModels.Models;

namespace STOContracts.BindingModels
{
    public class MaintenanceBindingModel : IMaintenanceModel
    {
        public int Id { get; set; }
        public int EmployerId { get; set; }
        public double Cost { get; set; }
        public DateTime DateCreate { get; set; }
        public Dictionary<int, (ICarModel, int)> MaintenanceCars { get; set; } = new();
    }
}
