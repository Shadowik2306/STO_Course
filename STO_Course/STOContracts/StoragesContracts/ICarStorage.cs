using STOContracts.BindingModels;
using STOContracts.SearchModels;
using STOContracts.ViewModels;

namespace STOContracts.StoragesContracts
{
    public interface ICarStorage
    {
        List<CarViewModel> GetFullList();
        List<CarViewModel> GetFilteredList(CarSearchModel model);
        CarViewModel? GetElement(CarSearchModel model);
        CarViewModel? Insert(CarBindingModel model);
        CarViewModel? Update(CarBindingModel model);
        CarViewModel? Delete(CarBindingModel model);

        public List<SpareViewModel>? GetSpares(CarSearchModel model);
    }
}
