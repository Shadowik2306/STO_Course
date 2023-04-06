using STOContracts.BindingModels;
using STOContracts.ViewModels;
using STODataModels.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STODatabaseImplement.Models
{
    public class Employer : IEmployerModel
    {
        public int Id { get; set; }
        [Required]
        public string Login { get; set; } = String.Empty;
        [Required]
        public string Password { get; set; } = String.Empty;
        [Required]
        public string Email { get; set; } = String.Empty;
        [ForeignKey("EmployerId")]
        public virtual List<Maintenance> Maintenances { get; set; } = new();


        public static Employer Create(StorekeeperBindingModel model)
        {
            return new Employer()
            {
                Id = model.Id,
                Login = model.Login,
                Password = model.Password,
                Email = model.Email,
            };
        }

        public void Update(EmployerBindingModel model)
        {
            Login = model.Login;
            Password = model.Password;
            Email = model.Email;
        }

        public EmployerViewModel GetViewModel => new()
        {
            Id = Id,
            Login = Login,
            Email = Email,
            Password = Password
        };
    }
}
