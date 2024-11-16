using QuanLiCoffeeShop.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLiCoffeeShop.ViewModel.StaffVM.SalesVM
{
    public partial class SalesMainPageViewModel:BaseViewModel
    {
        public static List<ProductDTO> prdList;
        private ObservableCollection<ProductDTO> _productList;

        public ObservableCollection<ProductDTO> ProductList
        {
            get { return _productList; }
            set { _productList = value; OnPropertyChanged(); }
        }
        public ICommand AllPrDFilter { get; set; }
        public ICommand ProductFilter { get; set; }
        public ICommand SearchProductCM { get; set; }
        public ICommand SelectPrd {  get; set; }
    }
}
