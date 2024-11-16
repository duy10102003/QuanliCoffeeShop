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

namespace QuanLiCoffeeShop.View.Staff.SalesHistory
{
    /// <summary>
    /// Interaction logic for SalesHistory.xaml
    /// </summary>
    public partial class SalesHistory : Page
    {
        public SalesHistory()
        {
            InitializeComponent();
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
            HistoryBd.Background = new SolidColorBrush(Colors.White);           
            FavorBd.Background = new SolidColorBrush(Colors.White);
        }

        private void HighlightBorder(string borderName)
        {
            switch (borderName)
            {
                case "HistoryBd":
                    HistoryBd.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xF4, 0xF4));
                    break;               
                case "FavorBd":
                    FavorBd.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xF4, 0xF4));
                    break;
            }
        }
    }
}
