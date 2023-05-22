using STOContracts.BindingModels;
using STOContracts.SearchModels;
using STOContracts.ViewModels;

namespace STOContracts.BusinessLogicsContracts
{
    public interface ICarLogic
    {
        List<CarViewModel>? ReadList(CarSearchModel? model);
        CarViewModel? ReadElement(CarSearchModel? model);
        bool Create(CarBindingModel model);
        bool Update(CarBindingModel model);
        bool Delete(CarBindingModel model);

        void CreateExcelReport(List<CarViewModel> cars);
    }
}
