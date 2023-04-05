namespace STODataModels.Models
{
    public interface ICarModel : IId
    {
        string Brand { get; }
        string Model { get; }
        string VIN {  get; }
        Dictionary<int, (ISpareModel, int)> CarSpares { get; }
    }
}
