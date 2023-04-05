using STODataModels.Models;

namespace STOContracts.BindingModels
{
    public class ServiceBindingModel : IServiceModel
    {
        public int Id { get; set; }
        public string ServiceDescription { get; set; }=string.Empty;
        public int CarId { get; set; }
    }
}
