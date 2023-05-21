using BankYouBankruptDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankYouBankruptContracts.ViewModels.Client.Reports
{
    public class CheckboxViewModel
    {
        public int Id { get; set; }

        public string LabelName { get; set; }

        public bool IsChecked { get; set; }
    }
}
