using BankYouBankruptBusinessLogic.OfficePackage.HelperEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankYouBankruptBusinessLogic.OfficePackage.HelperModels
{
    //модель свойств текста, которые нам нужны в word документе
    public class WordTextProperties
    {
        //размере текста
        public string Size { get; set; } = string.Empty;

        //надо ли делать его жирным
        public bool Bold { get; set; }

        //выравнивание
        public WordJustificationType JustificationType { get; set; }
    }
}
