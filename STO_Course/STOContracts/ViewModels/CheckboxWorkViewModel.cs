using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOContracts.ViewModels
{
    public class CheckboxWorkViewModel
    {
        public List<CheckboxViewModel> Spares { get; set; }

        public List<CheckboxViewModel> Maintenance { get; set; }
    }
}
