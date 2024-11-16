using System;
using QuanLiCoffeeShop.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiCoffeeShop.DTOs
{
    public class ProductDTO : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string DisplayName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> IDGenre { get; set; }
        public string GenreName { get; set; }
        private Nullable<int> _count;
        public Nullable<int> Count
        {
            get { return _count; }
            set { _count = value; OnPropertyChanged(nameof(Count)); }
        }
        public string Description { get; set; }
        public string Image { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
