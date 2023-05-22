﻿using BankYouBankruptBusinessLogic.OfficePackage.HelperEnums;
using BankYouBankruptBusinessLogic.OfficePackage.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankYouBankruptBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToPdf
    {
        //публичный метод создания документа. Описание методов ниже
        public void CreateDoc(PdfInfo info)
        {
            if(info.ForClient)
			{
				CreateDocClient(info);
			}
			else
			{
				CreateDocCashier(info);
			}
		}

		#region Отчёт для клиента

		public void CreateDocClient(PdfInfo info)
		{
			CreatePdf(info);

			CreateParagraph(new PdfParagraph
			{
				Text = info.Title + $"\nот {DateTime.Now.ToShortDateString()}",

				Style = "NormalTitle",
				ParagraphAlignment = PdfParagraphAlignmentType.Center
			});

			CreateParagraph(new PdfParagraph
			{
				Text = $"Расчётный период: с {info.DateFrom.ToShortDateString()} по {info.DateTo.ToShortDateString()}",
				Style = "Normal",
				ParagraphAlignment = PdfParagraphAlignmentType.Center
			});

			//параграф с отчётом на пополнения
			CreateParagraph(new PdfParagraph { Text = "Отчёт по пополнениям", Style = "Normal", ParagraphAlignment = PdfParagraphAlignmentType.Center });

			CreateTable(new List<string> { "3cm", "3cm", "5cm", "5cm" });

			CreateRow(new PdfRowParameters
			{
				Texts = new List<string> { "Номер операции", "Номер карты", "Сумма", "Дата операции" },
				Style = "NormalTitle",
				ParagraphAlignment = PdfParagraphAlignmentType.Center
			});

			foreach (var report in info.ReportCrediting)
			{
				CreateRow(new PdfRowParameters
				{
					Texts = new List<string> { report.OperationId.ToString(), report.CardNumber, report.SumOperation.ToString(), report.DateComplite.ToString() },
					Style = "Normal",
					ParagraphAlignment = PdfParagraphAlignmentType.Left
				});
			}

			//подсчёт суммы операций на пополнение
			CreateParagraph(new PdfParagraph { Text = $"Итоговая сумма поступлений за период: {info.ReportCrediting.Sum(x => x.SumOperation)}\t", Style = "Normal", ParagraphAlignment = PdfParagraphAlignmentType.Right });

			//отчёт с отчётом на снятие
			CreateParagraph(new PdfParagraph { Text = "Отчёт по снятиям", Style = "Normal", ParagraphAlignment = PdfParagraphAlignmentType.Center });

			CreateTable(new List<string> { "3cm", "3cm", "5cm", "5cm" });

			CreateRow(new PdfRowParameters
			{
				Texts = new List<string> { "Номер операции", "Номер карты", "Сумма", "Дата операции" },
				Style = "NormalTitle",
				ParagraphAlignment = PdfParagraphAlignmentType.Center
			});

			foreach (var report in info.ReportDebiting)
			{
				CreateRow(new PdfRowParameters
				{
					Texts = new List<string> { report.OperationId.ToString(), report.CardNumber, report.SumOperation.ToString(), report.DateComplite.ToString() },
					Style = "Normal",
					ParagraphAlignment = PdfParagraphAlignmentType.Left
				});
			}

			//подсчёт суммы операций на пополнение
			CreateParagraph(new PdfParagraph { Text = $"Итоговая сумма снятий за период: {info.ReportDebiting.Sum(x => x.SumOperation)}\t", Style = "Normal", ParagraphAlignment = PdfParagraphAlignmentType.Right });

			SavePdf(info);
		}

		#endregion

		#region Отчёт для кассира

		//создание отчёта для кассира
		public void CreateDocCashier(PdfInfo info)
		{
			CreatePdf(info);

			CreateParagraph(new PdfParagraph
			{
				Text = info.Title + $"\nот {DateTime.Now.ToShortDateString()}\nФИО клиента: {info.FullClientName}",
				Style = "NormalTitle",
				ParagraphAlignment = PdfParagraphAlignmentType.Center
			});

			CreateParagraph(new PdfParagraph
			{
				Text = $"Расчётный период: с {info.DateFrom.ToShortDateString()} по {info.DateTo.ToShortDateString()}",
				Style = "Normal",
				ParagraphAlignment = PdfParagraphAlignmentType.Center
			});

			//параграф с отчётом по выдаче наличных с карт
			CreateParagraph(new PdfParagraph { Text = "Отчёт по выдаче наличных со счёта", Style = "Normal", ParagraphAlignment = PdfParagraphAlignmentType.Center });

			CreateTable(new List<string> { "3.5cm", "3.5cm", "5cm", "5cm" });

			CreateRow(new PdfRowParameters
			{
				Texts = new List<string> { "Номер операции", "Номер счёта", "Сумма операции", "Дата операции" },
				Style = "NormalTitle",
				ParagraphAlignment = PdfParagraphAlignmentType.Center
			});

			foreach (var report in info.ReportCashWithdrawal)
			{
				CreateRow(new PdfRowParameters
				{
					Texts = new List<string> { report.OperationId.ToString(), report.AccountPayeeNumber, report.SumOperation.ToString(), report.DateComplite.ToShortDateString(), },
					Style = "Normal",
					ParagraphAlignment = PdfParagraphAlignmentType.Left
				});
			}

			CreateParagraph(new PdfParagraph { Text = $"Итоговая сумма снятий за период: {info.ReportCashWithdrawal.Sum(x => x.SumOperation)}\t", Style = "Normal", ParagraphAlignment = PdfParagraphAlignmentType.Right });

			//параграф с отчётом по переводу денег со счёта на счёт
			CreateParagraph(new PdfParagraph { Text = "Отчёт по денежным переводам между счетами", Style = "Normal", ParagraphAlignment = PdfParagraphAlignmentType.Center });

			CreateTable(new List<string> { "3cm", "3cm", "3cm", "4cm", "4cm" });

			CreateRow(new PdfRowParameters
			{
				Texts = new List<string> { "Номер операции", "Номер счёта отправителя", "Номер счёта получателя", "Сумма операции", "Дата операции" },
				Style = "NormalTitle",
				ParagraphAlignment = PdfParagraphAlignmentType.Center
			});

			foreach (var report in info.ReportMoneyTransfer)
			{
				CreateRow(new PdfRowParameters
				{
					Texts = new List<string> { report.OperationId.ToString(), report.AccountSenderNumber, report.AccountPayeeNumber, report.SumOperation.ToString(), report.DateComplite.ToShortDateString(), },
					Style = "Normal",
					ParagraphAlignment = PdfParagraphAlignmentType.Left
				});
			}

			CreateParagraph(new PdfParagraph { Text = $"Итоговая сумма переводов за период: {info.ReportMoneyTransfer.Sum(x => x.SumOperation)}\t", Style = "Normal", ParagraphAlignment = PdfParagraphAlignmentType.Right });

			SavePdf(info);
		}

		#endregion

		/// Создание pdf-файла
		protected abstract void CreatePdf(PdfInfo info);

        /// Создание параграфа с текстом
        protected abstract void CreateParagraph(PdfParagraph paragraph);

        /// Создание таблицы
        protected abstract void CreateTable(List<string> columns);

        /// Создание и заполнение строки
        protected abstract void CreateRow(PdfRowParameters rowParameters);

        /// Сохранение файла
        protected abstract void SavePdf(PdfInfo info);
    }
}
