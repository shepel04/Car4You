using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car4You.Core;

namespace Car4You.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }

        public RelayCommand SearchViewCommand { get; set; }

        public RelayCommand SettingsViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public SearchViewModel SearchVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
            

        }
        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            SearchVM = new SearchViewModel();
            SettingsVM = new SettingsViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            SearchViewCommand = new RelayCommand(o =>
            {
                CurrentView = SearchVM;
            });

            SettingsViewCommand = new RelayCommand(o =>
            {
                CurrentView = SettingsVM;
            });
        }
    }
}
