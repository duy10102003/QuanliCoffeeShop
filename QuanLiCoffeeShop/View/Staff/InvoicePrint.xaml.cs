using System.IO;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;

namespace QuanLiCoffeeShop.View.Staff
{
    /// <summary>
    /// Interaction logic for InvoicePrint.xaml
    /// </summary>
    public partial class InvoicePrint : Window
    {
        public InvoicePrint()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();
            if (printDlg.ShowDialog() == true)
            {
                printDlg.PrintVisual(this, "Bill");
            }
        }
    }
}
