using Car4You.Core;
using Car4You.MVVM.Model;
using Car4You.MVVM.Model.Data;
using Car4You.MVVM.Model.DTO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
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
                    PopulateModels(); // Call the PopulateModels method when selected brand changes
                }
            }
        }
        public string SelectedModel { get; set; }
        public string FromYear { get; set; }
        public string ToYear { get; set; }



        public SearchViewModel()
        {
            dbContext = new ApplicationContext(); // Replace with your DbContext class

            // Initialize properties and commands
            Brands = new List<string>();
            Models = new List<string>();
            SearchResults = new List<CarDTO>();

            PopulateBrands();

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

        private void PopulateBrands()
        {
            Brands.Clear();
            var brands = dbContext.Cars.Select(c => c.Brand).Distinct().ToList();
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
                query = query.Where(c => Int32.Parse(c.Year) >= Int32.Parse(FromYear));
            }

            if (!string.IsNullOrEmpty(ToYear))
            {
                query = query.Where(c => Int32.Parse(c.Year) <= Int32.Parse(ToYear));
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
