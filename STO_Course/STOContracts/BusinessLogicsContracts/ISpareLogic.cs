using STOContracts.BindingModels;
using STOContracts.SearchModels;
using STOContracts.ViewModels;

namespace STOContracts.BusinessLogicsContracts
{
    public interface ISpareLogic
    {
        List<SpareViewModel>? ReadList(SpareSearchModel? model);
        SpareViewModel? ReadElement(SpareSearchModel? model);
        bool Create(SpareBindingModel model);
        bool Update(SpareBindingModel model);
        bool Delete(SpareBindingModel model);
    }
}
