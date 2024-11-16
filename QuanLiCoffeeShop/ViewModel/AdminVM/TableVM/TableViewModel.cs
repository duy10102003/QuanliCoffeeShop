using System;
using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.Model;
using QuanLiCoffeeShop.Model.Service;
using QuanLiCoffeeShop.View.Admin.Table;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLiCoffeeShop.View.MessageBox;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Controls;
using System.Windows;

namespace QuanLiCoffeeShop.ViewModel.AdminVM.TableVM
{
    internal class TableViewModel: BaseViewModel
    {
        public static List<SeatDTO> tablelist = new List<SeatDTO>();
        private ObservableCollection<SeatDTO> _tablelist = new ObservableCollection<SeatDTO>();
        public ObservableCollection<SeatDTO> TableList
        { 
            get { return _tablelist; }
            set {  _tablelist = value; OnPropertyChanged(); }
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

        private SeatDTO selecteditem;
        public SeatDTO SelectedItem
        {
            get { return selecteditem; }
            set { selecteditem = value; OnPropertyChanged();}
        }
        private string contentbtn="Tất cả";
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
        private bool isPopupOpenEdit = false;
        public bool IsPopupOpenEdit
        {
            get { return isPopupOpenEdit; }
            set
            {
                isPopupOpenEdit = value;
                IsOpenMain = !value;
                OnPropertyChanged(nameof(IsPopupOpenEdit)); // Đảm bảo cập nhật giao diện người dùng khi giá trị này thay đổi.
            }
        }
        private bool isPopupOpenDelete = false;
        public bool IsPopupOpenDelete
        {
            get { return isPopupOpenDelete; }
            set
            {
                isPopupOpenDelete = value;
                IsOpenMain = !value;
                OnPropertyChanged(nameof(IsPopupOpenDelete)); // Đảm bảo cập nhật giao diện người dùng khi giá trị này thay đổi.
            }
        }
     
        private bool isOpenMain = true;
        public bool IsOpenMain 
        { get { return isOpenMain; }
           set { isOpenMain = value; OnPropertyChanged(nameof(IsOpenMain)); } 
        }
        private bool isPopupOpenAdd = false;
        public bool IsPopupOpenAdd
        {
            get { return isPopupOpenAdd; }
            set
            {
                isPopupOpenAdd = value;
                IsOpenMain = !value;
                OnPropertyChanged(nameof(IsPopupOpenAdd)); // Đảm bảo cập nhật giao diện người dùng khi giá trị này thay đổi.

            }
        }
        private bool isPopupOpenInfo=false;
        public bool IsPopupOpenInfo
        {
            get { return isPopupOpenInfo; }
            set { isPopupOpenInfo = value;
                IsOpenMain = !value;
                OnPropertyChanged(nameof(IsPopupOpenInfo)); }
        }
        void resetdata()
        {
            ID = -1;
            GenreName = null;
            Status=null;
        }
        void get_selecteditem(SeatDTO a)
        {
            SelectedItem = null;
            SelectedItem = a;
        }
        async void  UpdateBtn()
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
        public ICommand FirstLoadTable { get; set; }
        public ICommand Add {  get; set; }
        public ICommand Edit { get; set; }
        public ICommand OpenAdd { get; set; }
        public ICommand CloseAdd { get; set; }
        public ICommand OpenEdit {  get; set; }
        public ICommand CloseEdit { get; set;}
        public ICommand OpenDelete { get; set; }
        public ICommand OpenInfo { get; set; }
        public ICommand CloseInfo { get; set; }
        public ICommand Classify {  get; set; }
       
        public TableViewModel()
        {
            FirstLoadTable = new RelayCommand<Page>((p) => { return true; }, async (p) =>
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


            });
            OpenInfo = new RelayCommand<SeatDTO>((p) => { return true; }, (p) =>
            {
                get_selecteditem(p);
                IsPopupOpenInfo = true;
                ID = SelectedItem.ID;
                GenreName = SelectedItem.GenreName;
                Status = SelectedItem.Status;
            });
            CloseInfo = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsPopupOpenInfo = false;
            });
            OpenEdit = new RelayCommand<SeatDTO>((p) => { return true; }, (p) =>
            {
                get_selecteditem(p);
                ID=SelectedItem.ID;
                GenreName =SelectedItem.GenreName;
                Status = SelectedItem.Status;
                IsPopupOpenEdit = true;
            });
            CloseEdit = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsPopupOpenEdit = false;
            });


            OpenDelete = new RelayCommand<SeatDTO>((p) => { return true; },async (p) =>
            {
                get_selecteditem(p);
                DeleteMessage wd = new DeleteMessage();
                wd.ShowDialog();
                if (wd.DialogResult == true)
                {
                    (bool success, string messageEdit) = await SeatService.Ins.DeleteSeat(selecteditem.ID);
                    if (success)
                    {
                        UpdateBtn();
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Bạn đã xóa thành công");

                    }
                    else
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Bạn đã xóa thất bại");
                    }
                }    
            });          
            OpenAdd = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                resetdata();
                IsPopupOpenAdd = true;
            });
            CloseAdd = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsPopupOpenAdd = false;
            });

            Add =new RelayCommand<object>((p) => { return true;},async (p)=>
            {
                if(Status==null || GenreName==null)
                {
                    IsPopupOpenAdd=false;
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Bạn đã nhập thiếu thông tin");
                    IsPopupOpenAdd = true;

                }
                else
                {
                    IsPopupOpenAdd = false;
                    int id;
                    GenreSeat genreseat = new GenreSeat();
                    (id, genreseat) = await GenreService.Ins.FindGenreSeat(GenreName);
                    Seat newseat = new Seat
                    {
                        Status = Status,
                        IDGenre = id,
                        IsDeleted = false,
                    };
                    resetdata();
                    (bool IsAdded, string messageAdd) = await SeatService.Ins.AddNewSeat(newseat);
                    if(IsAdded)
                    {
                        UpdateBtn();
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Bạn đã thêm thành công");                        
                    }    
                    else
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Bạn đã thêm thất bại");
                    }    
                }    
            });
            Edit = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                IsPopupOpenEdit = false;
                int id;
                GenreSeat genreseat = new GenreSeat();
                (id, genreseat) = await GenreService.Ins.FindGenreSeat(genreName);              
                Seat newseat = new Seat
                {
                    ID = this.ID,
                    Status = Status,
                    IDGenre = id,
                    IsDeleted = false,
                };
                resetdata();
                (bool success, string messageEdit) = await SeatService.Ins.EditSeat(newseat);
                if(success)
                {                   
                    UpdateBtn() ;
                    MessageBoxCustom.Show(MessageBoxCustom.Success, "Bạn đã sửa thành công");                   
                }
                else
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Bạn đã sửa thất bại");
                }

            });
            Classify = new RelayCommand<Button>((p) => { return true; }, async (p) =>
            {
                TableList = new ObservableCollection<SeatDTO>(await SeatService.Ins.GetAllSeat());
                tablelist = new List<SeatDTO>(TableList);

                if (Genre == "Tất cả loại bàn")
                {
                    switch (p.Content.ToString())
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
                    switch (p.Content.ToString())
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
                
            });
           


        }
    }
}