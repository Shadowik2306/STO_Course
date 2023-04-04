using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STODataModels.Models
{
    public interface IWorkModel : IId
    {
        string Title { get; }

        double Price { get; }

        double StorekeeperId { get; }

        Dictionary<int, (ISpareModel, int)> WorkSpares { get; }
    }
}
