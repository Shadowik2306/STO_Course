using STOContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOBusinessLogic.OfficePackage.HelperModels
{
    public class PdfInfo
    {
        public string FileName { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public bool ForClient { get; set; } = true;

        public string FullClientName { get; set; } = string.Empty;

        public List<SpareViewModel> ReportCrediting { get; set; } = new();

		public List<CarViewModel> ReportDebiting { get; set; } = new();

  //      public List<ReportCashierViewModel> ReportMoneyTransfer { get; set; } = new();

		//public List<ReportCashierViewModel> ReportCashWithdrawal { get; set; } = new();
	}
}
