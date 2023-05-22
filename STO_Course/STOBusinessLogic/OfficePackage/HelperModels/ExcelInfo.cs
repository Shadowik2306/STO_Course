using STOContracts.ViewModels;
using STOContracts.ViewModels.Client.Default;
using BankYouBankruptContracts.ViewModels;
using BankYouBankruptContracts.ViewModels.Client.Default;
using STOContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; } = string.Empty; 

        public string Title { get; set; } = string.Empty;

        public List<SpareViewModel> Spares { get; set; } = new();

        public List<CarViewModel> Cars { get; set; } = new();
	}
}
