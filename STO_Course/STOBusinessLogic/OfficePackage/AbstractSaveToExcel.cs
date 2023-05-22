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

			foreach (var car in info.Cars)
			{
				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "A",
					RowIndex = rowIndex,
					Text = "Машина" +  car.Id,
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
