﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using QuanLiCoffeeShop.View.MessageBox;
using System.Data.Entity;
using QuanLiCoffeeShop.Utils;

namespace QuanLiCoffeeShop.Model.Service
{
    public class LoginService
    {
        private static LoginService _ins;

        public static LoginService Ins
        {
            get
            {
                if (_ins == null) 
                    _ins = new LoginService();
                return _ins;
            }
            set { _ins = value; }
        }
        public async Task sendEmail(string email, string pass, string username)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.EnableSsl = true;
                smtpClient.Port = 587;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("luongcongduy826@gmail.com", "yhvrbkstycnaavhx");

                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.From = new MailAddress("luongcongduy826@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Coffee time xin thông báo";
                mail.Body = "Username:" + username + "\n" +"New password:" + pass;
                await smtpClient.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Có lỗi xảy ra, vui lòng nhập lại !");
            }
        }
    }
}
