using Car4You.MVVM.Model.DTO;
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
    /// Логика взаимодействия для SearchView.xaml
    /// </summary>
    public partial class SearchView : UserControl
    {
        public SearchView()
        {
            InitializeComponent();
            DataContext = new SearchViewModel();            
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Get the selected CarDTO instance
            var selectedCar = (sender as ListViewItem).Content as CarDTO;

            // Create an instance of CarView
            CarWindow carView = new CarWindow(selectedCar);

            // Assign the selected CarDTO as the DataContext of CarView
            carView.DataContext = selectedCar;

            // Show the CarView window
            carView.Show();
        }
    }
}
