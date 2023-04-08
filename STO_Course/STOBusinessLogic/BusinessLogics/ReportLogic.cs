using STOContracts.BindingModels;
using STOContracts.BusinessLogicsContracts;
using STOContracts.SearchModels;
using STOContracts.StoragesContracts;
using STOContracts.ViewModels;

namespace STOBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly IMaintenanceStorage _maintenanceStorage;
        private readonly ISpareStorage _spareStorage;
        private readonly ICarStorage _carStorage;
        public ReportLogic(IMaintenanceStorage maintenanceStorage, ISpareStorage spareStorage, ICarStorage carStorage) 
        { 
            _carStorage = carStorage;
            _maintenanceStorage = maintenanceStorage;
            _spareStorage = spareStorage;
        }
        public List<ReportViewModel> GetCarsAndSpares(ReportBindingModel model)
        {
            return _maintenanceStorage.GetFilteredList(new MaintenanceSearchModel { DateFrom = model.DateFrom, DateTo = model.DateTo })
                .Select(x => new ReportViewModel
                {
                    Cost=x.Cost,
                    Cars=x.MaintenanceCars.Select(x=>(x.Value.Item1.VIN,x.Value.Item2)).ToList(),
                    Spares=_carStorage.GetFilteredList(new CarSearchModel
                    {
                        Id=x.MaintenanceCars
                    })
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
