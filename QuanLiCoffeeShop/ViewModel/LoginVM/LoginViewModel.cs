using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.Model;
using QuanLiCoffeeShop.Model.Service;
using QuanLiCoffeeShop.Utils;
using QuanLiCoffeeShop.View.Admin;
using QuanLiCoffeeShop.View.LoginWindow;
using QuanLiCoffeeShop.View.MessageBox;
using QuanLiCoffeeShop.View.Staff;
using QuanLiCoffeeShop.ViewModel;
using QuanLiCoffeeShop.ViewModel.AdminVM;
using QuanLiCoffeeShop.ViewModel.StaffVM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace QuanLiCoffeeShop.ViewModel.LoginVM
{
    public class LoginViewModel : BaseViewModel
    {
        private string _Username;

        public string Username
        {
            get { return _Username; }
            set { _Username = value; OnPropertyChanged(); }
        }
        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; OnPropertyChanged(); }
        }

        private string _forgotEmail;

        public string forgotEmail
        {
            get { return _forgotEmail; }
            set { _forgotEmail = value; OnPropertyChanged(); }
        }
        private bool IsLogin = false;
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand ForgotPasswordCM { get; set; }
        public ICommand SendCM { get; set; }
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand<Window>((p) =>
            {
                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                {
                    return false;
                }
                return true;
            },
            async (p) =>
            {
                if (IsLogin == false)
                {
                    IsLogin = true;
                    await Login(p);
                    IsLogin = false;
                }
              
            });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                Password = p.Password;
            });
            ForgotPasswordCM = new RelayCommand<TextBlock>((p) => { return true; }, async (p) =>
            {
                ForgotPassword wd = new ForgotPassword();
                wd.ShowDialog();
            });
            SendCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                string newPass = Helper.randomCode();
                (bool updateSuccess, string message, string username) = await StaffService.Ins.UpdatePassword(forgotEmail, newPass);
                if (!updateSuccess) 
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, message);
                }
                else
                {
                    await LoginService.Ins.sendEmail(forgotEmail, newPass,username);
                    MessageBoxCustom.Show(MessageBoxCustom.Success, "Đã gửi email thành công");
                }
                
            });

        }
        #region methods
        async Task Login(Window p)
        {
            try
            {
                using (var context = new QuanLiCoffeShopEntities())
                {
                    string password = Helper.MD5Hash(Password);
                    Staff staff = await context.Staff.Where(x => x.UserName == Username && x.PassWord == password && x.IsDeleted == false).FirstOrDefaultAsync();
                    if (staff != null)
                    {
                        p.Visibility = Visibility.Collapsed;
                        StaffDTO curStaff = new StaffDTO
                        {
                            ID = staff.ID,
                            DisplayName = staff.DisplayName,
                            StartDate = staff.StartDate,
                            UserName = staff.UserName,
                            PassWord = staff.PassWord,
                            PhoneNumber = staff.PhoneNumber,
                            BirthDay = staff.BirthDay,
                            Wage = staff.Wage,
                            Status = staff.Status,
                            Email = staff.Email,
                            Gender = staff.Gender,
                            Role = staff.Role,
                            IsDeleted = staff.IsDeleted,
                        };
                        if (staff.Role == "Quản lí")
                        {
                            MainAdminWindow ad = new MainAdminWindow();
                            MainAdminViewModel.currentStaff = curStaff;
                            ad.Owner = p;
                            ad.Show();

                        }
                        else
                        {
                            MainStaffWindow st = new MainStaffWindow();
                            MainStaffViewModel.currentStaff = curStaff;
                            st.Owner = p;
                            st.Show();
                        }

                    }
                    else
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Sai tài khoản hoặc mật khẩu, vui lòng nhập lại!");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Có lỗi xảy ra khi đăng nhập");
            }
        }
        
        #endregion
    }
}
