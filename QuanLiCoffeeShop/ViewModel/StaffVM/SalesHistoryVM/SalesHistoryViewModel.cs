using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.Model;
using QuanLiCoffeeShop.Model.Service;
using QuanLiCoffeeShop.Utils;
using QuanLiCoffeeShop.View.Staff.Sales;
using QuanLiCoffeeShop.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Microsoft.Win32;

namespace QuanLiCoffeeShop.ViewModel.StaffVM.SalesHistoryVM
{
    public partial class SalesHistoryViewModel:BaseViewModel
    {
        private DateTime _selectedDateFrom;
        public DateTime SelectedDateFrom
        {
            get { return _selectedDateFrom; }
            set { _selectedDateFrom = value; OnPropertyChanged(); }
        }
        private DateTime _selectedDateTo;
        public DateTime SelectedDateTo
        {
            get { return _selectedDateTo; }
            set { _selectedDateTo = value; OnPropertyChanged(); }
        }
        public ICommand FirstLoadCM { get; set; }
        public ICommand CloseWdCM { get; set; }
        public ICommand HistoryCM { get; set; }
        public ICommand FavorCM { get; set; }
        public ICommand InfoBillCM { get; set; }
        public ICommand DeleteBillCM { get; set; }
        public ICommand DateChange { get; set; }
        public SalesHistoryViewModel()
        {

        }
    }
}
