using QuanLiCoffeeShop.ViewModel.MessageBoxVM;
using System.Security.Policy;
using System.Security.RightsManagement;
using System.Windows;
using System.Windows.Input;

namespace QuanLiCoffeeShop.View.MessageBox
{
    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class Success : Window
    {

        public Success(string text)
        {
            InitializeComponent(); 
            DataContext = new MessageBoxViewModel(text);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Ok_btn_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}
