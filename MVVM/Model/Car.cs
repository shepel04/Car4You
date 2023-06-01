using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car4You.MVVM.Model
{
    class Car
    {
        private int _id;
        private string? _brand;
        private string? _model;
        private int? _year;
        private string? _price;
        private string? _body;
        private int? _mileage;
        private double? _engine;
        private string? _fuel;
        private double? _consumption;
        private string? _gear;
        private string? _drive;
        private string? _color;
        private string? _photo;
        private string? _url;

        
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        

        public string? Brand
        {
            get { return _brand; }
            set { _brand = value; }
        }
        public string? Model
        {
            get { return _model; }
            set { _model = value; }
        }
        public int? Year
        {
            get { return _year; }
            set { _year = value; }
        }
        public string? Price
        {
            get { return _price; }
            set { _price = value; }
        }
        public string? Body
        {
            get { return _body; }
            set { _body = value; }
        }
        public int? Mileage
        {
            get { return _mileage; }
            set { _mileage = value; }
        }
        public double? Engine
        {
            get { return _engine; }
            set { _engine = value; }
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







    }
}
