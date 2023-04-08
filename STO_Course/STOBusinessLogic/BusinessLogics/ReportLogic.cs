using STOContracts.BindingModels;
using STOContracts.BusinessLogicsContracts;
using STOContracts.SearchModels;
using STOContracts.StoragesContracts;
using STOContracts.ViewModels;
using System.Security.Cryptography.X509Certificates;

namespace STOBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly IWorkStorage _workStorage;
        private readonly ISpareStorage _spareStorage;
        public ReportLogic(IWorkStorage work, ISpareStorage spareStorage) 
        { 
            _workStorage = work;
            _spareStorage = spareStorage;
        }
        public List<ReportViewModel> GetSpares(ReportBindingModel model)
        {
            return _workStorage.GetFilteredList(new WorkSearchModel { DateTo = model.DateTo, DateFrom = model.DateFrom })
             .Select(x => new ReportViewModel
             {
                 Name = x.Title,
                 Spares = x.WorkSpares.Select(x => x.Value.Item1.Name).ToList(),
             }).ToList();
        }

        public void SaveToPdfFile(ReportBindingModel model)
        {
            throw new NotImplementedException();
        }

        public void SaveToExcelFile(ReportBindingModel model)
        {
            throw new NotImplementedException();
        }

        public void SaveToWordFile(ReportBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
