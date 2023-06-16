using Car4You.MVVM.Model.Data;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car4You.MVVM.Model
{
    public class Car : Vehicle, IPrice, IVehicle    
    {        
        private string? _brand;
        private string? _model;
        private string? _year;
        private string? _price;
        private string? _body;
        private double? _mileage;
        private double? _volume;
        private string? _fuel;
        private double? _consumption;
        private string? _gear;
        private string? _drive;
        private string? _color;
        private string? _photo;
        private string? _url;
                
           
        
        public string? Brand
        {
            get { return _brand; }
            set { _brand = value; }
        }
        public string? Model
        {
            get { return _model.Replace("/", string.Empty); }
            set { _model = value; }
        }
        public string? Year
        {
            get { return _year; }
            set { _year = value; }
        }
        public string? Price
        {
            get 
            {
                if (_price.Contains("грн"))
                {
                    double convertedPrice = Double.Parse(_price.Replace(" ", string.Empty).Replace("грн", string.Empty)) / 37.2;
                    _price = $"{(int)convertedPrice} $";
                    return _price;
                }
                else if (_price.Contains("€"))
                {
                    double convertedPrice = Double.Parse(_price.Replace(" ", string.Empty).Replace("€", string.Empty)) * 1.0697;
                    _price = $"{(int)convertedPrice} $";
                    return _price;
                }
                else
                    return _price;
            }
            set 
            {
                if (value.Contains("грн"))
                {
                    double convertedPrice = Double.Parse(value.Replace(" ", string.Empty).Replace("грн", string.Empty)) / 37.2;
                    _price = $"{convertedPrice} $";
                }
                else if (value.Contains("€"))
                {
                    double convertedPrice = Double.Parse(value.Replace(" ", string.Empty).Replace("€", string.Empty)) * 1.0697;
                    _price = $"{convertedPrice} $";
                }
                else
                _price = value;
            }
        }
        public string? Body
        {
            get { return _body; }
            set { _body = value; }
        }
        public double? Mileage
        {
            get { return _mileage; }
            set { _mileage = value; }
        }
        public double? Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }
        public string? Fuel
        {
            get { return _fuel; }
            set { _fuel = value; }
        }
        public double? Consumption
        {
            get { return _consumption; }
            set { _consumption = value; }
        }
        public string? Gear
        {
            get { return _gear; }
            set { _gear = value; }
        }
        public string? Drive
        {
            get { return _drive; }
            set { _drive = value; }
        }
        public string? Color
        {
            get { return _color; }
            set { _color = value; }
        }
        public string? Photo
        {
            get { return _photo; }
            set { _photo = value; }
        }
        public string? Url
        {
            get { return _url; }
            set { _url = value; }
        }
                
        public string GetFuelType() 
        {
            return Fuel ?? "Unknown";
        }

        public bool GetPrice()
        {
            int clearPrice = 0;
            if (Year.Contains("$"))
            {
                clearPrice = Int32.Parse(Year.Replace(" ", string.Empty).Replace("$", string.Empty));
            }
            else if (Year.Contains("€"))
            {
                clearPrice = Int32.Parse(Year.Replace(" ", string.Empty).Replace("€", string.Empty));
            }
            else if (Year.Contains("грн"))
            {
                clearPrice = Int32.Parse(Year.Replace(" ", string.Empty).Replace("грн", string.Empty));
            }

            if (Int32.Parse(Year) >= 2000 && Int32.Parse(Year) <= 2006 && clearPrice < 5000)
            {
                return true;
            }
            else if (Int32.Parse(Year) >= 2006 && Int32.Parse(Year) <= 2010 && clearPrice < 7000)
            {
                return true;
            }
            {
                return false;
            }
        }

        public string GetVehicleDescription()
        {
            return $"{Brand} {Model} {Year}";
        }

    }
}
