using STOContracts.BindingModels;
using STOContracts.ViewModels;
using STODataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STODatabaseImplement.Models
{
    public class Storekeeper : IStorekeeperModel
    {
        public int Id { get; set; }
        [Required]
        public string Login { get; set; } = String.Empty;
        [Required]
        public string Password { get; set; } = String.Empty;
        [Required]
        public string Email { get; set; } = String.Empty;
        [ForeignKey("StorekeeperId")]
        public virtual List<Work> Works { get; set; } = new();


        public static Storekeeper Create(StorekeeperBindingModel model)
        {
            return new Storekeeper()
            {
                Id = model.Id,
                Login = model.Login,
                Password = model.Password,
                Email = model.Email,
            };
        }

        public void Update(StorekeeperBindingModel model)
        {
            Login = model.Login;
            Password = model.Password;
            Email = model.Email;
        }

        public StorekeeperViewModel GetViewModel => new()
        {
            Id = Id,
            Login = Login,
            Email = Email,
            Password = Password
        };
    }
}
