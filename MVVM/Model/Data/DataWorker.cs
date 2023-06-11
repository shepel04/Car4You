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
        public static List<Car> GetAllCars()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Cars.ToList();
            }
        }

        public static string GetAmountOfAllItems()
        {
            using (var db = new ApplicationContext())
            {
                int itemCount = db.Cars.Count();
                return itemCount.ToString();
            }
        }
        
    }
}
