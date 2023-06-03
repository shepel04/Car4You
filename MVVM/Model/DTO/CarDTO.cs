using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car4You.MVVM.Model.DTO
{
    public class CarDTO
    {
        public double Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Year { get; set; }
        public string? Price { get; set; }
        public string? Body { get; set; }
        public double? Mileage { get; set; }
        public double? Volume { get; set; }
        public string? Fuel { get; set; }
        public double? Consumption { get; set; }
        public string? Gear { get; set; }
        public string? Drive { get; set; }
        public string? Color { get; set; }
        public string? Photo { get; set; }
        public string? Url { get; set; }

        public override string ToString()
        {
            return $"{Brand} {Model} {Year}";
        }
    }
}
