using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STODataModels.Models
{
    public interface ISpareModel : IId
    {
        string Name { get; }

        double Price { get; }
    }
}
