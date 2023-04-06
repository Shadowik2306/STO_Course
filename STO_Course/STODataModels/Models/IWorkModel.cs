namespace STODataModels.Models
{
    public interface IWorkModel : IId
    {
        string Title { get; }

        double Price { get; }

        int StorekeeperId { get; }

        int DurationId { get; }

        Dictionary<int, (ISpareModel, int)> WorkSpares { get; }
        Dictionary<int, (IMaintenanceModel, int)> WorkMaintences { get; }
    }
}
