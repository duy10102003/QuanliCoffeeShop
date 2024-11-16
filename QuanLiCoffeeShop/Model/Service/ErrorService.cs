using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiCoffeeShop.Model.Service
{
    public class ErrorService
    {
        public ErrorService() { }
		private static ErrorService _ins;

		public static ErrorService Ins
		{
			get 
			{ 
				if (_ins == null)
				{
					_ins = new ErrorService();
				}
				return _ins; 
			}
			private set { _ins = value; }
		}

		//Get all error
		public async Task<List<ErrorDTO>> GetAllError()
		{
			try
			{
                using (var context = new QuanLiCoffeShopEntities())
                {

                    var errList = (from c in context.Error
                                   where c.IsDeleted == false
                                   select new ErrorDTO
                                   {
                                       ID = c.ID,
                                       DisplayName = c.DisplayName,
                                       Status = c.Status,
                                       Description = c.Description,
                                       IsDeleted = c.IsDeleted,
                                   }).ToListAsync();
                    return await errList;
                }
            }
			catch
			{
				MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
				return null;
			}
				
		}
		
		//Add error
		public async Task<(bool, string)> AddNewError(Error newError)
		{
			try
			{
                using (var context = new QuanLiCoffeShopEntities())
                {
                    context.Error.Add(newError);
                    await context.SaveChangesAsync();
                    return (true, "Them thanh cong");
                }
            }
			catch
			{
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return (false,null);
            }
				
		}
		
		//Edit error
		public async Task<(bool,string)> EditError(Error newError)
		{
			try
			{
                using (var context = new QuanLiCoffeShopEntities())
                {
                    var err = await context.Error.Where(p => p.ID == newError.ID).FirstOrDefaultAsync();
                    err.Description = newError.Description;
                    err.Status = newError.Status;
                    err.DisplayName = newError.DisplayName;
                    err.IsDeleted = false;
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

		//Delete Error
		public async Task<(bool, string)>  DeleteError(int ID)
		{
            try
            {
                using (var context = new QuanLiCoffeShopEntities())
                {
                    var err = await context.Error.Where(p => p.ID == ID).FirstOrDefaultAsync();
                    if (err.IsDeleted == false) err.IsDeleted = true;
                    else
                    {
                        return (false, "Da xoa");
                    }
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
	}
}
