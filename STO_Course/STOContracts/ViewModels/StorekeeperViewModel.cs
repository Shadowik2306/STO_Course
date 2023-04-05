using STODataModels.Models;
using System.ComponentModel;

namespace STOContracts.ViewModels
{
    public class StorekeeperViewModel : IStorekeeperModel
    {
        public int Id { get; set; }
        [DisplayName("Логин")]
        public string Login { get; set; } = String.Empty;
        [DisplayName("Пароль")]
        public string Password { get; set; } = String.Empty;
        [DisplayName("Почта")]
        public string Email { get; set; } = String.Empty;
    }
}
