﻿using Microsoft.Extensions.Logging;
using STOBusinessLogic.Mail;
using STOBusinessLogic.OfficePackage;
using STOBusinessLogic.OfficePackage.HelperModels;
using STOContracts.BindingModels;
using STOContracts.BusinessLogicsContracts;
using STOContracts.SearchModels;
using STOContracts.StoragesContracts;
using STOContracts.ViewModels;

namespace STOBusinessLogic.BusinessLogics
{
    public class MaintenanceLogic : IMaintenanceLogic
    {
        private readonly ILogger _logger;
        private readonly IMaintenanceStorage _maintenanceStorage;
        private readonly AbstractSaveToPdf _saveToPdf;
        private readonly ICarStorage _carStorage;
        private readonly MailWorker _mailKitWorker;
        public MaintenanceLogic(ILogger<MaintenanceLogic> logger, IMaintenanceStorage maintenanceStorage, AbstractSaveToPdf saveToPdf, ICarStorage carStorage, MailWorker mailWorker)
        {
            _logger = logger;
            _maintenanceStorage = maintenanceStorage;
            _saveToPdf = saveToPdf;
            _carStorage = carStorage;
            _mailKitWorker = mailWorker;
        }
        public List<MaintenanceViewModel>? ReadList(MaintenanceSearchModel? model)
        {
            _logger.LogInformation("ReadList. MaintenanceId:{ MaintenanceId}", model?.Id);
            var list = model == null ? _maintenanceStorage.GetFullList() : _maintenanceStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }
        public MaintenanceViewModel? ReadElement(MaintenanceSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. MaintenanceId:{ MaintenanceId}", model?.Id);
            var element = _maintenanceStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }
        public bool Create(MaintenanceBindingModel model)
        {
            CheckModel(model);
            if (_maintenanceStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }
        public bool Update(MaintenanceBindingModel model)
        {
            CheckModel(model);
            if (_maintenanceStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }
        public bool Delete(MaintenanceBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_maintenanceStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }
        private void CheckModel(MaintenanceBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            if (model.EmployerId<0)
            {
                throw new ArgumentNullException("Некорректный идентификатор работника", nameof(model.EmployerId));
            }
            if (model.Cost<=0)
            {
                throw new ArgumentNullException("Стоимость должна быть больше нуля", nameof(model.Cost));
            }
            _logger.LogInformation("Maintenance. MaintenanceId:{MaintenanceId}.EmployerId: { EmployerId}.Cost:{Cost}", model.Id, model.EmployerId, model.Cost);            
        }

        public void createReportPdf(List<MaintenanceViewModel> model, EmployerViewModel employer, DateTime to, DateTime from) {
            _saveToPdf.CreateDoc(new PdfInfo() {
                FileName = "Отчет по ТО.pdf",
                Title = "Отчет по ТО",
                DateFrom = from,
                DateTo = to,
                Maintences = model,
                car = _carStorage
            });

            _mailKitWorker.SendMailAsync(new()
            {
                MailAddress = employer.Email,
                Subject = "Отчёт по ТО",
                Text = $"Отчёт по состоянию на {DateTime.Now.ToString()}",
                File = System.IO.File.ReadAllBytes("Отчет по ТО.pdf")
            });
        }

    }
}
