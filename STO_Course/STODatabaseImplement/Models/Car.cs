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

        private Dictionary<int, (ISpareModel, int)>? _carSpares = null;
        [NotMapped]
        public Dictionary<int, (ISpareModel, int)> CarSpares => throw new NotImplementedException();

    }
}
