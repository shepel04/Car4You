using Car4You.Core;
using Car4You.MVVM.Model;
using Car4You.MVVM.Model.Data;
using Car4You.MVVM.Model.DTO;
using Car4You.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace Car4You.MVVM.ViewModel
{
    class CarViewModel : INotifyPropertyChanged
    {
        private CarDTO _selectedCar;        
        public string Model { get; set; }
        public CarDTO SelectedCar
        {
            get { return _selectedCar; }
            set
            {
                _selectedCar = value;
                OnPropertyChanged(nameof(SelectedCar));
            }
        }
               

        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<string> _carImages;

        public ObservableCollection<string> CarImages
        {
            get { return _carImages; }
            set
            {
                if (_carImages != value)
                {
                    _carImages = value;
                    OnPropertyChanged(nameof(CarImages));
                }
            }
        }

        public CarViewModel(CarDTO selectedCar)
        {
            SelectedCar = selectedCar;
            
            //ParseImageLinks(selectedCar);
        }

        

        public CarViewModel()
        {
            
        }


        //private void ParseImageLinks(CarDTO selectedCar)
        //{
        //    string[] images = selectedCar.Photo.Replace(" ", string.Empty).Split(",");
        //    foreach (string image in images) 
        //    {
        //        Photo.Add(image);
        //    }
        //}



        //private void Next()
        //{
        //    CurrentIndex++;
        //    if (CurrentIndex >= Items.Count)
        //    {
        //        CurrentIndex = 0;
        //    }
        //}

        //private void Previous()
        //{
        //    CurrentIndex--;
        //    if (CurrentIndex < 0)
        //    {
        //        CurrentIndex = Items.Count - 1;
        //    }
        //}

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
