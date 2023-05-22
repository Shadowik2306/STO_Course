using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankYouBankruptBusinessLogic.OfficePackage.HelperModels
{
    //информация для объединения ячеек
    public class ExcelMergeParameters
    {
        public string CellFromName { get; set; } = string.Empty;

        public string CellToName { get; set; } = string.Empty;

        //гетер для указания диапазона для объединения, чтобы каждый раз его не вычислять
        public string Merge => $"{CellFromName}:{CellToName}";
    }
}
