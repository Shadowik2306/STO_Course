using Microsoft.Extensions.Logging;
using STOBusinessLogic.OfficePackage;
using STOBusinessLogic.OfficePackage.HelperModels;
using STOContracts.BindingModels;
using STOContracts.BusinessLogicsContracts;
using STOContracts.SearchModels;
using STOContracts.StoragesContracts;
using STOContracts.ViewModels;

namespace STOBusinessLogic.BusinessLogics
{
    public class CarLogic : ICarLogic
    {
        private readonly ILogger _logger;
        private readonly ICarStorage _carStorage;
        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToWord _saveToWord;
        public CarLogic(ILogger<CarLogic> logger, ICarStorage carStorage, AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord)
        {
            _logger = logger;
            _carStorage = carStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
        }
        public List<CarViewModel>? ReadList(CarSearchModel? model)
        {
            _logger.LogInformation("ReadList.CarId:{ CarId}", model?.Id);
            var list = model == null ? _carStorage.GetFullList() : _carStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }
        public CarViewModel? ReadElement(CarSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. CarId:{ CarId}", model?.Id);
            var element = _carStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }

        public bool Create(CarBindingModel model)
        {
            CheckModel(model);
            if (_carStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }
        public bool Update(CarBindingModel model)
        {
            CheckModel(model);
            if (_carStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }
        public bool Delete(CarBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_carStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }
        private void CheckModel(CarBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            if (string.IsNullOrEmpty(model.Brand))
            {
                throw new ArgumentNullException("Нет бренда автомобиля", nameof(model.Brand));
            }
            if (string.IsNullOrEmpty(model.Model))
            {
                throw new ArgumentNullException("Нет модели автомобиля", nameof(model.Model));
            }
            if (string.IsNullOrEmpty(model.VIN))
            {
                throw new ArgumentNullException("Нет VIN автомобиля", nameof(model.Model));
            }
            var element = _carStorage.GetElement(new CarSearchModel
            {
               VIN  = model.VIN
            });
            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("Автомобиль с таким VIN уже есть");
            }
            _logger.LogInformation("Car. Id:{ Id}.CarBrand:{CarBrand}.CarModel:{CarModel}.CarVIN:{CarVIN}", model?.Id, model?.Brand, model?.Brand, model?.VIN);
        }

        public void CreateExcelReport(List<CarViewModel> cars) {
            _saveToExcel.CreateReport(new ExcelInfo() { 
                FileName = "Отчет по деталям.xls",
                Title = "Отчет по деталям",
                Cars = cars,
            });
        }

        public void CreateWordReport(List<CarViewModel> cars)
        {
            _saveToWord.CreateDoc(new WordInfo()
            {
                FileName = "Отчет по деталям.dock",
                Title = "Отчет по деталям",
                Cars = cars,
            });
        }
    }
}
