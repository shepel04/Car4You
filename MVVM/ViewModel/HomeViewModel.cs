using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Car4You.MVVM.Model;
using Car4You.MVVM.Model.Data;

namespace Car4You.MVVM.ViewModel
{
    class HomeViewModel : INotifyPropertyChanged
    {
        private List<Car> allCars = DataWorker.GetAllCars();
        private string carAmount = DataWorker.GetAmountOfAllItems();
        
        public List<Car> AllCars 
        {
            get { return allCars; }
            set 
            { 
                allCars = value;
                OnPropertyChanged(nameof(AllCars));                   
            }
        }

        public string CarAmount
        {
            get { return carAmount; }
            set 
            {
                carAmount = value; 
                OnPropertyChanged(nameof(CarAmount)); 
            }
        }

        

        // Implement the INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
