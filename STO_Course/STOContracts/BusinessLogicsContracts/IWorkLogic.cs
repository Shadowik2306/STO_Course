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
        void CreateExcelReport(List<WorkViewModel> works);
        void CreateWordReport(List<WorkViewModel> works);
        void СreateReportPdf(List<WorkViewModel> model, StorekeeperViewModel storekeeper, DateTime to, DateTime from);

    }
}
