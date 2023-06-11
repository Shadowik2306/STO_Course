using STOContracts.BindingModels;
using STOContracts.SearchModels;
using STOContracts.ViewModels;

namespace STOContracts.BusinessLogicsContracts
{
    public interface IEmployerLogic
    {
        List<EmployerViewModel>? ReadList(EmployerSearchModel? model);
        EmployerViewModel? ReadElement(EmployerSearchModel? model);
        bool Create(EmployerBindingModel model);
        bool Update(EmployerBindingModel model);
        bool Delete(EmployerBindingModel model);

       void Emulation();
    }
}
