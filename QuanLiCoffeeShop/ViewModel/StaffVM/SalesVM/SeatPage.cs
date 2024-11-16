using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.Model.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLiCoffeeShop.ViewModel.StaffVM.SalesVM
{
    public partial class SalesMainPageViewModel : BaseViewModel
    {
        public static List<SeatDTO> tablelist = new List<SeatDTO>();
        private ObservableCollection<SeatDTO> _tablelist = new ObservableCollection<SeatDTO>();
        public ObservableCollection<SeatDTO> TableList
        {
            get { return _tablelist; }
            set { _tablelist = value; OnPropertyChanged(nameof(TableList)); }
        }
        private ObservableCollection<string> combogenrelist;
        public ObservableCollection<string> ComboList
        {
            get { return combogenrelist; }
            set { combogenrelist = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> statuslist;
        public ObservableCollection<string> StatusList
        {
            get { return statuslist; }
            set { statuslist = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> _genreList;
        public ObservableCollection<string> GenreList
        {
            get { return _genreList; }
            set { _genreList = value; OnPropertyChanged(); }
        }
        private string contentbtn = "Tất cả";
        public string Contentbtn
        {
            get { return contentbtn; }
            set { contentbtn = value; OnPropertyChanged(); }
        }
        private string genre;
        public string Genre
        {
            get { return genre; }
            set
            {
                genre = value;
                OnPropertyChanged();
                UpdateBtn();
            }
        }
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }
        private string genreName;
        public string GenreName
        {
            get { return genreName; }
            set { genreName = value; OnPropertyChanged(nameof(GenreName)); }
        }
        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(); }
        }
        async void UpdateBtn()
        {
            TableList = new ObservableCollection<SeatDTO>(await SeatService.Ins.GetAllSeat());
            tablelist = new List<SeatDTO>(TableList);
            if (Genre == "Tất cả loại bàn")
            {
                switch (Contentbtn)
                {
                    case "Tất cả":
                        Contentbtn = "Tất cả";
                        TableList = new ObservableCollection<SeatDTO>(await SeatService.Ins.GetAllSeat());
                        break;
                    case "Đã đặt":
                        Contentbtn = "Đã đặt";
                        TableList = new ObservableCollection<SeatDTO>(tablelist.FindAll(x => (x.Status == "Đã đặt")));
                        break;
                    case "Còn trống":
                        Contentbtn = "Còn trống";
                        TableList = new ObservableCollection<SeatDTO>(tablelist.FindAll(x => (x.Status == "Còn trống")));
                        break;
                    case "Đang sửa chữa":
                        Contentbtn = "Đang sửa chữa";
                        TableList = new ObservableCollection<SeatDTO>(tablelist.FindAll(x => (x.Status == "Đang sửa chữa")));
                        break;

                }
            }
            else
            {
                switch (contentbtn)
                {
                    case "Tất cả":
                        Contentbtn = "Tất cả";
                        TableList = new ObservableCollection<SeatDTO>((tablelist.FindAll(x => x.GenreName == Genre)));
                        break;
                    case "Đã đặt":
                        Contentbtn = "Đã đặt";
                        TableList = new ObservableCollection<SeatDTO>(tablelist.FindAll(x => (x.Status == "Đã đặt" && x.GenreName == Genre)));
                        break;
                    case "Còn trống":
                        Contentbtn = "Còn trống";
                        TableList = new ObservableCollection<SeatDTO>(tablelist.FindAll(x => (x.Status == "Còn trống" && x.GenreName == Genre)));
                        break;
                    case "Đang sửa chữa":
                        Contentbtn = "Đang sửa chữa";
                        TableList = new ObservableCollection<SeatDTO>(tablelist.FindAll(x => (x.Status == "Đang sửa chữa" && x.GenreName == Genre)));
                        break;

                }
            }
        }
        async void LoadPage()
        {
            StatusList = new ObservableCollection<string>();
                StatusList.Add("Còn trống");
                StatusList.Add("Đã đặt");
                StatusList.Add("Đang sửa chữa");
                TableList = new ObservableCollection<SeatDTO>(await SeatService.Ins.GetAllSeat());
                tablelist = new List<SeatDTO>(TableList);
                GenreList = new ObservableCollection<string>(await GenreService.Ins.GetAllSeat());
                ComboList = new ObservableCollection<string>(await GenreService.Ins.GetAllSeat());
                ComboList.Insert(0, "Tất cả loại bàn");
                Genre = "Tất cả loại bàn";
        }
        public ICommand Classify { get; set; }
        public ICommand LoadBill { get; set; }

    }
}
