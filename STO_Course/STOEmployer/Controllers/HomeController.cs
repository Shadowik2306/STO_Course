using BankYouBankruptContracts.ViewModels.Client.Reports;
using Microsoft.AspNetCore.Mvc;
using STOBusinessLogic.BusinessLogics;
using STOContracts.BindingModels;
using STOContracts.BusinessLogicsContracts;
using STOContracts.SearchModels;
using STOContracts.ViewModels;
using STODataModels.Models;
using STOEmployer.Models;
using System.Diagnostics;

namespace STOEmployer.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

        public static EmployerViewModel? Employer { get; set; } = null;

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
            if (Employer is not null)
            {
                return Redirect("IndexMaintenance");
            }
            return View();
		}

		[HttpGet]
		public IActionResult Register() {
			return View(Employer);
		}

		[HttpPost]
		public IActionResult Register(string login, string email, string password) {
			_employerLogic.Create(new EmployerBindingModel()
			{
				Login = login,
				Email = email,
				Password = password
			});

			return Redirect("Privacy");
		}

		[HttpGet]
		public IActionResult Enter() {
			return View();
		}

		[HttpPost]
		public IActionResult Enter(string login, string password) {
			Employer = _employerLogic.ReadElement(new EmployerSearchModel()
			{
				Login = login,
				Password = password,
			});

			return Redirect("Privacy");
		}

		[HttpGet]
		public IActionResult IndexMaintenance() {
            if (Employer is null)
            {
                return Redirect("Privacy");
            }
            return View(_maintenanceLogic.ReadList(null));
        }

        [HttpGet]
        public IActionResult IndexCar()
        {
            if (Employer is null)
            {
                return Redirect("Privacy");
            }
            return View(_carLogic.ReadList(null));
        }


        [HttpGet]
        public IActionResult CreateMaintenance()
        {
            if (Employer is null)
            {
                return Redirect("Privacy");
            }

			ViewBag.Car = _carLogic.ReadList(new CarSearchModel()).Select(x => new CheckboxViewModel()
			{
				Id = x.Id,
				LabelName = x.Model + " " + x.Brand,
				IsChecked = false
			});
            return View();
        }

        [HttpGet]
        public IActionResult CreateCar()
        {
            if (Employer is null)
            {
                return Redirect("Privacy");
            }
            return View();
        }
    }
}