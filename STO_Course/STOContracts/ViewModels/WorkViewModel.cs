using STODataModels.Models;
using System.ComponentModel;

namespace STOContracts.ViewModels
{
    public class WorkViewModel : IWorkModel
    {
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Title { get; set; } = String.Empty;
        [DisplayName("Цена")]
        public double Price { get; set; }
        public int DurationId { get; set; }
        public int Duration { get; set; }
        public int StorekeeperId { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public Dictionary<int, (ISpareModel, int)> WorkSpares { get; set; } = new();

        public Dictionary<int, (IMaintenanceModel, int)> WorkMaintenances { get; set; } = new();

        
    }
}
