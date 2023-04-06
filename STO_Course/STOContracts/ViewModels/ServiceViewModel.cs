using STODataModels.Models;
using System.ComponentModel;

namespace STOContracts.ViewModels
{
    public class ServiceViewModel : IServiceModel
    {
        public int Id { get; set; }
        [DisplayName("Описание сервиса")]
        public string ServiceDescription { get; set; } = string.Empty;
    }
}
