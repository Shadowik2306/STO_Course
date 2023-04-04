using STODataModels.Models;

namespace STOContracts.BindingModels
{
    public class CarBindingModel : ICarModel
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public Dictionary<int, (ISpareModel, int)> CarSpares { get; set; } = new();

    }
}
