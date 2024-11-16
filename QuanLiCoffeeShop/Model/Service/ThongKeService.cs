using QuanLiCoffeeShop.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLiCoffeeShop.ViewModel.AdminVM.ThongKeVM;
using System.Windows.Forms;
using QuanLiCoffeeShop.View.MessageBox;

namespace QuanLiCoffeeShop.Model.Service
{
    public class ThongKeService {
        private ThongKeService() { }
        private static ThongKeService _ins;
        public static ThongKeService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new ThongKeService();
                }
                return _ins;
            }
            private set => _ins = value;
        }
        public async Task<List<ProductDTO>> GetTop10SalerBetween(DateTime from, DateTime to)
        {
            try
            {
                using (var context = new QuanLiCoffeShopEntities())
                {
                    var prodStatistic = context.BillInfo.Where(b => b.Bill.CreateAt>=from&&b.Bill.CreateAt<=to&&b.IsDeleted==false)
                    .GroupBy(pBill => pBill.IDProduct)
                    .Select(gr => new
                    {
                        IDProduct = gr.Key,
                        Revenue = gr.Sum(pBill => (Decimal?)(pBill.PriceItem)) ?? 0,
                        SalesQuantity = gr.Sum(pBill => (int?)pBill.Count) ?? 0
                    })
                    .OrderByDescending(m => m.SalesQuantity).Take(10)
                    .Join(
                    context.Product,
                    statis => statis.IDProduct,
                    prod => prod.ID,
                    (statis, prod) => new ProductDTO
                    {
                        ID = prod.ID,
                        DisplayName = prod.DisplayName,
                        Price = statis.Revenue,
                        Count = statis.SalesQuantity
                    }).ToListAsync();

                    return await prodStatistic;
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