using Microsoft.Extensions.Logging;
using STOContracts.BindingModels;
using STOContracts.BusinessLogicsContracts;
using STOContracts.SearchModels;
using STOContracts.StoragesContracts;
using STOContracts.ViewModels;

namespace STOBusinessLogic.BusinessLogics
{
    public class EmployerLogic : IEmployerLogic
    {
        private readonly ILogger _logger;
        private readonly IEmployerStorage _employerStorage;
        private readonly IMaintenanceLogic _maintenanceLogic;
        private readonly IServiceLogic _serviceLogic;
        private readonly ICarLogic _carLogic;
        public EmployerLogic(ILogger<EmployerLogic> logger, IEmployerStorage employerStorage, IMaintenanceLogic maintenanceLogic, IServiceLogic serviceLogic, ICarLogic carLogic)
        {
            _logger = logger;
            _employerStorage = employerStorage;
            _maintenanceLogic = maintenanceLogic;
            _serviceLogic = serviceLogic;
            _carLogic = carLogic;
        }
        public List<EmployerViewModel>? ReadList(EmployerSearchModel? model)
        {
            _logger.LogInformation("ReadList. EmployerId:{ EmployerId}", model?.Id);
            var list = model == null ? _employerStorage.GetFullList() : _employerStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }
        public EmployerViewModel? ReadElement(EmployerSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. EmployerId:{ EmployerId}", model?.Id);
            var element = _employerStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }
        public bool Create(EmployerBindingModel model)
        {
            CheckModel(model);
            if (_employerStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }
        public bool Update(EmployerBindingModel model)
        {
            CheckModel(model);
            if (_employerStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }
        public bool Delete(EmployerBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_employerStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }
        private void CheckModel(EmployerBindingModel model, bool withParams = true)
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
                throw new ArgumentNullException("Нет логина работника", nameof(model.Login));
            }
            if (string.IsNullOrEmpty(model.Email))
            {
                throw new ArgumentNullException("Нет почты работника", nameof(model.Email));
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                throw new ArgumentNullException("Нет пароля работника", nameof(model.Password));
            }
            var element = _employerStorage.GetElement(new EmployerSearchModel
            {
                Login = model.Login
            });
            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("работник с таким логином уже есть");
            }
            _logger.LogInformation("Employer. EmployerId:{ Id}.EmployerLogin:{EmployerLogin}.EmployerEmail:{EmployerEmail}", model?.Id, model?.Login, model?.Email);
        }

        public void Emulation() {
            Create(new EmployerBindingModel() { 
                Email = "1@1",
                Login = "emp" + ReadList(null).Count,
                Password = "123"
            });

            Random random = new Random();

            for (int i = 0; i < random.Next(1, 4); i++) {
                _maintenanceLogic.Create(new MaintenanceBindingModel()
                {
                    Cost = random.Next(1000, 100000),
                    DateCreate = DateTime.Now,
                    EmployerId = ReadList(null).Count - 1
                });
            }

            for (int i = 0; i < random.Next(1, 4); i++)
            {
                _carLogic.Create(new CarBindingModel()
                {
                    Brand = "Unknown",
                    Model = "Car " + _carLogic.ReadList(null).Count,
                    VIN = "None",
                });
            }
        }
    }
}
