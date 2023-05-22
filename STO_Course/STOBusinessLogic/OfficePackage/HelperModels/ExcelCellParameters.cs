using BankYouBankruptBusinessLogic.OfficePackage.HelperEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankYouBankruptBusinessLogic.OfficePackage.HelperModels
{
    //информация по ячейке в таблице excel
    public class ExcelCellParameters
    {
        //название колонки
        public string ColumnName { get; set; } = string.Empty;

        //строка
        public uint RowIndex { get; set; }

        //тект в ячейке
        public string Text { get; set; } = string.Empty;

        //геттер для того, чтобы не искать каждый раз
        public string CellReference => $"{ColumnName}{RowIndex}";

        //в каком стиле выводить информацию
        public ExcelStyleInfoType StyleInfo { get; set; }
    }
}
