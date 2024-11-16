using QuanLiCoffeeShop.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using QuanLiCoffeeShop.View.MessageBox;

namespace QuanLiCoffeeShop.Model.Service
{
    public class SeatService
    {
        public SeatService() { }
		private static SeatService _ins;

		public static SeatService Ins
		{
			get 
			{ 
				if (_ins == null)
				{
					_ins = new SeatService();
				}
				return _ins; 
			}
			private set { _ins = value; }
		}

		//Get all seat
		public async Task<List<SeatDTO>> GetAllSeat()
		{
			try
			{
                using (var context = new QuanLiCoffeShopEntities())
                {
                    var seatList = (from c in context.Seat
                                    where c.IsDeleted == false
                                    select new SeatDTO
                                    {
                                        ID = c.ID,
                                        GenreName = c.GenreSeat.DisplayName,
                                        IDGenre = c.IDGenre,
                                        IsDeleted = c.IsDeleted,
                                        Status = c.Status,
                                    }).ToListAsync();
                    return await seatList;
                }
            }
			catch
			{
				MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
				return null;
			}
			
		}
		public async Task<(Seat, bool)> FindSeat(int seat)
		{
			try
			{
                using (var context = new QuanLiCoffeShopEntities())
                {
                    var s = await context.Seat.Where(p => p.ID == seat).FirstOrDefaultAsync();
                    if (s == null)
                    {
                        return (null, false);
                    }
                    else
                        return (s, true);
                }
            }
			catch
			{
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return (null,false);
            }
		}
		//Add new seat
		public async Task<(bool, string)> AddNewSeat(Seat newSeat)
		{
			try
			{
                using (var context = new QuanLiCoffeShopEntities())
                {
                    context.Seat.Add(newSeat);
                    await context.SaveChangesAsync();
                    return (true, "Them thanh cong");
                }
            }
			catch
			{
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return (false, null);

            }
			

        }

		//Delete seat
		public async Task<(bool, string)> DeleteSeat(int ID)
		{
            try
            {
                using (var context = new QuanLiCoffeShopEntities())
                {
                    var seat = await context.Seat.Where(p => p.ID == ID).FirstOrDefaultAsync();
                    if (seat.IsDeleted == false) seat.IsDeleted = true;
                    await context.SaveChangesAsync();
                    return (true, "Xoa thanh cong");
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return (false, null);
            }
				
		}

		//Edit seat
		public async Task<(bool,string)> EditSeat(Seat newSeat)
		{
            try
            {
                using (var context = new QuanLiCoffeShopEntities())
                {
                    var seat = await context.Seat.Where(p => p.ID == newSeat.ID).FirstOrDefaultAsync();
                    seat.IDGenre = newSeat.IDGenre;
                    seat.Status = newSeat.Status;
                    seat.GenreSeat = newSeat.GenreSeat;
                    await context.SaveChangesAsync();
                    return (true, "Cap nhat thanh cong");
                }
            }

            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return (false, null);
            }
				
		}
	}
}
