using STOContracts.BindingModels;
using STOContracts.SearchModels;
using STOContracts.ViewModels;

namespace STOContracts.BusinessLogicsContracts
{
    public interface IServiceLogic
    {
        List<ServiceViewModel>? ReadList(ServiceSearchModel? model);
        ServiceViewModel? ReadElement(ServiceSearchModel? model);
        bool Create(ServiceBindingModel model);
        bool Update(ServiceBindingModel model);
        bool Delete(ServiceBindingModel model);
    }
}
