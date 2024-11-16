using QuanLiCoffeeShop.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLiCoffeeShop.Model;
using QuanLiCoffeeShop.Model.Service;
using QuanLiCoffeeShop.View.MessageBox;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;

namespace QuanLiCoffeeShop.ViewModel.AdminVM.ThongKeVM
{
    public partial class ThongKeViewModel: BaseViewModel
    {
        //private static List<ProductDTO> favorList;
        private List<ProductDTO> _favorList;
        public List<ProductDTO> FavorList
        {
            get { return _favorList; }
            set { _favorList = value; OnPropertyChanged(); }
        }
    }
}
