using Microsoft.Extensions.Logging;
using STOBusinessLogic.Mail;
using STOBusinessLogic.OfficePackage;
using STOBusinessLogic.OfficePackage.HelperModels;
using STOBusinessLogic.OfficePackage.Implements;
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
        private readonly IMaintenanceStorage _maintenanceStorage;
        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToWord _saveToWord;
        private readonly AbstractSaveToPdf _saveToPdf;
        private readonly MailWorker _mailKitWorker;
        public WorkLogic(ILogger<WorkLogic> logger, IWorkStorage workStorage, AbstractSaveToExcel saveToExcel,
            AbstractSaveToWord saveToWord, AbstractSaveToPdf saveToPdf, MailWorker mailWorker, IMaintenanceStorage maintenanceStorage)
        {
            _logger = logger;
            _workStorage = workStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
            _mailKitWorker = mailWorker;
            _maintenanceStorage = maintenanceStorage;
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

        public void CreateExcelReport(List<WorkViewModel> works)
        {
            _saveToExcel.CreateReport(new ExcelInfo()
            {
                FileName = "Отчет по деталям.xls",
                Title = "Отчет по деталям",
                Works = works,
                maintenance = _maintenanceStorage,
            });
        }

        public void CreateWordReport(List<WorkViewModel> works)
        {
            _saveToWord.CreateDoc(new WordInfo()
            {
                FileName = "Отчет по деталям.doc",
                Title = "Отчет по деталям",
                Works = works,
                maintenance = _maintenanceStorage
            });
        }

        public void СreateReportPdf(List<WorkViewModel> model, StorekeeperViewModel storekeeper, DateTime to, DateTime from)
        {
            _saveToPdf.CreatePDF(new PdfInfo()
            {
                FileName = "Отчет по движению деталей.pdf",
                Title = "Отчет по движению деталей",
                DateFrom = from,
                DateTo = to,
                Works = model,
            });

            _mailKitWorker.SendMailAsync(new()
            {
                MailAddress = storekeeper.Email,
                Subject = "Отчет по движению деталей",
                Text = $"Отчёт по состоянию на {DateTime.Now.ToString()}",
                File = System.IO.File.ReadAllBytes("Отчет по движению деталей.pdf")
            });
        }
    }
}
