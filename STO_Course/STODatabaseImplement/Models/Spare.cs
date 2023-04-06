using STOContracts.BindingModels;
using STOContracts.ViewModels;
using STODataModels.Models;
using System.ComponentModel.DataAnnotations;

namespace STODatabaseImplement.Models
{
    public class Spare : ISpareModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public double Price { get; set; }

        public static Spare Create(SpareBindingModel model)
        {
            return new Spare
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
            };
        }

        public void Update(SpareBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            Name = model.Name;
        }

        public SpareViewModel GetViewModel => new()
        {
            Id = Id,
            Name = Name,
            Price = Price,
        };
    }
}
