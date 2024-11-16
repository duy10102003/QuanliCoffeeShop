using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.Model;
using QuanLiCoffeeShop.Model.Service;
using QuanLiCoffeeShop.View.Admin.Problem;
using QuanLiCoffeeShop.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using QuanLiCoffeeShop.View.Admin.Problem.Problem_page_main;


namespace QuanLiCoffeeShop.ViewModel.AdminVM.ProblemVM
{
    internal class ProblemViewModel : BaseViewModel
    {
        public static List<ErrorDTO> ProList;
        private ObservableCollection<ErrorDTO> _problemList;

        public ObservableCollection<ErrorDTO> ProblemList
        {
            get { return _problemList; }
            set { _problemList = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> statuslist;

        public ObservableCollection<string> StatusList
        {
            get { return statuslist; }
            set { statuslist = value; OnPropertyChanged(); }
        }

        private ErrorDTO _selectedItem;

        public ErrorDTO SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }
        private string _name;
        public string Name
        { get { return _name; } set { _name = value; OnPropertyChanged(); } }

        private string _status;
        public string Status
        { get { return _status; } set { _status = value; OnPropertyChanged(); } }

        private string _description;
        public string Description
        { get { return _description; } set { _description = value; OnPropertyChanged(); } }
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
        private bool isPopupOpenAdd = false;
        private bool isOpenMain = true;
        public bool IsOpenMain { get { return isOpenMain; } set { isOpenMain = value; OnPropertyChanged(nameof(IsOpenMain)); } }
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

        public ICommand FirstLoadProblem { get; set; }
        public ICommand OpenAdd { get; set; }
        public ICommand CloseAdd { get; set; }
        public ICommand AddProblem { get; set; }
        public ICommand Edit { get; set; }
        public ICommand OpenEdit { get; set; }
        public ICommand CloseEdit { get; set; }
        public ICommand OpenDelete { get; set; }
        public ICommand CloseDelete { get; set; }
        public ICommand Search { get; set; }
        private void resetdata()
        {
            Name = null; Description=null;Status = null;
        }
        private void ExecuteOpenEdit(ErrorDTO Item)
        {
            IsPopupOpenEdit = true;
            SelectedItem = null;
          SelectedItem = Item;
        }
        private void ExecuteOpenDelete(ErrorDTO Item)
        {          
            SelectedItem = null;
            SelectedItem = Item;
        }

        public ProblemViewModel()
        {
            FirstLoadProblem = new RelayCommand<Page>((p) => { return true; }, async (p) =>
            {
                StatusList = new ObservableCollection<string>();
                StatusList.Add("Đã sửa");
                StatusList.Add("Đang sửa chữa");
                ProblemList = new ObservableCollection<ErrorDTO>(await ErrorService.Ins.GetAllError());
                ProList = new List<ErrorDTO>(ProblemList);
            });
            OpenAdd = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsPopupOpenAdd = true;
            });
            CloseAdd = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsPopupOpenAdd = false;
                resetdata();
            });
            AddProblem = new RelayCommand<object>((p) => { return true; },async (p) =>
            {
                if (Name == null || Status == null)
                {
                    IsPopupOpenAdd = false;
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Bạn đã nhập thiếu thông tin");
                    IsPopupOpenAdd = true;
                }
                else
                {
                    IsPopupOpenAdd = false;
                    if (Description == null) { Description = ""; }
                    Model.Error newerror = new Model.Error()
                    {
                        DisplayName = Name,
                        Status = Status,
                        Description = Description,
                        IsDeleted = false,

                    };
                    resetdata();
                    (bool IsAdded, string messageAdd) = await ErrorService.Ins.AddNewError(newerror);
                    if (IsAdded)
                    {
                        ProblemList = new ObservableCollection<ErrorDTO>(await ErrorService.Ins.GetAllError());
                         ProList = new List<ErrorDTO>(ProblemList);
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Bạn đã thêm thành công");           
                    }
                    else
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Bạn đã thêm thất bại");
                    }    
                   
                }

            });
            OpenEdit = new RelayCommand<ErrorDTO>((p) => { return true; }, (p)=>
            {
                ExecuteOpenEdit(p);
                Name = SelectedItem.DisplayName;
                Status = SelectedItem.Status;
                Description = SelectedItem.Description;            
            });
            CloseEdit = new RelayCommand<object>((p) => { return true; }, (p) => 
            { 
                IsPopupOpenEdit = false;
                resetdata() ;               
            });
            Edit=new RelayCommand<object>((p) => { return true; },async (p)=>
            {

                if (Status == "")
                {
                    isPopupOpenEdit = false;
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Bạn đã nhập thiếu dữ liệu");
                    IsPopupOpenEdit= true;
                    
                }
                else
                {
                    IsPopupOpenEdit = false;
                    if (Description == null) { Description = ""; }
                    Model.Error newerror = new Model.Error()
                    {
                        ID = SelectedItem.ID,
                        DisplayName = SelectedItem.DisplayName,
                        Status = this.Status,
                        Description = this.Description,
                        IsDeleted = false,
                    };
                    resetdata();
                    (bool success, string messageEdit) = await ErrorService.Ins.EditError(newerror);
                    if (success)
                    {
                        ProblemList = new ObservableCollection<ErrorDTO>(await ErrorService.Ins.GetAllError());
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Bạn đã sửa thành công");
                       
                    }
                    else
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Sửa thất bại");
                    }    
                }
                                      
            });
            OpenDelete = new RelayCommand<ErrorDTO>((p) => { return true; },async (p) =>
            {
               
                DeleteMessage wd = new DeleteMessage();
                wd.ShowDialog();
                if (wd.DialogResult == true)
                {
                    ExecuteOpenDelete(p);
                    (bool sucess, string messageDelete) = await ErrorService.Ins.DeleteError(SelectedItem.ID);
                    if (sucess)
                    {
                        ProblemList.Remove(SelectedItem);
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Bạn đã xóa thành công");
                    }
                    else
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Xóa thất bại");
                    }    
                }             

            });
            Search = new RelayCommand<TextBox>((p) => { return true; },async (p) =>
            {
                if(p.Text == "" )
                {
                    ProblemList = new ObservableCollection<ErrorDTO>(await ErrorService.Ins.GetAllError());
                }    
                else
                {
                    ProblemList = new ObservableCollection<ErrorDTO>(await ErrorService.Ins.GetAllError());
                    ProList = new List<ErrorDTO>(ProblemList);
                    ProblemList = new ObservableCollection<ErrorDTO>(ProList.FindAll(x => x.DisplayName.ToLower().Contains(p.Text.ToLower())));
                }    
            });
        }
    }
}
