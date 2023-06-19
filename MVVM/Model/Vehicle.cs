using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Car4You.MVVM.Model.Data
{
    public class Vehicle : IVehicle
    {
        protected double _id;

        public double Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string GetVehicleDescription()
        {
            return $"{Id}";
        }

    }
}
