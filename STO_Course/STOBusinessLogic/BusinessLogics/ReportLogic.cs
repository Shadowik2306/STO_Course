
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
       

        public ReportLogic(IReinforcedStorage reinforcedStorage, IComponentStorage componentStorage, IOrderStorage orderStorage,
            AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord, AbstractSaveToPdf saveToPdf)
        {
            _reinforcedStorage = reinforcedStorage;
            _componentStorage = componentStorage;
            _orderStorage = orderStorage;

            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        }
        public List<ReportReinforcedComponentViewModel> GetReinforcedComponents()
        {
            return _reinforcedStorage.GetFullList().Select(x => new ReportReinforcedComponentViewModel
            {
                ReinforcedName = x.ReinforcedName,
                Components = x.ReinforcedComponents.Select(x => (x.Value.Item1.ComponentName, x.Value.Item2)).ToList(),
                TotalCount = x.ReinforcedComponents.Select(x => x.Value.Item2).Sum()
            }).ToList();
        }
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderSearchModel { DateFrom = model.DateFrom, DateTo = model.DateTo })
                    .Select(x => new ReportOrdersViewModel
                    {
                        Id = x.Id,
                        DateCreate = x.DateCreate,
                        ReinforcedName = x.ReinforcedName,
                        Sum = x.Sum,
                        Status= x.Status.ToString()
                    })
                    .ToList();
        }
        public void SaveReinforcedesToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список изделий",
                Reinforcedes = _reinforcedStorage.GetFullList()
            });
        }
        public void SaveReinforcedComponentToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список изделий",
                ReinforcedComponents = GetReinforcedComponents()
            });
        }
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom!.Value,
                DateTo = model.DateTo!.Value,
                Orders = GetOrders(model)
            });
        }
    }
}
