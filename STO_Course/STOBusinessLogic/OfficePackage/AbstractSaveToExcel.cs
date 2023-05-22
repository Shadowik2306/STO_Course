using STOBusinessLogic.OfficePackage.HelperEnums;
using STOBusinessLogic.OfficePackage.HelperModels;

namespace STOBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToExcel
    {
        public void CreateReport(ExcelInfo info)
        {
            CreateExcel(info);

            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });

            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });

            uint rowIndex = 2;

			foreach (var mt in info.MoneyTransfer)
			{
				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "A",
					RowIndex = rowIndex,
					Text = "Перевод №" +  mt.Id,
					StyleInfo = ExcelStyleInfoType.Text
				});

				rowIndex++;

				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "B",
					RowIndex = rowIndex,
					Text = "Номер счёта отправителя: ",
					StyleInfo = ExcelStyleInfoType.TextWithBorder
				});

				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "C",
					RowIndex = rowIndex,
					Text = mt.AccountSenderNumber,
					StyleInfo = ExcelStyleInfoType.TextWithBorder
				});

				rowIndex++;

				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "B",
					RowIndex = rowIndex,
					Text = "Номер счёта получателя: ",
					StyleInfo = ExcelStyleInfoType.TextWithBorder
				});

				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "C",
					RowIndex = rowIndex,
					Text = mt.AccountPayeeNumber,
					StyleInfo = ExcelStyleInfoType.TextWithBorder
				});

				rowIndex++;

				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "A",
					RowIndex = rowIndex,
					Text = "Сумма перевода: ",
					StyleInfo = ExcelStyleInfoType.Text
				});

				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "C",
					RowIndex = rowIndex,
					Text = mt.Sum.ToString(),
					StyleInfo = ExcelStyleInfoType.Text
				});

				rowIndex++;
			}

			SaveExcel(info);
        }

        protected abstract void CreateExcel(ExcelInfo info);

        protected abstract void InsertCellInWorksheet(ExcelCellParameters excelParams);

        protected abstract void MergeCells(ExcelMergeParameters excelParams);

        protected abstract void SaveExcel(ExcelInfo info);
    }
}
