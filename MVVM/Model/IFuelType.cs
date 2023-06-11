using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car4You.MVVM.Model
{
    public interface IFuelType
    {
        string? Fuel { get; set; }
        string GetFuelType();
    }
}
