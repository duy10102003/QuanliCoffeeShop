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

namespace QuanLiCoffeeShop.View.Admin.Table.Table_page_main
{
    /// <summary>
    /// Interaction logic for Page_main.xaml
    /// </summary>
    public partial class Page_main : Page
    {
       
        public Page_main()
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
           Button Btn = (Button)sender;
            reset();
            ControlTemplate newTemplate = FindResource("BtnClick") as ControlTemplate;
            Btn.Template = newTemplate;
        }
        void reset()
        {
            ControlTemplate Template = FindResource("BtnDefault") as ControlTemplate;
            BtnAll.Template = Template;
            BtnBooked.Template = Template;
            BtnEmpty.Template = Template;
            BtnRepair.Template = Template;
        }
    }

}
