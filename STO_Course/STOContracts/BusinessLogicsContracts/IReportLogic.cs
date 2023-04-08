using STOContracts.BindingModels;
using STOContracts.ViewModels;

namespace STOContracts.BusinessLogicsContracts
{
    public interface IReportLogic
    {
        List<ReportViewModel> GetSpares(ReportBindingModel model);
        void SaveToWordFile(ReportBindingModel model);
        void SaveToExcelFile(ReportBindingModel model);
        void SaveToPdfFile(ReportBindingModel model);
    }
}
