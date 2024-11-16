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
    public class BillService
    {
        public BillService() { }
		private static BillService _ins;

		public static BillService Ins
		{
			get 
			{
				if (_ins == null)
				{
					_ins = new BillService();
				}
				return _ins; 
			}
			private set { _ins = value; }
		}
		// Get All Bill
		public async Task<List<BillDTO>> GetAllBill()
		{
            try
            {
                using (var context = new QuanLiCoffeShopEntities())
                {
                    var billList = (from c in context.Bill
                                    where c.IsDeleted == false
                                    select new BillDTO
                                    {
                                        ID = c.ID,
                                        IDCus = c.IDCus,
                                        IDStaff = c.IDStaff,
                                        CreateAt = c.CreateAt,
                                        TotalPrice = c.TotalPrice,
                                        Customer = c.Customer,
                                        Staff = c.Staff,
                                        Seat = c.Seat,
                                        IDSeat = c.IDSeat,
                                        BillInfo = (from x in c.BillInfo
                                                    where x.IsDeleted == false
                                                    select new BillInfoDTO
                                                    {
                                                        IDBill = x.IDBill,
                                                        PriceItem = x.PriceItem,
                                                        IDProduct = x.IDProduct,
                                                        Count = x.Count,
                                                        Description = x.Description,
                                                        Bill = x.Bill,
                                                        Product = x.Product,
                                                    }).ToList(),
                                        IsDeleted = c.IsDeleted
                                    }).OrderByDescending(m => m.CreateAt).ToListAsync();

                    return await billList;
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Lỗi xảy ra!");
                return null;
            }
			
				
		}
        //Get bill by id seat
        public async Task<BillDTO> getBillByIdSeat(int id)
        {
            try
            {
                using (var context = new QuanLiCoffeShopEntities())
                {
                    var billList = await (from c in context.Bill
                                          where c.IsDeleted == false && c.IDSeat == id
                                          select new BillDTO
                                          {
                                              ID = c.ID,
                                              IDCus = c.IDCus,
                                              IDStaff = c.IDStaff,
                                              CreateAt = c.CreateAt,
                                              TotalPrice = c.TotalPrice,
                                              Customer = c.Customer,
                                              Staff = c.Staff,
                                              Seat = c.Seat,
                                              IDSeat = c.IDSeat,
                                              BillInfo = (from x in c.BillInfo
                                                          where x.IsDeleted == false
                                                          select new BillInfoDTO
                                                          {
                                                              IDBill = x.IDBill,
                                                              PriceItem = x.PriceItem,
                                                              IDProduct = x.IDProduct,
                                                              Count = x.Count,
                                                              Description = x.Description,
                                                              Bill = x.Bill,
                                                              Product = x.Product,
                                                          }).ToList(),
                                              IsDeleted = c.IsDeleted
                                          }).ToListAsync();


                    var bill = billList.LastOrDefault();
                    return bill;
                }
            
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Lỗi xảy ra khi tìm bill theo bàn!");
                return null;
            }
        }
        public async Task<List<BillDTO>> GetBillBetweenDate(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                using (var context = new QuanLiCoffeShopEntities())
                {
                    List<BillDTO> billDTOs = await BillService.Ins.GetAllBill();

                    List<BillDTO> billList = billDTOs
                        .Where(bill => bill.CreateAt >= dateFrom && bill.CreateAt <= dateTo)
                        .ToList();

                    return billList;

                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Lỗi xảy ra!");
                return null;
            }
        }
        //Add new bill
        public async Task<(bool, string)> AddNewBill(BillDTO newBill)
		{
            try
            {
                using (var context = new QuanLiCoffeShopEntities())
                {

                    int? maxID = await context.Bill.MaxAsync(b => (int?)b.ID);
                    int curID = 0;
                    if (maxID.HasValue)
                    {
                        curID = (int)maxID + 1;
                    }
                    else
                    {
                        curID = 1;
                    }
                    Bill bill = new Bill
                    {
                        ID = curID,
                        IDCus = newBill.IDCus,
                        IDStaff = newBill.IDStaff,
                        IsDeleted = false,
                        CreateAt = newBill.CreateAt,
                        IDSeat = newBill.IDSeat,
                        TotalPrice = newBill.TotalPrice,

                    };

                    List<BillInfo> billInfoList = new List<BillInfo>();

                    foreach (var bI in newBill.BillInfo)
                    {
                        BillInfo billInfo = new BillInfo
                        {
                            IDBill = curID,
                            IDProduct = bI.IDProduct,
                            IsDeleted = false,
                            PriceItem = bI.PriceItem,
                            Description = bI.Description,
                            Count = bI.Count
                        };
                        (bool success, string msg) = await ProductService.Ins.EditCountPrd(bI.IDProduct, bI.Count);
                        if (!success)
                        {
                            return (false, null);
                        }
                        billInfoList.Add(billInfo);

                    }

                    context.BillInfo.AddRange(billInfoList);
                    context.Bill.Add(bill);
                    await context.SaveChangesAsync();
                    return (true, "Them thanh cong");
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Lỗi xảy ra khi thêm bill!");
                return (false,null);
            }
				
		}
        //Delete bill
        public async Task<(bool, string)> DeleteBill(BillDTO Bill)
		{
            try
            {
                using (var context = new QuanLiCoffeShopEntities())
                {
                    var bill = await context.Bill.Where(p => p.ID == Bill.ID).FirstOrDefaultAsync();
                    if (bill.IsDeleted == false) bill.IsDeleted = true;
                    foreach (var b in Bill.BillInfo)
                    {
                        var billInfo = await context.BillInfo.Where(p => p.IDBill == b.IDBill && p.IDProduct == b.IDProduct).FirstOrDefaultAsync();
                        if (billInfo.IsDeleted == false) billInfo.IsDeleted = true;
                    }
                    await context.SaveChangesAsync();
                    return (true, "Xoa thanh cong");
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Lỗi xảy ra!");
                return (false, null);

            }
               
		}

		//Edit bill
		public async Task<(bool, string)> EditBill(BillDTO newBill)
		{
            try
            {
                using (var context = new QuanLiCoffeShopEntities())
                {
                    var bill = await context.Bill.Where(p => p.ID == newBill.ID).FirstOrDefaultAsync();
                    bill.IDStaff = newBill.IDStaff;
                    bill.Staff = newBill.Staff;
                    bill.Customer = newBill.Customer;
                    bill.IDCus = newBill.IDCus;
                    bill.CreateAt = newBill.CreateAt;
                    bill.TotalPrice = newBill.TotalPrice;
                    bill.IDSeat = newBill.IDSeat;
                    bill.Seat = newBill.Seat;
                    foreach (var b in newBill.BillInfo)
                    {
                        var billInfo = await context.BillInfo.Where(p => p.IDBill == b.IDBill && p.IDProduct == b.IDProduct).FirstOrDefaultAsync();
                        billInfo.IDProduct = b.IDProduct;
                        billInfo.PriceItem = b.PriceItem;
                        billInfo.IsDeleted = false;
                        billInfo.Product = b.Product;
                        billInfo.Description = b.Description;
                        billInfo.Count = b.Count;
                        billInfo.Bill = b.Bill;
                        bill.BillInfo.Add(new BillInfo
                        {
                            IDBill = b.IDBill,
                            IDProduct = b.IDProduct,
                            PriceItem = b.PriceItem,
                            IsDeleted = false,
                            Product = b.Product,
                            Count = b.Count,
                            Bill = b.Bill,
                        });
                    }
                    await context.SaveChangesAsync();
                    return (true, "Cập nhật thành công");
                }
            }
            catch
            {

                MessageBoxCustom.Show(MessageBoxCustom.Error, "Lỗi xảy ra!");
                return (false, null);
            }
                
        }
        public async Task<int> getBillByDate(DateTime date)
        {
            try
            {
                using (var context = new QuanLiCoffeShopEntities())
                {
                    var billTotal = await context.Bill.Where(p => p.CreateAt.Value.Day == date.Day
                                                           && p.CreateAt.Value.Month == date.Month
                                                           && p.CreateAt.Value.Year == date.Year 
                                                           && p.IsDeleted==false).ToListAsync();
                    int totalPrice = 0;
                    foreach (var bill in billTotal)
                    {
                        totalPrice = totalPrice + (int)bill.TotalPrice;
                    }
                    return totalPrice;

                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Lỗi xảy ra!");
                return -1;
            }
        }

    }
    
}
