using STOContracts.BindingModels;
using STOContracts.SearchModels;
using STOContracts.ViewModels;

namespace STOContracts.StoragesContracts
{
    public interface IStorekeeperStorage
    {
        List<StorekeeperViewModel> GetFullList();
        List<StorekeeperViewModel> GetFilteredList(StorekeeperSearchModel model);
        StorekeeperViewModel? GetElement(StorekeeperSearchModel model);
        StorekeeperViewModel? Insert(StorekeeperBindingModel model);
        StorekeeperViewModel? Update(StorekeeperBindingModel model);
        StorekeeperViewModel? Delete(StorekeeperBindingModel model);
    }
}
