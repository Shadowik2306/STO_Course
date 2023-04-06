using STODataModels.Models;

namespace STOContracts.BindingModels
{
    public class WorkBindingModel : IWorkModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = String.Empty;

        public double Price { get; set; }

        public int StorekeeperId { get; set; }

        public Dictionary<int, (ISpareModel, int)> WorkSpares { get; set; } = new();

        public Dictionary<int, (IMaintenanceModel, int)> WorkMaintences { get; set; } = new();
    }
}
