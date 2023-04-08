using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOContracts.ViewModels
{
    public class ReportViewModel
    {
        public double Cost { get; set; }
        public List<string> Cars { get; set; } = new();

        public List<string> Spares { get; set; } = new();
    }
}
