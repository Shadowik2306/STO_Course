using STOContracts.BindingModels;
using STOContracts.SearchModels;
using STOContracts.ViewModels;

namespace STOContracts.BusinessLogicsContracts
{
    public interface IStorekeeperLogic
    {
        List<StorekeeperViewModel>? ReadList(StorekeeperSearchModel? model);
        StorekeeperViewModel? ReadElement(StorekeeperSearchModel? model);
        bool Create(StorekeeperBindingModel model);
        bool Update(StorekeeperBindingModel model);
        bool Delete(StorekeeperBindingModel model);
    }
}
