using Microsoft.Extensions.Logging;
using STOContracts.BindingModels;
using STOContracts.BusinessLogicsContracts;
using STOContracts.SearchModels;
using STOContracts.StoragesContracts;
using STOContracts.ViewModels;

namespace STOBusinessLogic.BusinessLogics
{
    public class StorekeeperLogic : IStorekeeperLogic
    {
        private readonly ILogger _logger;
        private readonly IStorekeeperStorage _storekeeperStorage;
        public StorekeeperLogic(ILogger logger, IStorekeeperStorage storekeeperStorage)
        {
            _logger = logger;
            _storekeeperStorage = storekeeperStorage;
        }
        public List<StorekeeperViewModel>? ReadList(StorekeeperSearchModel? model)
        {
            _logger.LogInformation("ReadList.StorekeeperId:{Id}", model?.Id);
            var list = model == null ? _storekeeperStorage.GetFullList() : _storekeeperStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }
        public StorekeeperViewModel? ReadElement(StorekeeperSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. StorekeeperId:{Id}", model?.Id);
            var element = _storekeeperStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }

        public bool Create(StorekeeperBindingModel model)
        {
            CheckModel(model);
            if (_storekeeperStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }
        public bool Update(StorekeeperBindingModel model)
        {
            CheckModel(model);
            if (_storekeeperStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }
        public bool Delete(StorekeeperBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_storekeeperStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }
        private void CheckModel(StorekeeperBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            if (string.IsNullOrEmpty(model.Login))
            {
                throw new ArgumentNullException("Нет логина кладовщика", nameof(model.Login));
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                throw new ArgumentNullException("Нет пароля кладовщика", nameof(model.Password));
            }
            if (string.IsNullOrEmpty(model.Email))
            {
                throw new ArgumentNullException("Нет почты кладовщика", nameof(model.Email));
            }
            var element = _storekeeperStorage.GetElement(new StorekeeperSearchModel
            {
               Login  = model.Login,
               Email = model.Email
            });
            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("Кладовщик с такими параметрами уже есть");
            }
            _logger.LogInformation("Storekeeper. Id:{ Id}.Login:{Login}.Email:{Email}.Password:{Password}", model?.Id, model?.Login, model?.Email, model?.Password);
        }
    }
}
