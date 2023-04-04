namespace STODataModels.Models
{
    public interface IServiceModel : IId
    {
        string ServiceDescription { get; }
        int CarId { get; }
    }
}
