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
using Car4You.MVVM.Model;
using Car4You.MVVM.ViewModel;

namespace Car4You.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            DataContext = new HomeViewModel();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Get the selected CarDTO instance
            var selectedCar = (sender as ListViewItem).Content as Car;

            // Create an instance of CarView
            CarWindow carView = new CarWindow(selectedCar);
            
            // Show the CarView window
            carView.Show();
        }
    }
}
