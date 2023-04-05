using Microsoft.Extensions.Logging;
using STOContracts.BindingModels;
using STOContracts.BusinessLogicsContracts;
using STOContracts.SearchModels;
using STOContracts.StoragesContracts;
using STOContracts.ViewModels;

namespace STOBusinessLogic.BusinessLogics
{
    public class WorkLogic : IWorkLogic
    {
        private readonly ILogger _logger;
        private readonly IWorkStorage _workStorage;
        public WorkLogic(ILogger logger, IWorkStorage workStorage)
        {
            _logger = logger;
            _workStorage = workStorage;
        }
        public List<WorkViewModel>? ReadList(WorkSearchModel? model)
        {
            _logger.LogInformation("ReadList.WorkId:{Id}", model?.Id);
            var list = model == null ? _workStorage.GetFullList() : _workStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }
        public WorkViewModel? ReadElement(WorkSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. WorkId:{ Id}", model?.Id);
            var element = _workStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }

        public bool Create(WorkBindingModel model)
        {
            CheckModel(model);
            if (_workStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }
        public bool Update(WorkBindingModel model)
        {
            CheckModel(model);
            if (_workStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }
        public bool Delete(WorkBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_workStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }
        private void CheckModel(WorkBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            if (string.IsNullOrEmpty(model.Title))
            {
                throw new ArgumentNullException("Нет названия работы", nameof(model.Title));
            }
            if (model.Price < 0)
            {
                throw new ArgumentNullException("Нет цены работы", nameof(model.Price));
            }
            var element = _workStorage.GetElement(new WorkSearchModel
            {
               Title = model.Title,
            });
            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("Работа с таким именем уже есть");
            }
            _logger.LogInformation("Work. Id:{ Id}.Title:{Title}.Price:{Price}", model?.Id, model?.Title, model?.Price);
        }
    }
}
