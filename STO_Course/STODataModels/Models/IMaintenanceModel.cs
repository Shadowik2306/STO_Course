namespace STODataModels.Models
{
    public interface IMaintenanceModel : IId
    {

        double Cost { get; }
        DateTime DateCreate { get; }
    }
}
