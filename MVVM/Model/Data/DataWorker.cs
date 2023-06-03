using Car4You.MVVM.Model.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Car4You.MVVM.Model.Data
{
    public static class DataWorker
    {
        public static List<CarDTO> GetAllCars()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Cars.ToList();
            }
        }
    }
}
