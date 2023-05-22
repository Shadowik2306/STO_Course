using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankYouBankruptBusinessLogic.OfficePackage.HelperModels
{
    //модель параграфов, которые есть в тексте
    public class WordParagraph
    {
        //набор текстов в абзаце (для случая, если в абзаце текст разных стилей)
        public List<(string, WordTextProperties)> Texts { get; set; } = new();

        //свойства параграфа, если они есть
        public WordTextProperties? TextProperties { get; set; }
    }
}
