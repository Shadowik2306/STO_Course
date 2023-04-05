using STODataModels.Models;
using System.ComponentModel;

namespace STOContracts.ViewModels
{
    public class WorkDurationViewModel : IWorkDurationModel
    {
        public int Id { get; set; }
        [DisplayName("Длительность")]
        public int Duration { get; set; }
    }
}
