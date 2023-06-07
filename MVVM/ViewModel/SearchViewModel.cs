using Car4You.Core;
using Car4You.MVVM.Model;
using Car4You.MVVM.Model.Data;
using Car4You.MVVM.Model.DTO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Car4You.MVVM.ViewModel
{
    class SearchViewModel : INotifyPropertyChanged
    {

        private readonly ApplicationContext dbContext;

        public event PropertyChangedEventHandler? PropertyChanged;

        // Properties bound to the controls in your view
        public List<string> Brands { get; set; }
        public List<string> Models { get; set; }
        public List<string> Years { get; set; }
        public List<string> Bodies { get; set; }
        public List<string> Fuels { get; set; }
        public List<string> Gears { get; set; }
        public List<string> Drives { get; set; }
        public List<string> Colors { get; set; }

        // Add more properties for other search parameters

        // Collection bound to the data grid to display search results
        public List<CarDTO> SearchResults { get; set; }

        // Command bound to the search button
        public System.Windows.Input.ICommand SearchCommand { get; set; }

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
                    Models.Sort();
                }
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
            dbContext = new ApplicationContext(); // Replace with your DbContext class

            // Initialize properties and commands
            Brands = new List<string>();
            Models = new List<string>();
            Years = new List<string>();
            Bodies = new List<string> { "Седан", "Хетчбек", "Ліфтбек", "Купе", "Мінівен", "Позашляховик", "Кабріолет", "Універсал" };
            Fuels = new List<string> { "Бензин", "Дизель", " Газ / Бензин", "Гібрид", "Електро" };
            Gears = new List<string> { "Ручна", "Автомат", "Варіатор", "Типтронік", "Робот" };
            Drives = new List<string> { "Передній", "Задній", "Повний" };
            Colors = new List<string> { "Чорний", "Білий", "Сірий", "Синій", "Червоний", "Зелений", "Жовтий" }; 
            SearchResults = new List<CarDTO>();

            PoputateYears();          
            
            PopulateBrands();
            if (Brands != null)
            {
                Brands.Sort();
            }

            Brands.Add(string.Empty);

            //PropertyChanged += (sender, e) =>
            //{
            //    if (e.PropertyName == nameof(SelectedBrand))
            //    {
            //        PopulateModels();
            //    }
            //};

            // Initialize the SearchCommand with a RelayCommand or another implementation of ICommand
            SearchCommand = new RelayCommand(o =>
            {
                Search();
            });
        }

        private void PoputateYears()
        {
            int startYear = 1900;
            int endYear = 2023;
           
            for (int year = endYear; year >= startYear; year--)
            {
                Years.Add(year.ToString());
            }

            Years.Add(string.Empty);

            
        }

        private async Task PopulateBrands()
        {
            Brands.Clear();
            var brands = await Task.Run(() => dbContext.Cars.Select(c => c.Brand).Distinct().ToList());
            foreach (var brand in brands)
            {
                Brands.Add(brand);
            }
        }
        

        private void PopulateModels()
        {
            Models.Clear();

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

                Models.Add(string.Empty);
            }
        }

        private void Search()
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

            if (!string.IsNullOrEmpty(FromPrice))
            {
                query = query.Where(c => string.Compare(c.Price, FromPrice) >= 0);
            }

            if (!string.IsNullOrEmpty(ToPrice))
            {
                query = query.Where(c => string.Compare(c.Price, ToPrice) <= 0);
            }

            // Retrieve the search results from the database
            var results = query.ToList();

            // Update the SearchResults collection with the retrieved results
            SearchResults.Clear();
            foreach (var car in results)
            {
                SearchResults.Add(car);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
