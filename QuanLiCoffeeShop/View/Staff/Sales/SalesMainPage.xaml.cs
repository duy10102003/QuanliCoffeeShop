using QuanLiCoffeeShop.Model;
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
    /// Interaction logic for SalesPage.xaml
    /// </summary>
    public partial class SalesMainPage : Page
    {
        public SalesMainPage()
        {
            InitializeComponent();
        }
        //private void Tab_btn_Click(object sender, RoutedEventArgs e)
        //{
        //    if (sender is Button button)
        //    {
        //        string borderName = button.Tag.ToString();
        //        ResetBorders();
        //        HighlightBorder(borderName);
        //    }
        //}
        //private void ResetBorders()
        //{
        //    SeatBd.Background = new SolidColorBrush(Colors.Transparent);
        //    MenuBd.Background = new SolidColorBrush(Colors.Transparent);
        //}

        //private void HighlightBorder(string borderName)
        //{
        //    switch (borderName)
        //    {
        //        case "SeatBd":
        //            SeatBd.Background = new SolidColorBrush(Colors.White);
        //            break;
        //        case "MenuBd":
        //            MenuBd.Background = new SolidColorBrush(Colors.White);
        //            break;
        //    }
        //}


        private void TabFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (TabFrame.Content is SeatPage)
            {
                SeatBd.Background = new SolidColorBrush(Colors.White);
                MenuBd.Background = new SolidColorBrush(Colors.Transparent);
            }
            else if (TabFrame.Content is ProductPage)
            {
                SeatBd.Background = new SolidColorBrush(Colors.Transparent);
                MenuBd.Background = new SolidColorBrush(Colors.White);
            }
        }
    }
}
