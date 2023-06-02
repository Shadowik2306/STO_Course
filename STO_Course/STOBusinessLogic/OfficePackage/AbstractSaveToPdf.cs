using DocumentFormat.OpenXml.Bibliography;
using STOBusinessLogic.OfficePackage.HelperEnums;
using STOBusinessLogic.OfficePackage.HelperModels;
using STOContracts.SearchModels;

namespace STOBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToPdf
    {
        public void CreateDoc(PdfInfo info)
        {
			
			CreatePDF(info);
		}


		public void CreatePDF(PdfInfo info)
		{
			CreatePdf(info);


			CreateParagraph(new PdfParagraph
			{
				Text = $"Расчётный период: с {info.DateFrom.ToShortDateString()} по {info.DateTo.ToShortDateString()}",
				Style = "Normal",
				ParagraphAlignment = PdfParagraphAlignmentType.Center
			});

			CreateParagraph(new PdfParagraph { Text = "Отчёт по движению запчастей", Style = "Normal", ParagraphAlignment = PdfParagraphAlignmentType.Center });

			CreateTable(new List<string> { "4cm", "4cm", "5cm" });

			CreateRow(new PdfRowParameters
			{
				Texts = new List<string> { "Название работы", "Деталь", "Дата работы" },
				Style = "NormalTitle",
				ParagraphAlignment = PdfParagraphAlignmentType.Center
			});

			foreach (var work in info.Works)
			{
				foreach (var spare in work.WorkSpares)
				{
					CreateRow(new PdfRowParameters() { 
						Texts = new List<string> { work.Title, spare.Value.Item1.Name, work.Date.ToString() },
                        Style = "Normal",
                        ParagraphAlignment = PdfParagraphAlignmentType.Left
                    });
				}
			}
		
			SavePdf(info);
		}


		protected abstract void CreatePdf(PdfInfo info);

        protected abstract void CreateParagraph(PdfParagraph paragraph);

        protected abstract void CreateTable(List<string> columns);

        protected abstract void CreateRow(PdfRowParameters rowParameters);

        protected abstract void SavePdf(PdfInfo info);
    }
}
