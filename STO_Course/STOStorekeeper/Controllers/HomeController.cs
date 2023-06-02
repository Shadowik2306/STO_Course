using Microsoft.AspNetCore.Mvc;
using STOBusinessLogic.BusinessLogics;
using STOContracts.BindingModels;
using STOContracts.BusinessLogicsContracts;
using STOContracts.SearchModels;
using STOContracts.ViewModels;
using STODatabaseImplement.Models;
using STODataModels.Models;
using STOEmployer.Models;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace STOEmployer.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

        public static StorekeeperViewModel? Storekeeper { get; set; } = null;

		public readonly IEmployerLogic _employerLogic;
		public readonly IMaintenanceLogic _maintenanceLogic;
		public readonly ICarLogic _carLogic;
		public readonly IServiceLogic _serviceLogic;

		public readonly IStorekeeperLogic _storekeeperLogic;
		public readonly IWorkLogic _workLogic;
		public readonly IWorkDurationLogic _workDurationLogic;
		public readonly ISpareLogic _spareLogic;

		public HomeController(ILogger<HomeController> logger, IEmployerLogic employerLogic, IMaintenanceLogic maintenanceLogic,
			ICarLogic carLogic, IServiceLogic serviceLogic, IStorekeeperLogic storekeeperLogic,
			IWorkDurationLogic workDurationLogic, ISpareLogic spareLogic, IWorkLogic workLogic)
		{
			_logger = logger;
			_employerLogic = employerLogic;
			_spareLogic = spareLogic;
			_carLogic = carLogic;
			_serviceLogic = serviceLogic;
			_storekeeperLogic = storekeeperLogic;
			_maintenanceLogic = maintenanceLogic;
			_workDurationLogic= workDurationLogic;
			_workLogic = workLogic;
		}

		[HttpGet]
		public IActionResult Privacy() {
            if (Storekeeper is not null)
            {
                return Redirect("IndexWork");
            }
            return View();
		}

		[HttpGet]
		public IActionResult Register() {
			return View(Storekeeper);
		}

		[HttpPost]
		public IActionResult Register(string login, string email, string password) {
			_storekeeperLogic.Create(new StorekeeperBindingModel()
			{
				Login = login,
				Email = email,
				Password = password
			});

			return Redirect("~/Home/Privacy");
		}

		[HttpGet]
		public IActionResult Enter() {
			return View();
		}

		[HttpPost]
		public IActionResult Enter(string login, string password) {
			Storekeeper = _storekeeperLogic.ReadElement(new StorekeeperSearchModel()
			{
				Login = login,
				Password = password,
			});

			return Redirect("~/Home/Privacy");
		}

		[HttpGet]
		public IActionResult IndexSpare() {
            if (Storekeeper is null)
            {
                return Redirect("~/Home/Privacy");
            }
            return View(_spareLogic.ReadList(null));
        }

        [HttpGet]
        public IActionResult IndexWork()
        {
            if (Storekeeper is null)
            {
                return Redirect("~/Home/Privacy");
            }
            return View(_workLogic.ReadList(null));
        }


        [HttpGet]
        public IActionResult CreateSpare()
        {
            if (Storekeeper is null)
            {
                return Redirect("~/Home/Privacy");
            }

            return View();
        }

		[HttpPost]
		public IActionResult CreateSpare(string name, int price) {
			_spareLogic.Create(new SpareBindingModel() { 
				Name = name,
				Price = price
			});
			return Redirect("~/Home/IndexSpare");
		}

        [HttpGet]
        public IActionResult CreateWork()
        {
            if (Storekeeper is null)
            {
                return Redirect("~/Home/Privacy");
            }

			CheckboxWorkViewModel model = new CheckboxWorkViewModel();

			model.Maintenance = _maintenanceLogic.ReadList(null).Select(x => new CheckboxViewModel()
			{
				Id = x.Id,
				LabelName = x.Id.ToString(),
				IsChecked = false,
				Count = 0,
				Object = x
			}).ToList();

			model.Spares = _spareLogic.ReadList(null).Select(x => new CheckboxViewModel() {
                Id = x.Id,
                LabelName = x.Name,
                IsChecked = false,
                Count = 0,
                Object = x
            }).ToList();

            return View(model);
        }

		[HttpPost]
		public IActionResult CreateWork(CheckboxWorkViewModel SparesAndMaintenances, int duration, int price, string title) {
			_workDurationLogic.Create(new WorkDurationBindingModel()
			{
				Duration = duration,
			});

			_workLogic.Create(new WorkBindingModel()
			{
				Title = title,
				StorekeeperId = Storekeeper.Id,
				Date = DateTime.Now,
				DurationId = _workDurationLogic.ReadList(null).Last().Id,
				Price = price,
				WorkMaintenances = SparesAndMaintenances.Maintenance.Where(x => x.IsChecked).ToDictionary(x => x.Id, x => (x.Object as IMaintenanceModel, x.Count)),
				WorkSpares = SparesAndMaintenances.Spares.Where(x => x.IsChecked).ToDictionary(x => x.Id, x => (x.Object as ISpareModel, x.Count)),
			}); 

            return Redirect("~/Home/IndexWork");
        }


        [HttpGet]
        public IActionResult Reports()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateExcellReport()
        {
            _workLogic.CreateExcelReport(_workLogic.ReadList(null));
            return Redirect("~/Home/IndexWork");
        }

        [HttpPost]
        public IActionResult CreateWordReport()
        {
            _workLogic.CreateWordReport(_workLogic.ReadList(null));
            return Redirect("~/Home/IndexWork");
        }

        [HttpPost]
        public IActionResult CreatePdfReport(DateTime dateFrom, DateTime dateTo)
        {
            _workLogic.СreateReportPdf(_workLogic.ReadList(new WorkSearchModel()
            {
                DateFrom = dateFrom,
                DateTo = dateTo,
            }), Storekeeper, dateTo, dateFrom);
            return Redirect("~/Home/IndexWork");
        }

    }
}