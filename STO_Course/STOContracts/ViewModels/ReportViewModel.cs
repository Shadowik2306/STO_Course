using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOContracts.ViewModels
{
    public class ReportViewModel
    {
        public string Name { get; set; } = string.Empty;

        public List<string> Spares { get; set; } = new();
    }
}
