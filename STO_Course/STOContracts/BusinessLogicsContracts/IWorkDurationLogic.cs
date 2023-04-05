using STOContracts.BindingModels;
using STOContracts.SearchModels;
using STOContracts.ViewModels;

namespace STOContracts.BusinessLogicsContracts
{
    public interface IWorkDurationLogic
    {
        List<WorkDurationViewModel>? ReadList(WorkDurationSearchModel? model);
        WorkDurationViewModel? ReadElement(WorkDurationSearchModel? model);
        bool Create(WorkDurationBindingModel model);
        bool Update(WorkDurationBindingModel model);
        bool Delete(WorkDurationBindingModel model);
    }
}
