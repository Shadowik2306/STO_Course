﻿using BankYouBankruptContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOBusinessLogic.OfficePackage.HelperModels
{
    //общая информация по документу
    public class WordInfo
    {
        public string FileName { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        //списки для формирования отчёта клиента
        public List<AccountViewModel> Accounts { get; set; } = new();
    }
}
