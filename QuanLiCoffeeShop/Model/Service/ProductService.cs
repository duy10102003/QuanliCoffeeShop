using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLiCoffeeShop.Model.Service
{
    public class ProductService
    {
        public ProductService() { }
		private static ProductService _ins;

		public static ProductService Ins
		{
			get 
			{ 
				if (_ins == null)
				{
					_ins = new ProductService();
				}
				return _ins; 
			}
			private set { _ins = value; }
		}
        //Get all product
        public async Task<List<ProductDTO>> GetAllProduct()
		{
            try
            {
                using (var context = new QuanLiCoffeShopEntities())
                {
                    var productList = (from c in context.Product
                                       where c.IsDeleted == false
                                       select new ProductDTO
                                       {
                                           ID = c.ID,
                                           DisplayName = c.DisplayName,
                                           Price = c.Price,
                                           IDGenre = c.IDGenre,
                                           GenreName = c.GenreProduct.DisplayName,
                                           Count = c.Count,
                                           Description = c.Description,
                                           Image = c.Image,
                                           IsDeleted = c.IsDeleted,
                                       }).ToListAsync();
                    return await productList;
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return null;
            }
		}
        public async Task<List<ProductDTO>> GetAllProductCounted()
        {
            try
            {
                using (var context = new QuanLiCoffeShopEntities())
                {
                    var productList = (from c in context.Product
                                       where c.IsDeleted == false && c.Count > 0
                                       select new ProductDTO
                                       {
                                           ID = c.ID,
                                           DisplayName = c.DisplayName,
                                           Price = c.Price,
                                           IDGenre = c.IDGenre,
                                           GenreName = c.GenreProduct.DisplayName,
                                           Count = c.Count,
                                           Description = c.Description,
                                           Image = c.Image,
                                           IsDeleted = c.IsDeleted,
                                       }).ToListAsync();
                    return await productList;
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi khi tìm sản phẩm");
                return null;
            }

        }

        //Add new product

        public async Task<(bool, string)> AddNewPrD(Product newPrD)
		{
            try
            {
                using (var context = new QuanLiCoffeShopEntities())
                {
                    var prD = await context.Product.Where(p => p.DisplayName == newPrD.DisplayName).FirstOrDefaultAsync();
                    if (prD != null)
                    {
                        if (prD.IsDeleted == true)
                        {
                            prD.DisplayName = newPrD.DisplayName;
                            prD.Price = newPrD.Price;
                            prD.IDGenre = newPrD.IDGenre;
                            prD.GenreProduct = newPrD.GenreProduct;
                            prD.Count = newPrD.Count;
                            prD.Description = newPrD.Description;
                            prD.Image = newPrD.Image;
                            prD.IsDeleted = false;
                            await context.SaveChangesAsync();
                            return (true, "Thêm thành công");
                        }
                        else
                        {
                            return (false, "Đã tồn tại sản phẩm");
                        }
                    }
                    context.Product.Add(newPrD);
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

        //Delete product
        public async Task<(bool, string)> DeletePrD(int ID)
        {
            try
            {
                using (var context = new QuanLiCoffeShopEntities())
                {
                    var prD = await context.Product.Where(p => p.ID == ID).FirstOrDefaultAsync();
                    if (prD.IsDeleted == false) prD.IsDeleted = true;
                    await context.SaveChangesAsync();
                    return (true, "Xóa thành công");
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return (false, null);
            }
			
        }

		//Edit product
		public async Task<(bool, string)> EditPrD(Product newPrD, int ID)
		{
            try
            {
                using (var context = new QuanLiCoffeShopEntities())
                {
                    var prD = await context.Product.Where(p => p.ID == newPrD.ID).FirstOrDefaultAsync();

                    if (prD == null) return (false, "Không tìm thấy ID");
                    prD.DisplayName = newPrD.DisplayName;
                    prD.Price = newPrD.Price;
                    prD.IDGenre = newPrD.IDGenre;
                    prD.GenreProduct = newPrD.GenreProduct;
                    prD.Count = newPrD.Count;
                    prD.Description = newPrD.Description;
                    prD.Image = newPrD.Image;
                    prD.IsDeleted = false;
                    await context.SaveChangesAsync();
                    return (true, "Cập nhật thành công");
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi khi sửa sản phẩm");
                return (false, null);
            }
            

        }
        //update count product 
        public async Task<(bool,string)> EditCountPrd(int id, int? countDelta)
        {
            try
            {
                using(var context = new QuanLiCoffeShopEntities())
                {
                    var prd = await context.Product.Where(p => p.ID == id).FirstOrDefaultAsync();
                    if(prd == null) return(false, null);
                    prd.Count = prd.Count - countDelta;
                    if(prd.Count < 0) {
                        return(false, null);
                    }
                    await context.SaveChangesAsync();
                    return (true, "Da them thanh cong");
                }
                
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi khi sửa sản phẩm");
                return (false, null);
            }
        }
    }
}
