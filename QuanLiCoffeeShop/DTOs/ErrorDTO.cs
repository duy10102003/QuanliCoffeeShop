using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiCoffeeShop.DTOs
{
    public class ErrorDTO
    {
        public int ID { get; set; }
        public string DisplayName { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
