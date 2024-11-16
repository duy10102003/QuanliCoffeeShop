using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiCoffeeShop.Model.Service
{
    public class GenreService
    {
		private static GenreService _ins;

		public static GenreService Ins
		{
			get 
			{ 
				if(_ins == null) _ins = new GenreService();
				return _ins; 
			}
			private set { _ins = value; }
		}
        public async Task<(int, GenreProduct)> FindGenrePrD(string name)
        {
            try
            {
                using (var context = new QuanLiCoffeShopEntities())
                {
                    var prD = await context.GenreProduct.Where(p => p.DisplayName == name).FirstOrDefaultAsync();
                    if (prD == null)
                    {
                        return (-1, null);
                    }
                    return (prD.ID, prD);
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return (-1, null);
            }
                
        }
        public async Task<(int, GenreSeat)> FindGenreSeat(string name)
        {
            try
            {
                using (var context = new QuanLiCoffeShopEntities())
                {
                    var seat = await context.GenreSeat.Where(p => p.DisplayName == name).FirstOrDefaultAsync();
                    if (seat == null)
                    {
                        return (-1, null);
                    }
                    return (seat.ID, seat);
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return (-1, null);
            }

        }

        //Get  all gerne seat
        public async Task<List<string>> GetAllSeat()
        {
            try
            {
                using (var context = new QuanLiCoffeShopEntities())
                {
                    var seatGenreList = (from c in context.GenreSeat select c.DisplayName).ToListAsync();
                    return await seatGenreList;
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return null;
            }
            
        }

        // Get all genre prD
        public async Task<List<string>> GetAllPrD()
        {
            try
            {
                using (var context = new QuanLiCoffeShopEntities())
                {
                    var productGenreList = (from c in context.GenreProduct select c.DisplayName).ToListAsync();
                    return await productGenreList;
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return null;
            }
            
        }
    }
}
