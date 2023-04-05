using Microsoft.Extensions.Logging;
using STOContracts.BindingModels;
using STOContracts.BusinessLogicsContracts;
using STOContracts.SearchModels;
using STOContracts.StoragesContracts;
using STOContracts.ViewModels;

namespace STOBusinessLogic.BusinessLogics
{
    public class WorkDurationLogic : IWorkDurationLogic
    {
        private readonly ILogger _logger;
        private readonly IWorkDurationStorage _workDurationStorage;
        public WorkDurationLogic(ILogger logger, IWorkDurationStorage workDurationStorage)
        {
            _logger = logger;
            _workDurationStorage = workDurationStorage;
        }
        public List<WorkDurationViewModel>? ReadList(WorkDurationSearchModel? model)
        {
            _logger.LogInformation("ReadList.WorkDurationId:{Id}", model?.Id);
            var list = model == null ? _workDurationStorage.GetFullList() : _workDurationStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }
        public WorkDurationViewModel? ReadElement(WorkDurationSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. WorkDurationId:{Id}", model?.Id);
            var element = _workDurationStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }

        public bool Create(WorkDurationBindingModel model)
        {
            CheckModel(model);
            if (_workDurationStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }
        public bool Update(WorkDurationBindingModel model)
        {
            CheckModel(model);
            if (_workDurationStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }
        public bool Delete(WorkDurationBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_workDurationStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }
        private void CheckModel(WorkDurationBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            if (model.Duration < 0)
            {
                throw new ArgumentNullException("Нет длительности", nameof(model.Duration));
            }
            _logger.LogInformation("WorkDuration. Id:{ Id}.Duration:{Duration}", model?.Id, model?.Duration);
        }
    }
}
