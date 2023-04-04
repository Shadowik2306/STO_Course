using STODataModels.Models;
using System.ComponentModel;

namespace STOContracts.ViewModels
{
    public class CarViewModel : ICarModel
    {
        public int Id { get; set; }
        [DisplayName("Название бренда")]
        public string Brand { get; set; } = string.Empty;
        [DisplayName("Название модели")]
        public string Model { get; set; } = string.Empty;
        public Dictionary<int, (ISpareModel, int)> CarSpares { get; set; } = new();
    }
}
