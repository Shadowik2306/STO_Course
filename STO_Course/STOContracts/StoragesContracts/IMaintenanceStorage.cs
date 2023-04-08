using STOContracts.BindingModels;
using STOContracts.SearchModels;
using STOContracts.ViewModels;

namespace STOContracts.StoragesContracts
{
    public interface IMaintenanceStorage
    {
        List<MaintenanceViewModel> GetFullList();
        List<MaintenanceViewModel> GetFilteredList(MaintenanceSearchModel model);
        MaintenanceViewModel? GetElement(MaintenanceSearchModel model);
        MaintenanceViewModel? Insert(MaintenanceBindingModel model);
        MaintenanceViewModel? Update(MaintenanceBindingModel model);
        MaintenanceViewModel? Delete(MaintenanceBindingModel model);
        public List<CarViewModel> GetMaintenaceCars(MaintenanceSearchModel model);
        public List<SpareViewModel> GetCarsSpares(MaintenanceSearchModel model1, CarSearchModel model2);
    }
}
