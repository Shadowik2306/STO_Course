using STOContracts.BindingModels;
using STOContracts.SearchModels;
using STOContracts.ViewModels;

namespace STOContracts.StoragesContracts
{
    public interface IWorkStorage
    {
        List<WorkViewModel> GetFullList();
        List<WorkViewModel> GetFilteredList(WorkSearchModel model);
        WorkViewModel? GetElement(WorkSearchModel model);
        WorkViewModel? Insert(WorkBindingModel model);
        WorkViewModel? Update(WorkBindingModel model);
        WorkViewModel? Delete(WorkBindingModel model);
    }
}
