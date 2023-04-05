using STODataModels.Models;

namespace STOContracts.BindingModels
{
    public class SpareBindingModel : ISpareModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public double Price { get; set; }
    }
}
