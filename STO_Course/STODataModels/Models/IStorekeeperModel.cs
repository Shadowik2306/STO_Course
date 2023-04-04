using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STODataModels.Models
{
    public interface IStorekeeperModel : IId
    {
        string Login { get; }

        string Password { get; }
        string Email { get; }
    }
}
