using STOContracts.BindingModels;
using STOContracts.SearchModels;
using STOContracts.ViewModels;

namespace STOContracts.StoragesContracts
{
    public interface IEmployerStorage
    {
        List<EmployerViewModel> GetFullList();
        List<EmployerViewModel> GetFilteredList(EmployerSearchModel model);
        EmployerViewModel? GetElement(EmployerSearchModel model);
        EmployerViewModel? Insert(EmployerBindingModel model);
        EmployerViewModel? Update(EmployerBindingModel model);
        EmployerViewModel? Delete(EmployerBindingModel model);
    }
}
