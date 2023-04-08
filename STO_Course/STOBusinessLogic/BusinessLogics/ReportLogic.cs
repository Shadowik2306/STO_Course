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
            var maintenances = _maintenanceStorage.GetFilteredList(new MaintenanceSearchModel { DateFrom = model.DateFrom, DateTo = model.DateTo });
            var result = new List<ReportViewModel>();
            foreach (var maintenance in maintenances) {
                var cars = _maintenanceStorage.GetMaintenaceCars(new MaintenanceSearchModel { Id = maintenance.Id }).ToList();
                List<String> cars_spares = new List<String>();
                foreach (var car in cars)
                {
                    var spare = _maintenanceStorage.GetCarsSpares(new MaintenanceSearchModel { Id = maintenance.Id }, new CarSearchModel { Id = car.Id }).Select(x => x.Name).ToList();
                    cars_spares.AddRange(spare);
                }
                result.Add(new ReportViewModel
                {
                    Cost = maintenance.Cost,
                    Spares = cars_spares,
                    Cars = cars.Select(x => x.VIN).ToList()

                });
            }
            return result;   
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
