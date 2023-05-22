using BankYouBankruptContracts.ViewModels;
using BankYouBankruptContracts.ViewModels.Client.Default;
using STOContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankYouBankruptBusinessLogic.OfficePackage.HelperModels
{
    //информация по excel файлу, который хотим создать
    public class ExcelInfo
    {
        //название файла
        public string FileName { get; set; } = string.Empty; 

        //заголовок
        public string Title { get; set; } = string.Empty;

		//список для отчёта клиента
        public List<SpareViewModel> Spares { get; set; } = new();

        public List<CarViewModel> Cars { get; set; } = new();
	}
}
