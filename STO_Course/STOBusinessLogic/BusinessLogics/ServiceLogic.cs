using Microsoft.Extensions.Logging;
using STOContracts.BindingModels;
using STOContracts.BusinessLogicsContracts;
using STOContracts.SearchModels;
using STOContracts.StoragesContracts;
using STOContracts.ViewModels;

namespace STOBusinessLogic.BusinessLogics
{
    public class ServiceLogic : IServiceLogic
    {
        private readonly ILogger _logger;
        private readonly IServiceStorage _serviceStorage;
        public ServiceLogic(ILogger logger, IServiceStorage serviceStorage)
        {
            _logger = logger;
            _serviceStorage = serviceStorage;
        }
        public List<ServiceViewModel>? ReadList(ServiceSearchModel? model)
        {
            _logger.LogInformation("ReadList. ServiceId:{ ServiceId}", model?.Id);
            var list = model == null ? _serviceStorage.GetFullList() : _serviceStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }
        public ServiceViewModel? ReadElement(ServiceSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. ServiceId:{ ServiceId}", model.Id);
            var element = _serviceStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }
        public bool Create(ServiceBindingModel model)
        {
            CheckModel(model);
            if (_serviceStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }
        public bool Update(ServiceBindingModel model)
        {
            CheckModel(model);
            if (_serviceStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }
        public bool Delete(ServiceBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_serviceStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }
        private void CheckModel(ServiceBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            _logger.LogInformation("Service. ServiceId: { ServiceId}.", model.Id);
        }
    }
}
