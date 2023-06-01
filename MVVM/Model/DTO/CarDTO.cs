using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car4You.MVVM.Model.DTO
{
    public class CarDTO
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }
        public string? Price { get; set; }
        public string? Body { get; set; }
        public int? Mileage { get; set; }
        public double? Engine { get; set; }
        public string? Fuel { get; set; }
        public double? Consumption { get; set; }
        public string? Gear { get; set; }
        public string? Drive { get; set; }
        public string? Color { get; set; }
        public string? Photo { get; set; }
        public string? Url { get; set; }
    }
}
