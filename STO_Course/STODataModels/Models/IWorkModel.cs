namespace STODataModels.Models
{
    public interface IWorkModel : IId
    {
        string Title { get; }

        double Price { get; }

        int StorekeeperId { get; }

        int DurationId { get; }

        DateTime Date { get; }

        Dictionary<int, (ISpareModel, int)> WorkSpares { get; }
        Dictionary<int, (IMaintenanceModel, int)> WorkMaintenances { get; }
    }
}
