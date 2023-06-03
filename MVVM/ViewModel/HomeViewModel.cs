using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Car4You.MVVM.Model;
using Car4You.MVVM.Model.Data;
using Car4You.MVVM.Model.DTO;

namespace Car4You.MVVM.ViewModel
{
    class HomeViewModel : INotifyPropertyChanged
    {
        private List<CarDTO> allCars = DataWorker.GetAllCars();

        public List<CarDTO> AllCars 
        {
            get { return allCars; }
            set 
            { 
                allCars = value;
                OnPropertyChanged(nameof(AllCars));                   
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
