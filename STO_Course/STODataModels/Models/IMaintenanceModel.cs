namespace STODataModels.Models
{
    public interface IMaintenanceModel : IId
    {
        int EmployerId { get; }
        double Cost { get; }
        DateTime DateCreate { get; }
        Dictionary<int, (ISpareModel, int)> MaintenanceCars { get; }
    }
}
