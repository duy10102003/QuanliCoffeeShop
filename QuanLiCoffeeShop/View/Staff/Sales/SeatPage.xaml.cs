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

namespace QuanLiCoffeeShop.View.Staff.Sales
{
    /// <summary>
    /// Interaction logic for SeatPage.xaml
    /// </summary>
    public partial class SeatPage : Page
    {
        public SeatPage()
        {
            InitializeComponent();

        }
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void ListViewItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
