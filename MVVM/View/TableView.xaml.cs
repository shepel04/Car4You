using Car4You.MVVM.Model;
using Car4You.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Car4You.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для TableView.xaml
    /// </summary>
    public partial class TableView : UserControl
    {
        public TableView()
        {
            InitializeComponent();
            DataContext = new TableViewModel();
        }

        private void CarDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (carDataGrid.SelectedItem != null)
            {
                // Получаем выбранный объект Car из свойства SelectedItem
                Car selectedCar = carDataGrid.SelectedItem as Car;

                // Открываем новое окно и передаем информацию из выбранного объекта Car
                var carWindow = new CarWindow(selectedCar);
                carWindow.Show();
            }
        }

    }
}
