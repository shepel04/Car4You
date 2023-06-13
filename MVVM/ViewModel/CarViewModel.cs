using Car4You.Core;
using Car4You.MVVM.Model;
using Car4You.MVVM.Model.Data;
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
        private Car _selectedCar;
        private ObservableCollection<string> photoUrls;
        private int currentIndex = 0;
        private string currentPhotoUrl = "";
        private string photoUrlBack;

        public string PhotoUrlBack { get; set; }

        private string header;
        
        public string Header
        {
            get { return header; }
            set 
            { 
                header = value;
                OnPropertyChanged(nameof(Header));
            }
        }

        public string Model { get; set; }
        public Car SelectedCar
        {
            get { return _selectedCar; }
            set
            {
                _selectedCar = value;
                OnPropertyChanged(nameof(SelectedCar));
                OnPropertyChanged(nameof(VehicleDescription));
            }
        }
        public string VehicleDescription
        {
            get { return _selectedCar?.ToString(); }
        }

        private bool _isGoodPriceVisible;
        public bool IsGoodPriceVisible
        {
            get { return _isGoodPriceVisible; }
            set
            {
                _isGoodPriceVisible = value;
                OnPropertyChanged(nameof(IsGoodPriceVisible));
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

        





        public CarViewModel(Car selectedCar)
        {
            photoUrls = new ObservableCollection<string>();
            _selectedCar = selectedCar;
            SelectedCar = selectedCar;
            IVehicle vehicle = selectedCar;
            header = vehicle.GetVehicleDescription();
            IPrice priceInterface = selectedCar;
            IsGoodPriceVisible = priceInterface.GetPrice();
            GetUrls(selectedCar);

            DecreaseIndexCommand = new RelayCommand(o =>
            {
                DecreaseIndex();
            });
            IncreaseIndexCommand = new RelayCommand(o =>
            {
                IncreaseIndex();
            });
            
            //ParseImageLinks(selectedCar);
        }

        public CarViewModel()
        {
            


        }

        public ObservableCollection<string> PhotoUrls
        {
            get { return photoUrls; }
            set
            {
                photoUrls = value;
                OnPropertyChanged(nameof(PhotoUrls));
                UpdateCurrentIndex();
            }
        }

        public int CurrentIndex
        {
            get { return currentIndex; }
            set
            {
                currentIndex = value;
                OnPropertyChanged(nameof(CurrentIndex));
                OnPropertyChanged(nameof(CurrentPhotoUrl));
            }
        }

        public string CurrentPhotoUrl
        {
            get
            {                                       
                return currentPhotoUrl;                
            }

            set
            {
                currentPhotoUrl = value;
                OnPropertyChanged(nameof(CurrentPhotoUrl));
            }
        }

        public ICommand DecreaseIndexCommand { get; }

        public ICommand IncreaseIndexCommand { get; }

        private void DecreaseIndex()
        {
            CurrentIndex--;
        }

        private void IncreaseIndex()
        {
            CurrentIndex++;
        }

        private void UpdateCurrentIndex()
        {
            if (PhotoUrls.Count > 0)
                CurrentIndex = 0;
            else
                CurrentIndex = -1;
        }

        private void GetUrls(Car selectedCar)
        {
            string[] urls = selectedCar.Photo.Replace(" ", string.Empty).Split(',');
            foreach (var item in urls)
            {
                photoUrls.Add(item);
            }
            CurrentPhotoUrl = photoUrls[0];
        }
        

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
