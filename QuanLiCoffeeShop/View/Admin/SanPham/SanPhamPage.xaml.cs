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

namespace QuanLiCoffeeShop.View.Admin.SanPham
{
    /// <summary>
    /// Interaction logic for SanPhamPage.xaml
    /// </summary>
    public partial class SanPhamPage : Page
    {
        public SanPhamPage()
        {
            InitializeComponent();
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string borderName = button.Tag.ToString();
                ResetBorders(); 
                HighlightBorder(borderName); 
            }
        }
        private void ResetBorders()
        {
            AllBd.Background = new SolidColorBrush(Colors.White);
            CoffeeBd.Background = new SolidColorBrush(Colors.White);
            FreezeBd.Background = new SolidColorBrush(Colors.White);
            FoodBd.Background = new SolidColorBrush(Colors.White);
            OtherBd.Background = new SolidColorBrush(Colors.White);
            TeaBd.Background = new SolidColorBrush(Colors.White);
        }

        private void HighlightBorder(string borderName)
        {
            switch (borderName)
            {
                case "AllBd":
                    AllBd.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xF4, 0xF4));
                    break;
                case "CoffeeBd":
                    CoffeeBd.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xF4, 0xF4));
                    break;
                case "FreezeBd":
                    FreezeBd.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xF4, 0xF4));
                    break;
                case "FoodBd":
                    FoodBd.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xF4, 0xF4));
                    break;
                case "TeaBd":
                    TeaBd.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xF4, 0xF4));
                    break;
                case "OtherBd":
                    OtherBd.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xF4, 0xF4));
                    break;
            }
        }       
    }
}
