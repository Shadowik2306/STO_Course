using STOContracts.ViewModels;
using STOContracts.ViewModels.Client.Default;

namespace STOBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; } = string.Empty; 

        public string Title { get; set; } = string.Empty;

        public List<MoneyTransferViewModel> MoneyTransfer { get; set; } = new();

        public List<DebitingViewModel> Debiting { get; set; } = new();
	}
}
