using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOContracts.ViewModels
{
    public class CheckboxViewModel
    {
        public int Id { get; set; }

        public string LabelName { get; set; }

        public bool IsChecked { get; set; }

        public int Count { get; set; }
    }
}
