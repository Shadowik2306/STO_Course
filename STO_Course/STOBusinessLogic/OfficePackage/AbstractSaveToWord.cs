using STOBusinessLogic.OfficePackage.HelperEnums;
using STOBusinessLogic.OfficePackage.HelperModels;

namespace STOBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToWord
    {
        public void CreateDoc(WordInfo info)
        {
            CreateSpareReport(info);
        }

        private void CreateSpareReport(WordInfo info)
        {
            CreateWord(info);

            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24" }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });



            foreach (var car in info.Cars)
            {
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> { ("Машина " + car.Brand + car.Model + car.VIN, new WordTextProperties { Bold = true, Size = "24" }) },
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Center
                    }
                });

                List<List<(string, WordTextProperties)>> rowList = new()
                {
                    new()
                    {
                        new("Деталь", new WordTextProperties { Bold = true, Size = "24" } ),
                        new("Колл-во", new WordTextProperties { Bold = true, Size = "24" } )
                    }
                };

                foreach (var spare in car.CarSpares)
                {
                    rowList.Add(new()
                    {
                        new(spare.Value.Item1.Name.ToString(), new WordTextProperties { Size = "24" }),
                        new(spare.Value.Item2.ToString(), new WordTextProperties { Size = "24" })
                    });


                }

                CreateTable(new WordParagraph
                {
                    RowTexts = rowList,
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Center
                    }
                });
            }

            SaveWord(info);
        }

        protected abstract void CreateWord(WordInfo info);

        protected abstract void CreateParagraph(WordParagraph paragraph);

        protected abstract void CreateTable(WordParagraph paragraph);

        protected abstract void SaveWord(WordInfo info);
    }
}
