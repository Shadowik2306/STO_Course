using STOContracts.BindingModels;
using STOContracts.SearchModels;
using STOContracts.ViewModels;

namespace STOContracts.StoragesContracts
{
    public interface ISpareStorage
    {
        List<SpareViewModel> GetFullList();
        List<SpareViewModel> GetFilteredList(SpareSearchModel model);
        SpareViewModel? GetElement(SpareSearchModel model);
        SpareViewModel? Insert(SpareBindingModel model);
        SpareViewModel? Update(SpareBindingModel model);
        SpareViewModel? Delete(SpareBindingModel model);
    }
}
