using STOContracts.ViewModels;

namespace STOBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfo
    {
        public string FileName { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public List<AccountViewModel> Accounts { get; set; } = new();
    }
}
