using DocumentFormat.OpenXml.Bibliography;
using STOBusinessLogic.OfficePackage.HelperEnums;
using STOBusinessLogic.OfficePackage.HelperModels;

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

			CreateParagraph(new PdfParagraph { Text = "Отчёт по ТО", Style = "Normal", ParagraphAlignment = PdfParagraphAlignmentType.Center });

			CreateTable(new List<string> { "3cm", "4cm", "4cm", "5cm" });

			CreateRow(new PdfRowParameters
			{
				Texts = new List<string> { "Номер ТО", "Машина", "Деталь", "Дата операции" },
				Style = "NormalTitle",
				ParagraphAlignment = PdfParagraphAlignmentType.Center
			});

			foreach (var maintence in info.Maintences)
			{
				foreach (var car in maintence.MaintenanceCars)
				{
					foreach (var spare in car.Value.Item1.CarSpares)
					{
                        CreateRow(new PdfRowParameters
						{
							Texts = new List<string> { maintence.Id.ToString(), car.Value.Item1.Brand + " " + car.Value.Item1.Model, spare.Value.Item1.Name, maintence.DateCreate.ToString() },
							Style = "Normal",
							ParagraphAlignment = PdfParagraphAlignmentType.Left
						});
					}
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
