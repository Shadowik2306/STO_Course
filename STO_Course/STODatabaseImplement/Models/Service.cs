using STOContracts.BindingModels;
using STOContracts.ViewModels;
using STODataModels.Models;
using System.ComponentModel.DataAnnotations;

namespace STODatabaseImplement.Models
{
    public class Service : IServiceModel
    {
        public int Id { get; set; }
        [Required]
        public string ServiceDescription { get; set; } = string.Empty;

        public static Service Create(ServiceBindingModel model)
        {
            return new Service()
            {
                Id = model.Id,
                ServiceDescription = model.ServiceDescription,
            };
        }
        public void Update(ServiceBindingModel model)
        {
            ServiceDescription = model.ServiceDescription;
        }
        public ServiceViewModel GetViewModel => new()
        {
            Id = Id,
            ServiceDescription = ServiceDescription,
        };
    }
}
