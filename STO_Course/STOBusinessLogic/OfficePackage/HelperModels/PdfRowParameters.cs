using BankYouBankruptBusinessLogic.OfficePackage.HelperEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankYouBankruptBusinessLogic.OfficePackage.HelperModels
{
    //информация по параметрам строк таблицы
    public class PdfRowParameters
    {
        //набор текстов
        public List<string> Texts { get; set; } = new();

        //стиль к текстам
        public string Style { get; set; } = string.Empty;

        //как выравниваем
        public PdfParagraphAlignmentType ParagraphAlignment { get; set; }
    }
}
