using Microsoft.Extensions.Logging;
using STOContracts.BindingModels;
using STOContracts.BusinessLogicsContracts;
using STOContracts.SearchModels;
using STOContracts.StoragesContracts;
using STOContracts.ViewModels;
using STODataModels.Models;

namespace STOBusinessLogic.BusinessLogics
{
    public class StorekeeperLogic : IStorekeeperLogic
    {
        private readonly ILogger _logger;
        private readonly IStorekeeperStorage _storekeeperStorage;
        private readonly ISpareLogic _spareLogic;
        private readonly IWorkDurationLogic _workDurationLogic;
        private readonly IWorkLogic _workLogic;
        private readonly IMaintenanceLogic _maintenanceLogic;
        public StorekeeperLogic(ILogger<StorekeeperLogic> logger, IStorekeeperStorage storekeeperStorage,
            ISpareLogic spareLogic, IWorkDurationLogic workDurationLogic, IWorkLogic workLogic, IMaintenanceLogic maintenanceLogic)
        {
            _logger = logger;
            _storekeeperStorage = storekeeperStorage;
            _spareLogic = spareLogic;
            _workDurationLogic = workDurationLogic;
            _workLogic = workLogic;
            _maintenanceLogic = maintenanceLogic;
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

        public void Imitation() {
            Create(new StorekeeperBindingModel() { 
                Login = "stk " + ReadList(null).Count.ToString(),
                Email = "123@mail.ru",
                Password = "123",
            });
            int storekeeper_id = ReadList(null).Last().Id;
            Random random = new();
            for (int i = 0; i < random.Next(1, 3); i++) {
                _spareLogic.Create(new SpareBindingModel()
                {
                    Name = "spare " + _spareLogic.ReadList(null).Count.ToString(),
                    Price = random.Next(1000, 10000),
                });
            }
            for (int i = 0; i < random.Next(1, 3); i++)
            {
                _workDurationLogic.Create(new WorkDurationBindingModel()
                {
                    Duration = random.Next(10, 50)
                });
                int workduration_id = _workDurationLogic.ReadList(null).Last().Id;
                _workLogic.Create(new WorkBindingModel()
                {
                   Date = DateTime.Now,
                   DurationId = workduration_id,
                   Price = random.Next(10000, 50000),
                   StorekeeperId = storekeeper_id,
                   Title = "work " + _workLogic.ReadList(null).Count.ToString(),
                   WorkSpares = _spareLogic.ReadList(null).Where(x => random.Next(0, 1) == 1)
                   .ToDictionary(x => x.Id, x => (x as ISpareModel, random.Next(2, 4))),
                   WorkMaintenances = _maintenanceLogic.ReadList(null).Where(x => random.Next(0, 1) == 1)
                   .ToDictionary(x => x.Id, x => (x as IMaintenanceModel, random.Next(2, 4)))
                });;
            }
        }
    }
}
