using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiCoffeeShop.DTOs
{
    public class SeatDTO
    {
        public int ID { get; set; }
        public Nullable<int> IDGenre { get; set; }
        public string GenreName { get; set; }
        public string Status { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
