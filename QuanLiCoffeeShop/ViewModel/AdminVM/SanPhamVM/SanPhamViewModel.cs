using QuanLiCoffeeShop.DTOs;
using QuanLiCoffeeShop.Model;
using QuanLiCoffeeShop.Model.Service;
using QuanLiCoffeeShop.Utils;
using QuanLiCoffeeShop.View.Admin.SanPham;
using QuanLiCoffeeShop.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Microsoft.Win32;
using System.Diagnostics.Eventing.Reader;

namespace QuanLiCoffeeShop.ViewModel.AdminVM.SanPhamVM
{
    public class SanPhamViewModel : BaseViewModel
    {
        public static List<ProductDTO> prdList;
        private ObservableCollection<ProductDTO> _productList;

        public ObservableCollection<ProductDTO> ProductList
        {
            get { return _productList; }
            set {  _productList = value; OnPropertyChanged(); }
        }

        private ProductDTO _selectedItem;

        public ProductDTO SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }


        private ObservableCollection <string> _genreList;
        public ObservableCollection <string> GenreList
        {
            get { return _genreList;}
            set { _genreList = value;OnPropertyChanged(); }
        }

        //Add page
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        private string _price;
        public string Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(); }
        }

        private string _genre;
        public string Genre
        {
            get { return _genre; }
            set { _genre = value; OnPropertyChanged(); }
        }

        private string _count;
        public string Count
        {
            get { return _count; }
            set { _count = value; OnPropertyChanged(); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

        private string _image;
        public string Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged(); }
        }

        //Edit page
        private string _editname;
        public string EditName
        {
            get { return _editname; }
            set { _editname = value; OnPropertyChanged(); }
        }

        private string _editPrice;
        public string EditPrice
        {
            get { return _editPrice; }
            set { _editPrice = value; OnPropertyChanged(); }
        }

        private string _editGenre;
        public string EditGenre
        {
            get { return _editGenre; }
            set { _editGenre = value; OnPropertyChanged(); }
        }

        private string _editCount;
        public string EditCount
        {
            get { return _editCount; }
            set { _editCount = value; OnPropertyChanged(); }
        }

        private string _editDescription;
        public string EditDescription
        {
            get { return _editDescription; }
            set { _editDescription = value; OnPropertyChanged(); }
        }

        private string _editImage;
        public string EditImage
        {
            get { return _editImage; }
            set { _editImage = value; OnPropertyChanged(); }
        }
        private string OriginImage { get; set; }

        public ICommand FirstLoadCM { get; set; }
        public ICommand SearchSanPhamCM { get; set; }
        public ICommand AddSanPhamCM { get; set; }
        public ICommand AddSanPhamListCM { get; set; }
        public ICommand ProductFilter {  get; set; }
        public ICommand AllPrDFilter { get; set; }
        public ICommand CloseWdCM { get; set; }
        public ICommand OpenEditWdCM { get; set; }
        public ICommand EditSanPhamListCM { get; set; }
        public ICommand DeleteSanPhamListCM { get; set; }
        public ICommand UploadImageCM { get; set; }
        public ICommand EditImageCM { get; set; }
        public SanPhamViewModel()
        {
            FirstLoadCM = new RelayCommand<Page>((p) => { return true; }, async (p) =>
            {
                ProductList = new ObservableCollection<ProductDTO>(await ProductService.Ins.GetAllProduct());
                if (ProductList != null)
                {
                    prdList = new List<ProductDTO>(ProductList);
                }
                GenreList = new ObservableCollection<string>(await GenreService.Ins.GetAllPrD());
            });
            ProductFilter = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                ProductList = new ObservableCollection<ProductDTO>(prdList.FindAll(x => x.GenreName.ToLower().Contains(p.Text.ToString().ToLower() ) && x.IsDeleted == false));
            });
            AllPrDFilter = new RelayCommand<Button>((p) => { return true; }, async (p) =>
            {
                ProductList = new ObservableCollection<ProductDTO>(prdList);
            });
            SearchSanPhamCM = new RelayCommand<TextBox>((p) => { return true; }, async (p) =>
            {
                if (p.Text == "")
                {
                    ProductList = new ObservableCollection<ProductDTO>(await ProductService.Ins.GetAllProduct());
                }
                else
                {
                    ProductList = new ObservableCollection<ProductDTO>(prdList.FindAll(x => x.DisplayName.ToLower().Contains(p.Text.ToLower()) && x.IsDeleted == false));
                }

            });            


            AddSanPhamCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Name = null;
                Genre = null;
                Price = null;
                Image = null;
                Count = null;
                Description = null;
                AddSanPham wd = new AddSanPham();
                wd.ShowDialog();

            });
            AddSanPhamListCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {               

                if (this.Name == null || this.Genre == null || this.Image == null)
                {                    
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Không nhập đủ dữ liệu!");
                }
                else
                {
                    int id; 
                    GenreProduct genrePrD = new GenreProduct();
                    (id, genrePrD) = await GenreService.Ins.FindGenrePrD(Genre);
                    if (this.Description == null) this.Description = "";
                    Product newPrd = new Product
                    {
                        DisplayName = this.Name,
                        Price = decimal.Parse(this.Price),
                        Description = this.Description,                                           
                        IDGenre = id,
                        Count=int.Parse(this.Count),
                        Image= await CloudService.Ins.UploadImage(this.Image),
                        IsDeleted = false,

                    };
                    if(newPrd.Image!= null)
                    {
                        (bool IsAdded, string messageAdd) = await ProductService.Ins.AddNewPrD(newPrd);
                        if (IsAdded)
                        {
                            p.Close();
                            ProductList = new ObservableCollection<ProductDTO>(await ProductService.Ins.GetAllProduct());
                            prdList = new List<ProductDTO>(ProductList);
                            MessageBoxCustom.Show(MessageBoxCustom.Success, "Thêm thành công");
                            //AddedSuccessfully addedSuccessfully = new AddedSuccessfully();
                            //addedSuccessfully.Show();
                        }
                        else
                        {
                            MessageBoxCustom.Show(MessageBoxCustom.Error, messageAdd);
                        }
                    }
                    else
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Tải ảnh lên thất bại");
                    }
                }

            });

            UploadImageCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.jpg;*.png;*.jpeg;*.gif|All Files|*.*";
                if (openFileDialog.ShowDialog() == true)
                {                   
                    Image = openFileDialog.FileName;                  
                    if (Image != null)
                    {
                        // Image was uploaded successfully.                        
                    }
                    else
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Tải ảnh lên thất bại!");
                    }                   
                }

            });

            EditImageCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {              

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.jpg;*.png;*.jpeg;*.webp;*.gif|All Files|*.*";
                if (openFileDialog.ShowDialog() == true)
                {

                    EditImage = openFileDialog.FileName;
                    if (EditImage != null)
                    {
                        // Image was uploaded successfully.                        
                    }
                    else
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Tải ảnh lên thất bại!");
                    }
                }
            });

            CloseWdCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {                
                EditName = null;
                EditGenre = null;
                EditPrice = null;
                EditCount = null;
                EditImage = null;
                EditDescription = null;
                OriginImage = null;
                Name = null;
                Genre = null;
                Price = null;
                Image = null;
                Count = null;
                Description = null;
                p.Close();
            });

            OpenEditWdCM = new RelayCommand<ProductDTO>((p) => { return true; }, (p) => {
                SelectedItem = p;
                ProductDTO a = new ProductDTO();
                a = SelectedItem;

                EditName = SelectedItem.DisplayName;
                EditGenre = SelectedItem.GenreName;
                EditPrice = FormalPrice(SelectedItem.Price.ToString());
                EditCount = SelectedItem.Count.ToString();
                EditImage = SelectedItem.Image;
                EditDescription = SelectedItem.Description;
                OriginImage = SelectedItem.Image;                
                EditSanPham wd = new EditSanPham();
                wd.ShowDialog();
            });

            EditSanPhamListCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {                             
                if (SelectedItem == null)
                    MessageBox.Show("SelectedItem null");
                else
                {
                if (this.EditName == null || this.EditGenre == null || this.EditImage == null)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Không nhập đủ dữ liệu!");
                }
                else
                {
                    if (this.EditDescription == null) this.EditDescription = "";
                    int id;

                    GenreProduct genrePrD = new GenreProduct();
                    (id, genrePrD) = await GenreService.Ins.FindGenrePrD(EditGenre);
                    if (OriginImage != EditImage)
                        {
                            await CloudService.Ins.DeleteImage(OriginImage);
                            EditImage = await CloudService.Ins.UploadImage(EditImage);
                        }

                    Product newPrD = new Product
                    {
                        ID = SelectedItem.ID,
                        DisplayName = EditName,
                        IDGenre = id,
                        Price = decimal.Parse(EditPrice),
                        Count = int.Parse(EditCount),
                        Image = EditImage,
                        Description = EditDescription,
                        IsDeleted = false,
                    };
                    (bool success, string messageEdit) = await ProductService.Ins.EditPrD(newPrD, SelectedItem.ID);
                    if (success)
                    {
                        ProductList = new ObservableCollection<ProductDTO>(await ProductService.Ins.GetAllProduct());
                        prdList = new List<ProductDTO>(ProductList);
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Sửa thành công");
                        closingWd(p);
                    }
                    else
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, messageEdit);
                    }
                }
                }

            });
            DeleteSanPhamListCM = new RelayCommand<ProductDTO>((p) => { return true; }, async (p) =>
            {
                SelectedItem = p;
                ProductDTO a = new ProductDTO();
                a = SelectedItem;

                DeleteMessage wd = new DeleteMessage();
                wd.ShowDialog();                           
                if (wd.DialogResult == true)
                {
                    string deleteImg = SelectedItem.Image;
                    if(deleteImg != null)
                        await CloudService.Ins.DeleteImage(deleteImg);
                    (bool sucess, string messageDelete) = await ProductService.Ins.DeletePrD(SelectedItem.ID);
                    if (sucess)
                    {
                        ProductList.Remove(SelectedItem);
                        prdList = new List<ProductDTO>(ProductList);
                        // Dương sửa phần messageBox
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Bạn đã xóa thành công");
                    }
                    else
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, messageDelete);
                    }
                }               
            });
        }       

        private string FormalPrice(string s)
        {
            string valuePrice ="";            
            valuePrice += s.Substring(0,s.Length-5);
            return valuePrice;
        }
        private void closingWd(Window p)
        {
            EditName = null;
            EditGenre = null;
            EditPrice = null;
            EditCount = null;
            EditImage = null;
            EditDescription = null;
            OriginImage = null;
            Name = null;
            Genre = null;
            Price = null;
            Image = null;
            Count = null;
            Description = null;
            SelectedItem = null;
            p.Close();
        }
    }
}
