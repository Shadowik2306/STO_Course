using STOContracts.BindingModels;
using STOContracts.ViewModels;
using STODataModels.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STODatabaseImplement.Models
{
    public class Maintenance : IMaintenanceModel
    {
        public int Id { get; set; }
        [Required]
        public int EmployerId { get; set; }
        [Required]
        public double Cost { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }

        private Dictionary<int, (ICarModel, int)> _maintenanceCars = null;
        [NotMapped]
        public Dictionary<int, (ICarModel, int)> MaintenanceCars
        {
            get
            {
                if (_maintenanceCars == null)
                {
                    _maintenanceCars = Cars
                        .ToDictionary(recCM =>  recCM.CarId, recCM => (recCM.Car as ICarModel,recCM.Count));
                }
                return _maintenanceCars;
            }
        }
        [ForeignKey("MaintenanceId")]
        public virtual List<MaintenanceCar> Cars { get; set; } = new();

        public static Maintenance Create(STODatabase context, MaintenanceBindingModel model)
        {
            return new Maintenance()
            {
                Id = model.Id,
                EmployerId = model.EmployerId,
                Cars = model.MaintenanceCars.Select(x => new MaintenanceCar
                {
                    Car = context.Cars.First(y => y.Id == x.Key),
                    Count = x.Value.Item2
                }).ToList(),
                Cost = model.Cost,
                DateCreate = model.DateCreate
            };
        }

        public void Update(MaintenanceBindingModel model)
        {
            Cost = model.Cost;
        }

        public MaintenanceViewModel GetViewModel => new()
        {
            Id = Id,
            EmployerId = EmployerId,
            Cost = Cost,
            DateCreate = DateCreate,
            MaintenanceCars = MaintenanceCars
        };
        public void UpdateComponents(STODatabase context, MaintenanceBindingModel model)
        {
            var MaintenanceCars = context.MaintenanceCars.Where(rec => rec.MaintenanceId == model.Id).ToList();
            if (MaintenanceCars != null && MaintenanceCars.Count > 0)
            {
                context.MaintenanceCars.RemoveRange(MaintenanceCars.Where(rec => !model.MaintenanceCars.ContainsKey(rec.CarId)));
                context.SaveChanges();
                MaintenanceCars = context.MaintenanceCars.Where(rec => rec.MaintenanceId == model.Id).ToList();
                foreach (var updateComponent in MaintenanceCars)
                {
                    updateComponent.Count = model.MaintenanceCars[updateComponent.CarId].Item2;
                    model.MaintenanceCars.Remove(updateComponent.CarId);
                }
                context.SaveChanges();
            }
            var Maintenance = context.Maintenances.First(x => x.Id == Id);
            foreach (var pc in model.MaintenanceCars)
            {
                context.MaintenanceCars.Add(new MaintenanceCar
                {
                    Maintenance = Maintenance,
                    Car = context.Cars.First(x => x.Id == pc.Key),
                    Count = pc.Value.Item2
                });
                context.SaveChanges();
            }
            _maintenanceCars = null;
        }
    }
}
