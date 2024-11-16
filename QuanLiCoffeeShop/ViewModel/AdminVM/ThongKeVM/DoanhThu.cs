using QuanLiCoffeeShop.View.Admin;
using System;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows;
using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.View.Admin.ThongKe.DoanhThu;
using QuanLiCoffeeShop.View.Admin.ThongKe;
using QuanLiCoffeeShop.Model;
using QuanLiCoffeeShop.Model.Service;
using QuanLiCoffeeShop.View.MessageBox;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using LiveCharts;

namespace QuanLiCoffeeShop.ViewModel.AdminVM.ThongKeVM
{
    public partial class ThongKeViewModel: BaseViewModel
    {       
        private SeriesCollection _revenueSeries;
        public SeriesCollection RevenueSeries
        {
            get { return _revenueSeries; }
            set
            {
                _revenueSeries = value;
                OnPropertyChanged();
            }
        }
        private string[] _labels;
        public string[] Labels
        {
            get { return _labels; }
            set
            {
                _labels = value;
                OnPropertyChanged(nameof(Labels));
            }
        }

        private Func<int, string> _yFormatter;
        public Func<int, string> YFormatter
        {
            get { return _yFormatter; }
            set
            {
                _yFormatter = value;
                OnPropertyChanged(nameof(YFormatter));
            }
        }
        private decimal _sumBillTotal;
        public decimal SumBillTotal
        {
            get { return _sumBillTotal; }
            set { _sumBillTotal = value; OnPropertyChanged(); }
        }
        public void GetRevenueData() { 
        }
    }
}
