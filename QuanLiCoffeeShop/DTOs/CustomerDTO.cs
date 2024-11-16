using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiCoffeeShop.DTOs
{
    public class CustomerDTO
    {
        public int ID { get; set; }
        public Nullable<int> IDSeat { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Spend { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
