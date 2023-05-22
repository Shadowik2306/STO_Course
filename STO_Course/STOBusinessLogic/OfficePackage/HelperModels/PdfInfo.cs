using BankYouBankruptContracts.ViewModels;
using BankYouBankruptContracts.ViewModels.Client.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOBusinessLogic.OfficePackage.HelperModels
{
    //общая информация по pdf файлу
    public class PdfInfo
    {
        public string FileName { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        //по умолчанию отчёт делается для клиента
        public bool ForClient { get; set; } = true;

        //для передачи полного имени клиента в отчёт
        public string FullClientName { get; set; } = string.Empty;

        //перечень заказов за указанный период для вывода/сохранения
        public List<ReportClientViewModel> ReportCrediting { get; set; } = new();

		//перечень заказов за указанный период для вывода/сохранения
		public List<ReportClientViewModel> ReportDebiting { get; set; } = new();

        //перечень переводов со счёта на счёт
        public List<ReportCashierViewModel> ReportMoneyTransfer { get; set; } = new();

		//перечень зачислений денежных средств на карту (т. е. на её счёт)
		public List<ReportCashierViewModel> ReportCashWithdrawal { get; set; } = new();
	}
}
