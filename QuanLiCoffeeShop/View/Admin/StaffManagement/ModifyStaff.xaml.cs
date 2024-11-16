using QuanLiCoffeeShop.View.MessageBox;
using QuanLiCoffeeShop.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiCoffeeShop.View.Admin.StaffManagement
{
    /// <summary>
    /// Interaction logic for ModifyStaff.xaml
    /// </summary>
    public partial class ModifyStaff : Window
    {
        public ModifyStaff()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(wageTextBox.Text))
                {
					System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("vi-VN");
					var valueBefore = Int64.Parse(wageTextBox.Text, System.Globalization.NumberStyles.AllowThousands);

					string formattedValue = valueBefore.ToString("#,##0", culture);

					wageTextBox.Text = formattedValue;
					wageTextBox.Select(wageTextBox.Text.Length, 0);
                }
            }
            catch (Exception)
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Lương không hợp lệ");
            }
        }
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!((e.Key >= Key.D0 && e.Key <= Key.D9) ||  // Số từ 0 đến 9
            (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||  // Số từ bàn phím số
            e.Key == Key.Delete ||  // Phím xóa
            e.Key == Key.Back ||  // Phím backspace
            (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.A)))
            {
                e.Handled = true; // Ngăn chặn ký tự nếu không phải số từ bàn phím
            }
        }
    }

}
