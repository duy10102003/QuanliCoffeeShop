﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLiCoffeeShop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
	{
        public App()
        {
			CultureInfo newCulture = new CultureInfo("vi-VN");
			CultureInfo.DefaultThreadCurrentCulture = newCulture;
			CultureInfo.DefaultThreadCurrentUICulture = newCulture;
		}
    }
}
