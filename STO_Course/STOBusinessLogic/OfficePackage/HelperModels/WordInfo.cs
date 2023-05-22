﻿using STOContracts.ViewModels;
using BankYouBankruptContracts.ViewModels;
using STOContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfo
    {
        public string FileName { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public List<SpareViewModel> Accounts { get; set; } = new();
    }
}
