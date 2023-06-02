using STOBusinessLogic.OfficePackage.HelperEnums;
using STOBusinessLogic.OfficePackage.HelperModels;
using STOContracts.SearchModels;

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



            foreach (var work in info.Works)
            {
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> { ("Работв " + work.Title, new WordTextProperties { Bold = true, Size = "24" }) },
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
                        new("Машина", new WordTextProperties { Bold = true, Size = "24" } ),
                    }
                };


                foreach (var maintences in work.WorkMaintenances)
                {
                    foreach (var car in info.maintenance.GetCars(new MaintenanceSearchModel() { Id = maintences.Key }))
                    {
                        rowList.Add(new()
                        {
                            new("Машина " + car.Brand + " " + car.Model + " " + car.VIN, new WordTextProperties { Size = "24" }),
                     
                        });
                    }
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
