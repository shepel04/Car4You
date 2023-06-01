﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Car4You.MVVM.Model;
using Car4You.MVVM.Model.DTO;

namespace Car4You.MVVM.ViewModel
{
    class HomeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<CarDTO> _cars;
        public ObservableCollection<CarDTO> Cars
        {
            get { return _cars; }
            set
            {
                _cars = value;
                OnPropertyChanged();
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