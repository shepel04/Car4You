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
using System.Windows.Shapes;

namespace Car4You.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для CarWindow.xaml
    /// </summary>
    public partial class CarWindow : Window
    {
        private bool isDragging = false;
        private Point startDragPoint;


        public CarWindow(CarDTO selectedCar)
        {
            InitializeComponent();
            DataContext = new CarViewModel();
        }
        public CarWindow()
        {
                       
        }

        public void UpdateCarImageSource(string imageUrl)
        {
            carImage.Source = new BitmapImage(new Uri(imageUrl));
        }

        private void Chip_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void DatabaseConnection()
        //{
        //    string mySqlCon = "server=127.0.0.1;user=root;database=cars_db;password=;";
        //    MySqlConnection mySqlConnection = new MySqlConnection(mySqlCon);
        //    try
        //    {
        //        mySqlConnection.Open();
        //        MessageBox.Show("Connection success");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);    
        //    }
        //    finally 
        //    {
        //        mySqlConnection.Close();
        //    }
        //}


        private void TopPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                isDragging = true;
                startDragPoint = e.GetPosition(this);
            }
        }

        private void TopPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(this);
                double deltaX = currentPosition.X - startDragPoint.X;
                double deltaY = currentPosition.Y - startDragPoint.Y;
                this.Left += deltaX;
                this.Top += deltaY;
            }
        }

        private void TopPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                isDragging = false;
            }
        }

        private void CloseWindowIcon_Click(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinMaxWindowIcon_Click(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void CollapseWindowIcon_Click(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
