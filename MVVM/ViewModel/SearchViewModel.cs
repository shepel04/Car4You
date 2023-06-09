using Car4You.Core;
using Car4You.MVVM.Model;
using Car4You.MVVM.Model.Data;
using Car4You.MVVM.Model.DTO;
using Car4You.MVVM.View;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Car4You.MVVM.ViewModel
{
    class SearchViewModel : INotifyPropertyChanged
    {

        private readonly ApplicationContext dbContext;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<CarDTO> searchResults;

        
        public ObservableCollection<string> Brands { get; set; }
        public ObservableCollection<string> Models { get; set; }
        public ObservableCollection<string> Years { get; set; }
        public ObservableCollection<string> Bodies { get; set; }
        public ObservableCollection<string> Fuels { get; set; }
        public ObservableCollection<string> Gears { get; set; }
        public ObservableCollection<string> Drives { get; set; }
        public ObservableCollection<string> Colors { get; set; }


        private int amountOfResults = 0;

        public ObservableCollection<CarDTO> SearchResults
        {
            get { return searchResults; }
            set
            {
                searchResults = value;
                OnPropertyChanged(nameof(SearchResults));
            }
        }

        // Command bound to the search button
        public System.Windows.Input.ICommand SearchCommand { get; set; }
        public ICommand OpenCarWindowCommand { get; private set; }

        // Properties of user search controls
        private string selectedBrand;
        public string SelectedBrand
        {
            get { return selectedBrand; }
            set
            {
                if (selectedBrand != value)
                {
                    selectedBrand = value;
                    OnPropertyChanged(nameof(SelectedBrand));
                    PopulateModels();
                    SortObservableCollection(Models);
                }
            }
        }

        private bool isPanelVisible;
        public bool IsPanelVisible
        {
            get { return isPanelVisible; }
            set
            {
                isPanelVisible = value;
                OnPropertyChanged(nameof(IsPanelVisible));
            }
        }

        private bool isButtonVisible = true;
        public bool IsButtonVisible
        {
            get { return isButtonVisible; }
            set
            {
                isButtonVisible = value;
                OnPropertyChanged(nameof(IsButtonVisible));
            }
        }

        private double listViewTranslateY;
        public double ListViewTranslateY
        {
            get { return listViewTranslateY; }
            set
            {
                listViewTranslateY = value;
                OnPropertyChanged(nameof(ListViewTranslateY));
            }
        }

        public int AmountOfResults
        {
            get { return amountOfResults; }
            set
            {
                amountOfResults = value;
                OnPropertyChanged(nameof(AmountOfResults));
            }
        }

        public string SelectedModel { get; set; }
        public string FromYear { get; set; }
        public string ToYear { get; set; }
        public string FromPrice { get; set; }
        public string ToPrice { get; set; }
        public string SelectedBody { get; set; }
        public string FromMileage { get; set; }
        public string ToMileage { get; set; }
        public string FromVolume { get; set; }
        public string ToVolume { get; set; }
        public string SelectedFuel { get; set; }
        public string FromConsumption { get; set; }
        public string ToConsumption { get; set; }
        public string SelectedGear { get; set; }
        public string SelectedDrive { get; set; }
        public string SelectedColor { get; set; }
        




        public SearchViewModel()
        {
            dbContext = new ApplicationContext();

            SearchResults = new ObservableCollection<CarDTO>();

            // Initialize properties and commands
            Brands = new ObservableCollection<string>();
            Models = new ObservableCollection<string>();
            Years = new ObservableCollection<string>();
            Bodies = new ObservableCollection<string> { "", "Седан", "Хетчбек", "Ліфтбек", "Купе", "Мінівен", "Позашляховик", "Кабріолет", "Універсал" };
            Fuels = new ObservableCollection<string> { "", "Бензин", "Дизель", "Газ/Бензин", "Гібрид", "Електро" };
            Gears = new ObservableCollection<string> { "", "Ручна", "Автомат", "Варіатор", "Типтронік", "Робот" };
            Drives = new ObservableCollection<string> { "", "Передній", "Задній", "Повний" };
            Colors = new ObservableCollection<string> { "", "Чорний", "Білий", "Сірий", "Синій", "Червоний", "Зелений", "Жовтий" };


            PoputateYears();

            PopulateBrands();
            if (Brands != null)
            {
                SortObservableCollection(Brands);
            }



            //PropertyChanged += (sender, e) =>
            //{
            //    if (e.PropertyName == nameof(SelectedBrand))
            //    {
            //        PopulateModels();
            //    }
            //};

            OpenCarWindowCommand = new RelayCommand(o =>
            {
                var carWindow = new CarView();
                carWindow.Show();
            });

            SearchCommand = new RelayCommand(o =>
            {
                TogglePanelVisibility();
                Search();
            });
        }

        private static ObservableCollection<string> SortObservableCollection(ObservableCollection<string> collection)
        {
            List<string> sortedList = new List<string>(collection);
            sortedList.Sort();

            // Очистите коллекцию и добавьте отсортированные элементы обратно в ObservableCollection
            collection.Clear();
            foreach (var item in sortedList)
            {
                collection.Add(item);
            }
            return collection;
        }

        private ICommand togglePanelVisibilityCommand;
        public ICommand TogglePanelVisibilityCommand
        {
            get
            {
                if (togglePanelVisibilityCommand == null)
                {
                    togglePanelVisibilityCommand = new RelayCommand(o =>
                    {
                        TogglePanelVisibility();
                    });
                }
                return togglePanelVisibilityCommand;
            }
        }

        private void TogglePanelVisibility()
        {
            IsPanelVisible = !IsPanelVisible;
            IsButtonVisible = !IsButtonVisible;

            if (IsPanelVisible)
            {
                ListViewTranslateY += 200; // Slide down by 200 pixels
            }
            else
            {
                ListViewTranslateY -= 200; // Slide up by 200 pixels
            }
        }

        private void PoputateYears()
        {
            int startYear = 1900;
            int endYear = 2023;

            Years.Add(string.Empty);
            for (int year = endYear; year >= startYear; year--)
            {
                Years.Add(year.ToString());
            }
        }

        private void PopulateBrands()
        {
            Brands.Clear();
            Brands.Add(string.Empty);
            var brands = dbContext.Cars.Select(c => c.Brand).Distinct().ToList();
            foreach (var brand in brands)
            {
                Brands.Add(brand);
            }
        }


        private void PopulateModels()
        {
            Models.Clear();

            Models.Add(string.Empty);
            if (!string.IsNullOrEmpty(SelectedBrand))
            {
                var models = dbContext.Cars
                    .Where(c => c.Brand == SelectedBrand)
                    .Select(c => c.Model)
                    .Distinct()
                    .ToList();

                foreach (var model in models)
                {
                    Models.Add(model);
                }

            }
        }

        private void Search()
        {

            using (var dbContext = new ApplicationContext())
            {
                var query = dbContext.Cars.AsQueryable();



                if (!string.IsNullOrEmpty(SelectedBrand))
                {
                    query = query.Where(c => c.Brand == SelectedBrand);
                }

                if (!string.IsNullOrEmpty(SelectedModel))
                {
                    query = query.Where(c => c.Model == SelectedModel);
                }

                if (!string.IsNullOrEmpty(FromYear))
                {
                    query = query.Where(c => string.Compare(c.Year, FromYear) >= 0);
                }

                if (!string.IsNullOrEmpty(ToYear))
                {
                    query = query.Where(c => string.Compare(c.Year, ToYear) <= 0);
                }

                // Both prices entered
                if (!string.IsNullOrEmpty(FromPrice) && !string.IsNullOrEmpty(ToPrice))
                {
                    List<CarDTO> filteredCars = new List<CarDTO>();
                    foreach (var car in query)
                    {
                        if (IsValidPriceBoth(car.Price, Int32.Parse(FromPrice), Int32.Parse(ToPrice)))
                        {
                            filteredCars.Add(car);
                        }
                    }
                    query = filteredCars.AsQueryable();
                } // Entered only FromPrice
                else if (!string.IsNullOrEmpty(FromPrice))
                {
                    List<CarDTO> filteredCars = new List<CarDTO>();
                    foreach (var car in query)
                    {
                        if (IsValidPriceFrom(car.Price, Int32.Parse(FromPrice)))
                        {
                            filteredCars.Add(car);
                        }
                    }
                    query = filteredCars.AsQueryable();
                } // Entered only ToPrice
                else if (!string.IsNullOrEmpty(ToPrice))
                {

                    List<CarDTO> filteredCars = new List<CarDTO>();
                    foreach (var car in query)
                    {
                        if (IsValidPriceTo(car.Price, Int32.Parse(ToPrice)))
                        {
                            filteredCars.Add(car);
                        }
                    }
                    query = filteredCars.AsQueryable();
                }

                if (!string.IsNullOrEmpty(SelectedBody))
                {
                    query = query.Where(c => c.Body == SelectedBody);
                }

                if (!string.IsNullOrEmpty(FromMileage))
                {
                    query = query.Where(c => c.Mileage >= Double.Parse(FromMileage));
                }

                if (!string.IsNullOrEmpty(ToMileage))
                {
                    query = query.Where(c => c.Mileage <= Double.Parse(ToMileage));
                }

                if (!string.IsNullOrEmpty(FromVolume))
                {
                    query = query.Where(c => c.Volume >= float.Parse(FromVolume));
                }

                if (!string.IsNullOrEmpty(ToVolume))
                {
                    query = query.Where(c => c.Volume <= float.Parse(ToVolume));
                }

                if (!string.IsNullOrEmpty(SelectedFuel))
                {
                    query = query.Where(c => string.Compare(c.Fuel.Replace(" ", string.Empty), SelectedFuel) == 0);
                }

                if (!string.IsNullOrEmpty(FromConsumption))
                {
                    query = query.Where(c => c.Consumption >= float.Parse(FromConsumption));
                }

                if (!string.IsNullOrEmpty(ToConsumption))
                {
                    query = query.Where(c => c.Consumption <= float.Parse(ToConsumption));
                }

                if (!string.IsNullOrEmpty(SelectedGear))
                {
                    query = query.Where(c => c.Gear == SelectedGear);
                }

                if (!string.IsNullOrEmpty(SelectedDrive))
                {
                    query = query.Where(c => c.Drive == SelectedDrive);
                }

                if (!string.IsNullOrEmpty(SelectedColor))
                {
                    query = query.Where(c => c.Color == SelectedColor);
                }
                var results = new List<CarDTO>();
                // Retrieve the search results from the database
                //foreach (var item in query)
                //{
                //    results.Add(item);
                //}
                results = query.ToList();

                // Update the SearchResults collection with the retrieved results
                SearchResults.Clear();
                foreach (var car in results)
                {
                    SearchResults.Add(car);
                }
                AmountOfResults = SearchResults.Count;
            }

        }

        bool IsValidPriceBoth(string priceString, int priceFrom, int priceTo)
        {
            if (int.TryParse(priceString.Replace(" ", string.Empty).Replace("$", string.Empty), out int productPrice))
            {
                return productPrice >= priceFrom && productPrice <= priceTo;
            }
            else
            {
                return false;
            }
        }

        bool IsValidPriceFrom(string priceString, int price)
        {
            if (int.TryParse(priceString.Replace(" ", string.Empty).Replace("$", string.Empty), out int productPrice))
            {
                return productPrice >= price;
            }
            else
            {

                return false;
            }
        }
    
        // Separate method to validate the price format and compare with the given price
        bool IsValidPriceTo(string priceString, int price)
        {
            if (int.TryParse(priceString.Replace(" ", string.Empty).Replace("$", string.Empty), out int productPrice))
            {
                return productPrice <= price;
            }
            else
            {
                // Handle invalid price format for c.Price
                // For example, exclude products with invalid prices from the query
                return false;
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
