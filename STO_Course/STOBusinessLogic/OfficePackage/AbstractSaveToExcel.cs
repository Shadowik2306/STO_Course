using BankYouBankruptBusinessLogic.OfficePackage.HelperEnums;
using BankYouBankruptBusinessLogic.OfficePackage.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankYouBankruptBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToExcel
    {
        //Создание отчета. Описание методов ниже
        public void CreateReport(ExcelInfo info)
        {
            CreateExcel(info);

			//вставляет заголовок
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });

			//соединяет 3 ячейки для заголовка
            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });

			//номер строчки в докуметне
            uint rowIndex = 2;

			foreach (var mt in info.MoneyTransfer)
			{
				//вставляет номер перевода
				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "A",
					RowIndex = rowIndex,
					Text = "Перевод №" +  mt.Id,
					StyleInfo = ExcelStyleInfoType.Text
				});

				rowIndex++;

				//строчка с номером счёта отправителя
				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "B",
					RowIndex = rowIndex,
					Text = "Номер счёта отправителя: ",
					StyleInfo = ExcelStyleInfoType.TextWithBorder
				});

				//вставка номера отправителя
				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "C",
					RowIndex = rowIndex,
					Text = mt.AccountSenderNumber,
					StyleInfo = ExcelStyleInfoType.TextWithBorder
				});

				rowIndex++;

				//строчка с номером счёта получателя
				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "B",
					RowIndex = rowIndex,
					Text = "Номер счёта получателя: ",
					StyleInfo = ExcelStyleInfoType.TextWithBorder
				});

				//вставка номера отправителя
				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "C",
					RowIndex = rowIndex,
					Text = mt.AccountPayeeNumber,
					StyleInfo = ExcelStyleInfoType.TextWithBorder
				});

				rowIndex++;

				//Вставляет слово "Сумма перевода"
				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "A",
					RowIndex = rowIndex,
					Text = "Сумма перевода: ",
					StyleInfo = ExcelStyleInfoType.Text
				});

				//подсчитывает общее кол-во заготовок в изделии
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

        //Создание excel-файла
        protected abstract void CreateExcel(ExcelInfo info);

        //Добавляем новую ячейку в лист
        protected abstract void InsertCellInWorksheet(ExcelCellParameters excelParams);

        //Объединение ячеек
        protected abstract void MergeCells(ExcelMergeParameters excelParams);

        //Сохранение файла
        protected abstract void SaveExcel(ExcelInfo info);
    }
}
