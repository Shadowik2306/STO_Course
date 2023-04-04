namespace STODataModels.Models
{
    public interface ICarModel : IId
    {
        string Brand { get; }
        string Model { get; }
    }
}
