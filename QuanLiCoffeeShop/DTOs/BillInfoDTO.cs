using QuanLiCoffeeShop.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiCoffeeShop.DTOs
{
    public class BillInfoDTO: INotifyPropertyChanged
    {
        public int IDBill { get; set; }
        public int IDProduct { get; set; }
        private Nullable<int> _count;
        public Nullable<int> Count {
            get { return _count; }
            set
            {
                if (_count != value)
                {
                    _count = value;
                    OnPropertyChanged(nameof(Count));
                    OnPropertyChanged(nameof(PriceItem));
                }
            }
        }
        private Nullable<decimal> _priceItem;
        public Nullable<decimal> PriceItem
        {
            get { return _priceItem; }
            set
            {
                if (_priceItem != value)
                {
                    _priceItem = value;
                    OnPropertyChanged(nameof(PriceItem));
                }
            }
        }
        public Nullable<bool> IsDeleted { get; set; }
        public string Description { get; set; }
        public Bill Bill { get; set; }
        public Product Product { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
