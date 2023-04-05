using STOContracts.BindingModels;
using STOContracts.SearchModels;
using STOContracts.ViewModels;

namespace STOContracts.BusinessLogicsContracts
{
    public interface IWorkLogic
    {
        List<WorkViewModel>? ReadList(WorkSearchModel? model);
        WorkViewModel? ReadElement(WorkSearchModel? model);
        bool Create(WorkBindingModel model);
        bool Update(WorkBindingModel model);
        bool Delete(WorkBindingModel model);
    }
}
