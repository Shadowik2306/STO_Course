namespace STODataModels.Models
{
    public interface IEmployerModel : IId
    {
        string Login { get; }

        string Password { get; }
        string Email { get; }
    }
}
