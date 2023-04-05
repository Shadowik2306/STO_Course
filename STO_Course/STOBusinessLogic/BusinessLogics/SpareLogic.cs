using Microsoft.Extensions.Logging;
using STOContracts.BindingModels;
using STOContracts.BusinessLogicsContracts;
using STOContracts.SearchModels;
using STOContracts.StoragesContracts;
using STOContracts.ViewModels;

namespace STOBusinessLogic.BusinessLogics
{
    public class SpareLogic : ISpareLogic
    {
        private readonly ILogger _logger;
        private readonly ISpareStorage _spareStorage;
        public SpareLogic(ILogger logger, ISpareStorage spareStorage)
        {
            _logger = logger;
            _spareStorage = spareStorage;
        }
        public List<SpareViewModel>? ReadList(SpareSearchModel? model)
        {
            _logger.LogInformation("ReadList.SpareId:{Id}", model?.Id);
            var list = model == null ? _spareStorage.GetFullList() : _spareStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }
        public SpareViewModel? ReadElement(SpareSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. SpareId:{Id}", model?.Id);
            var element = _spareStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }

        public bool Create(SpareBindingModel model)
        {
            CheckModel(model);
            if (_spareStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }
        public bool Update(SpareBindingModel model)
        {
            CheckModel(model);
            if (_spareStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }
        public bool Delete(SpareBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_spareStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }
        private void CheckModel(SpareBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException("Нет название детали", nameof(model.Name));
            }
            if (model.Price <= 0)
            {
                throw new ArgumentNullException("Неправильная цена детали", nameof(model.Price));
            }
           
            var element = _spareStorage.GetElement(new SpareSearchModel
            {
               Name = model.Name
            });
            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("Деталь с таким названием уже есть");
            }
            _logger.LogInformation("Spare. Id:{ Id}.Name:{Name}.Price:{Price}", model?.Id, model?.Name, model?.Price);
        }
    }
}
