using STOContracts.BindingModels;
using STOContracts.ViewModels;
using STODataModels.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STODatabaseImplement.Models
{
    public class Car : ICarModel
    {
        public int Id { get; set; }
        [Required]
        public string Brand { get; set; } =string.Empty;
        [Required]
        public string Model { get; set; } = string.Empty;
        [Required]
        public string VIN { get; set; } = string.Empty;

        [ForeignKey("CarId")]
        public virtual List<Service> Services { get; set; } = new();

        private Dictionary<int, (ISpareModel, int)>? _carSpares = null;
        [NotMapped]
        public Dictionary<int, (ISpareModel, int)> CarSpares
        {
            get
            {
                if (_carSpares == null)
                {
                    _carSpares = Spares.ToDictionary(recCS => recCS.SpareId, recCS => (recCS.Spare as ISpareModel, recCS.Count));
                }
                return _carSpares;
            }
        }
        [ForeignKey("CarId")]
        public virtual List<CarSpare> Spares { get; set; } = new();
        public static Car Create(STODatabase context, CarBindingModel model)
        {
            return new Car()
            {
                Id = model.Id,
                Brand = model.Brand,
                Model = model.Model,
                VIN = model.VIN,
                Spares = model.CarSpares.Select(x => new CarSpare
                {
                    Car = context.Cars.First(y => y.Id == x.Key),
                    Count = x.Value.Item2
                }).ToList()
            };
        }
        public CarViewModel GetViewModel => new()
        {
            Id = Id,
            Brand = Brand,
            Model = Model,
            VIN = VIN,
            CarSpares = CarSpares
        };
        public void UpdateComponents(STODatabase context, CarBindingModel model)
        {
            var CarSpares = context.CarSpares.Where(recCS => recCS.CarId == model.Id).ToList();
            if (CarSpares != null && CarSpares.Count > 0)
            {
                context.CarSpares.RemoveRange(CarSpares.Where(rec => !model.CarSpares.ContainsKey(rec.CarId)));
                context.SaveChanges();
                CarSpares = context.CarSpares.Where(rec => rec.CarId == model.Id).ToList();
                foreach (var updateComponent in CarSpares)
                {
                    updateComponent.Count = model.CarSpares[updateComponent.CarId].Item2;
                    model.CarSpares.Remove(updateComponent.CarId);
                }
                context.SaveChanges();
            }
            var Car = context.Cars.First(x => x.Id == Id);
            foreach (var pc in model.CarSpares)
            {
                context.CarSpares.Add(new CarSpare
                {
                    Car = Car,
                    Spare = context.Spares.First(x => x.Id == pc.Key),
                    Count = pc.Value.Item2
                });
                context.SaveChanges();
            }
            _carSpares = null;
        }
    }
}
