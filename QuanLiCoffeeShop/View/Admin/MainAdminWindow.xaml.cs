using QuanLiCoffeeShop.ViewModel.AdminVM;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace QuanLiCoffeeShop.View.Admin
{
    /// <summary>
    /// Interaction logic for MainAdminWindow.xaml
    /// </summary>
    public partial class MainAdminWindow : Window
    {
        public MainAdminWindow()
        {
            InitializeComponent();
        }

        private void Overlay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BeginStoryboard((Storyboard)Resources["MenuClose"]);
        }

        private void AdminWD_Closed(object sender, System.EventArgs e)
        {
            this.Owner.Visibility = Visibility.Visible;
        }
    }
}
