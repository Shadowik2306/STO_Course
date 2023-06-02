using STOBusinessLogic.BusinessLogics;
using STOContracts.BusinessLogicsContracts;
using STOContracts.StoragesContracts;
using STOContracts.ViewModels;
using STOContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; } = string.Empty; 

        public string Title { get; set; } = string.Empty;

        public List<WorkViewModel> Works { get; set; } = new();

        public IMaintenanceStorage maintenance { get; set; }

	}
}
