using STODataModels.Models;
using System.ComponentModel;

namespace STOContracts.ViewModels
{
    public class SpareViewModel : ISpareModel
    {
        public int Id { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; } = String.Empty;
        [DisplayName("Цена")]
        public double Price { get; set; }
    }
}
