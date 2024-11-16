using QuanLiCoffeeShop.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace QuanLiCoffeeShop.View.Admin.StaffManagement
{
    /// <summary>
    /// Interaction logic for StaffPage.xaml
    /// </summary>
    /// 
    public partial class StaffPage : Page
    {
        public StaffPage()
        {
            InitializeComponent();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as StaffViewModel).OpenEditStaffCommand.Execute(new object());
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as StaffViewModel).DeleteStaffCommand.Execute(new object());
        }
    }
}
