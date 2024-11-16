using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Animation;
namespace QuanLiCoffeeShop.View.Staff
{
    /// <summary>
    /// Interaction logic for MainStaffWindow.xaml
    /// </summary>
    public partial class MainStaffWindow : Window
    {
        public MainStaffWindow()
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Excuted");
        }
    }
}
