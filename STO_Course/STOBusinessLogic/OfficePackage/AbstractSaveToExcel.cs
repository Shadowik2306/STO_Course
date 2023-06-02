using STOBusinessLogic.OfficePackage.HelperEnums;
using STOBusinessLogic.OfficePackage.HelperModels;
using STOContracts.SearchModels;

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

			foreach (var work in info.Works)
			{
				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "A",
					RowIndex = rowIndex,
					Text = "Работа " +  work.Title,
					StyleInfo = ExcelStyleInfoType.Text
				});

                rowIndex++;
			    foreach (var maintences in work.WorkMaintenances) {
                    foreach (var car in info.maintenance.GetCars(new MaintenanceSearchModel() {
                        Id = maintences.Key,
                    }))
                    {
                        InsertCellInWorksheet(new ExcelCellParameters
                        {
                            ColumnName = "B",
                            RowIndex = rowIndex,
                            Text = "Машина " + car.Brand + " " + car.Model + " " + car.VIN,
                            StyleInfo = ExcelStyleInfoType.Text
                        });

                        
                        rowIndex++;
                    }
                }

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
