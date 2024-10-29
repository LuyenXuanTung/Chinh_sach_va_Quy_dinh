using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TTCSN.Models;
using Xceed.Words.NET;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace TTCSN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static QlqdcsContext db = new QlqdcsContext();

        public MainWindow()
        {
            InitializeComponent();
            showListNV();
            showListCS();
            ShowcbbPB();
            ShowChooseCS();
            NameAdmin();
        }
        private void NameAdmin()
        {
            var name = (from lad in db.NgQls
                        where lad.MaAdm == DangNhap._admin
                        select lad.TenAdmin).SingleOrDefault();
            if (name != null)
            {
                NameUser.Text = name;
            }

        }
        private void ShowcbbPB()
        {
            var queryPB = from lpb in db.PhongBans
                          select lpb.TenPhong;
            cbbPB.ItemsSource = queryPB.ToList();
        }
        public void showListNV()
        {
            dtgListNV.ItemsSource = null;
            var queryNV = from lnv in db.Nhanviens
                          join lpb in db.PhongBans
                          on lnv.MaPhong equals lpb.MaPhong
                          select new
                          {
                              MA = lnv.MaNv,
                              TEN = lnv.HoTen,
                              MK = lnv.MatKhau,
                              NS = lnv.NgaySinh,
                              GT = lnv.GioiTinh,
                              CV = lnv.ChucVu,
                              DC = lnv.DiaChi,
                              EM = lnv.Email,
                              SDT = lnv.Sdt,
                              PB = lpb.TenPhong,
                              LCB = lnv.Luong,
                              TH = lnv.Thuong,
                              PH = lnv.Phat,
                              TC = lnv.TangCa,
                              NG = lnv.Nghi,
                              KN = lnv.KinhNghiem,
                              LOI = lnv.Loi,
                              HT = lnv.HoTro
                          };

            dtgListNV.ItemsSource = queryNV.ToList();
        }

        public void showListCS()
        {
            dtgListCS.ItemsSource = null;
            var queryCS = from lcs in db.ChinhSaches
                          select new
                          {
                              MACS = lcs.MaChinhSach,
                              TENCS = lcs.ChinhSach1,
                              NAP = lcs.NgayApDung,
                              NHH = lcs.NgayHetHan,
                              MT = lcs.Mota,
                              PB = lcs.PhienBan
                          };
            dtgListCS.ItemsSource = queryCS.ToList();

        }
        //private void dtgListNV_SelectedCellsChanged(object sender, RoutedEventArgs e)
        // {
        //    var item = dtgListNV.SelectedItem;
        //    if (item != null)
        //   {

        //    }
        // }
        private void ShowChooseCS()
        {

            var queryCS = from lcs in db.ChinhSaches
                          select new { lcs.MaChinhSach, lcs.ChinhSach1 };
            foreach (var item in queryCS)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = item.ChinhSach1;
                checkBox.Tag = item.MaChinhSach;
                lbCS.Children.Add(checkBox);
            }

            // List<string> sl = lbCS.SelectedItems.ToString();
        }

        private void tabControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TabControl tabControl = sender as TabControl;
            // TabItem tabItem = tabControl.SelectedItem as TabItem;
            lbCS.Children.Clear();
            ShowChooseCS();
        }

        //Check ma nhan vien xem da ton tai chua
        private bool checkMaNV(string a)
        {
            var manv = (from lnv in db.Nhanviens
                        where lnv.MaNv == a
                        select lnv.MaNv).Single();
            if (manv == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Ham nhap them nhan vien
        private void btnAddNv_Click(object sender, RoutedEventArgs e)
        {
            if (check() == true)
            {
                Nhanvien nv = new Nhanvien();
                nv.MaNv = txtMaNV.Text;
                nv.HoTen = txtTenNV.Text;
                nv.MatKhau = txtMK.Text;
                nv.NgaySinh = (DateTime)txtNS.SelectedDate;
                nv.ChucVu = txtChucVu.Text;
                nv.DiaChi = txtDiaChi.Text;
                nv.Sdt = txtSDT.Text;
                nv.TruyCap = DateTime.Now;
                if (rNam.IsChecked == true)
                {
                    nv.GioiTinh = "Nam";
                }
                else
                {
                    nv.GioiTinh = "Nữ";
                }
                var maph = (from pb in db.PhongBans
                            where pb.TenPhong == cbbPB.Text
                            select pb.MaPhong).Single();
                nv.MaPhong = maph.ToString();
                nv.Email = txtEmail.Text;
                try
                {
                    nv.Luong = Double.Parse(txtLuong.Text);
                }
                catch
                {
                    MessageBox.Show("Nhập lại lương", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                try
                {
                    nv.Thuong = Double.Parse(txtThuong.Text);
                }
                catch
                {
                    MessageBox.Show("Nhập lại thưởng", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                try
                {
                    nv.Phat = Double.Parse(txtPhat.Text);
                }
                catch
                {
                    MessageBox.Show("Nhập lại phạt", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                try
                {
                    nv.TangCa = Double.Parse(txtTangCa.Text);
                }
                catch
                {
                    MessageBox.Show("Nhập lại tăng ca", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                try
                {
                    nv.Nghi = Double.Parse(txtNghi.Text);
                }
                catch
                {
                    MessageBox.Show("Nhập lại ngày nghỉ", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                try
                {
                    nv.KinhNghiem = Double.Parse(txtNamKN.Text);
                }
                catch
                {
                    MessageBox.Show("Nhập lại kinh nghiệm", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                
                nv.Loi = txtLoi.Text;
                nv.HoTro = txtHoTro.Text;
                var manv = (from lnv in db.Nhanviens
                            where lnv.MaNv == txtMaNV.Text
                            select lnv.MaNv).SingleOrDefault();
                if (manv == null)
                {
                    db.Nhanviens.Add(nv);
                    db.SaveChanges();
                    foreach (CheckBox check in lbCS.Children)
                    {
                        if (check.IsChecked == true)
                        {
                            Nvc nvc = new Nvc();
                            nvc.MaNv = nv.MaNv;
                            nvc.MaChinhSach = (string)check.Tag;
                            db.Nvcs.Add(nvc);
                            db.SaveChanges();
                        }
                    }
                    var timnv = (from lnv in db.Nhanviens
                                 where lnv.MaNv == nv.MaNv
                                 select lnv.MaNv).SingleOrDefault();
                    if (timnv == null)
                    {
                        MessageBox.Show("Thêm nhân viên không thành công", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Thêm nhân viên thành công", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Mã nhân viên đã tồn tại", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtMaNV.Focus();
                }

                showListNV();
                clear();

            }
        }
        //Ham check thong bao ngoai le khong dien gia tri hoac chuan du lieu dau vao
        private bool check()
        {
            if (txtMaNV.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập mã nhân viên", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtMaNV.Focus();
                return false;
            }
            if (txtMaNV.Text.Any(c => c == ' '))
            {
                MessageBox.Show("Mã nhân viên phải không có khoảng trắng giữa các từ.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtMaNV.Focus();
                return false;
            }
            if (txtTenNV.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập tên nhân viên", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTenNV.Focus();
                return false;
            }
            if (txtMK.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtMK.Focus();
                return false;
            }
            if (txtMK.Text.Any(c => c == ' '))
            {
                MessageBox.Show("Mật khẩu phải không có khoảng trắng giữa các từ.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtMK.Focus();
                return false;
            }
            if (txtNS.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập ngày sinh", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNS.Focus();
                return false;
            }


            DateTime ns = (DateTime)txtNS.SelectedDate;
            if ((DateTime.Now.Year - ns.Year) < 18)
            {
                MessageBox.Show("Nhân viên phải từ đủ 18 tuổi", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (txtSDT.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtSDT.Focus();
                return false;
            }
            if (txtChucVu.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập chức vụ", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtChucVu.Focus();
                return false;
            }
            if (txtDiaChi.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtDiaChi.Focus();
                return false;
            }
            if (txtEmail.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập Email", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmail.Focus();
                return false;
            }
            if (txtLuong.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập tiền lương", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtLuong.Focus();
                return false;
            }
            if (txtThuong.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập tiền thưởng", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtThuong.Focus();
                return false;
            }
            if (txtPhat.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập tiền phạt", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtThuong.Focus();
                return false;
            }
            if (txtTangCa.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập ngày tăng ca", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTangCa.Focus();
                return false;
            }
            if (txtNghi.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập ngày nghỉ", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNghi.Focus();
                return false;
            }
            if (txtNamKN.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa năm kinh nghiệm", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNamKN.Focus();
                return false;
            }
            if (txtLoi.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập lỗi", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtLoi.Focus();
                return false;
            }
            if (txtHoTro.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập hỗ trợ", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtHoTro.Focus();
                return false;
            }
            return true;
        }
        //xoa du lieu trong bo nhap du lieu
        public void clear()
        {
            txtMaNV.Text = string.Empty;
            txtTenNV.Text = string.Empty;
            txtMK.Text = string.Empty;
            txtNS.SelectedDate = DateTime.Parse("01/01/2000");
            txtSDT.Text = string.Empty;
            txtChucVu.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtEmail.Text = string.Empty;
            rNam.IsChecked = true;
            cbbPB.SelectedIndex = 0;
            txtLuong.Text = string.Empty;
            txtThuong.Text = string.Empty;
            txtPhat.Text = string.Empty;
            txtTangCa.Text = string.Empty;
            txtNghi.Text = string.Empty;
            txtNamKN.Text = string.Empty;
            txtLoi.Text = string.Empty;
            txtHoTro.Text = string.Empty;
            txtNgSua.Text = string.Empty;
            txtMaNgSua.Text = string.Empty;
            txtTgianSua.SelectedDate = DateTime.Parse("01/01/2000");
            foreach (CheckBox check in lbCS.Children)
            {
                check.IsChecked = false;
            }

        }


        //chon dong => show du lieu nhan vien xuong duoi
        private void dtgListNV_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dtgListNV.SelectedItem != null)
            {
                var item = dtgListNV.SelectedItem;
                var c = (from lnv in db.Nhanviens
                         where lnv.MaNv == (dtgListNV.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text
                         select lnv).Single();
                if (c != null)
                {
                    clear();
                    txtMaNV.Text = c.MaNv.Trim();
                    txtTenNV.Text = c.HoTen;
                    txtMK.Text = c.MatKhau;
                    txtNS.SelectedDate = c.NgaySinh;
                    txtSDT.Text = c.Sdt;
                    txtEmail.Text = c.Email;
                    txtLuong.Text = c.Luong.ToString();
                    txtThuong.Text = c.Thuong.ToString();
                    txtPhat.Text = c.Phat.ToString();
                    txtTangCa.Text = c.TangCa.ToString();
                    txtNghi.Text = c.Nghi.ToString();
                    txtNamKN.Text = c.KinhNghiem.ToString();
                    txtLoi.Text = c.Loi;
                    txtHoTro.Text = c.HoTro;

                    if (c.GioiTinh == "Nam")
                    {
                        rNam.IsChecked = true;
                    }
                    else
                    {
                        rNu.IsChecked = true;
                    }
                    txtChucVu.Text = c.ChucVu;
                    txtDiaChi.Text = c.DiaChi;
                    cbbPB.SelectedItem = (dtgListNV.SelectedCells[9].Column.GetCellContent(item) as TextBlock).Text;
                    txtNgSua.Text = c.NguoiSua;
                    txtMaNgSua.Text = c.MaNguoiSua;
                    txtTgianSua.SelectedDate = c.NgaySua;
                    var lcs = (from lnv in db.Nhanviens
                               join lnvcs in db.Nvcs on lnv.MaNv equals lnvcs.MaNv
                               where lnv.MaNv == c.MaNv
                               select lnvcs.MaChinhSach
                               );

                    foreach (CheckBox check in lbCS.Children)
                    {
                        foreach (var cs in lcs)
                        {
                            if (cs == (string)check.Tag)
                            {
                                check.IsChecked = true;
                            }
                        }
                    }
                }
            }
        }

        private void btnEditNv_Click(object sender, RoutedEventArgs e)
        {
            if (dtgListNV.SelectedItem != null)
            {
                // if (dtgListNV.SelectedItem!=null)
                var item = dtgListNV.SelectedItem;
                var c = (from lnv in db.Nhanviens
                         where lnv.MaNv == (dtgListNV.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text.Trim()
                         select lnv).Single();

                var lcsXoa = (from lnvcs in db.Nvcs
                              where lnvcs.MaNv == c.MaNv
                              select lnvcs);
                foreach (var i in lcsXoa)
                {
                    db.Nvcs.Remove(i);
                }
                db.SaveChanges();
                db.Nhanviens.Remove(c);
                db.SaveChanges();
                /*Nhanvien nvSua = new Nhanvien();
                spSua.HoTen = txtTenNV.Text;
                spSua.MatKhau = txtMK.Text;
                spSua.NgaySinh = (DateTime)txtNS.SelectedDate;
                spSua.ChucVu = txtChucVu.Text;
                spSua.DiaChi = txtDiaChi.Text;
                spSua.Email = txtEmail.Text;
                spSua.Sdt = txtSDT.Text;
                spSua.TruyCap = DateTime.Now;
                spSua.Luong = Double.Parse(txtLuong.Text);
                spSua.Thuong = Double.Parse(txtThuong.Text);
                spSua.Phat = Double.Parse(txtPhat.Text);
                spSua.TangCa = Double.Parse(txtTangCa.Text);
                spSua.Nghi = Double.Parse(txtNghi.Text);
                spSua.Loi = txtLoi.Text;
                spSua.HoTro = txtHoTro.Text;
                if (rNam.IsChecked == true)
                {
                    spSua.GioiTinh = "Nam";
                }
                else
                {
                    spSua.GioiTinh = "Nữ";
                }
                var maph = (from pb in db.PhongBans
                            where pb.TenPhong == cbbPB.Text
                            select pb.MaPhong).Single();
                spSua.MaPhong = maph.ToString();
                spSua.Email = txtEmail.Text;
                spSua.MaNguoiSua = DangNhap._admin;
                var name = (from lad in db.NgQls
                            where lad.MaAdm == DangNhap._admin
                            select lad.TenAdmin).SingleOrDefault();
                if (name != null)
                {
                    spSua.NguoiSua = name;
                }
                spSua.NgaySua = DateTime.Now;


                db.SaveChanges();
                foreach (CheckBox check in lbCS.Children)
                {
                    if (check.IsChecked == true)
                    {
                        Nvc nvc = new Nvc();
                        nvc.MaNv = spSua.MaNv;
                        nvc.MaChinhSach = (string)check.Tag;
                        db.Nvcs.Add(nvc);
                        db.SaveChanges();
                    }
                }
                var timnv = (from lnv in db.Nhanviens
                             where lnv.MaNv == spSua.MaNv
                             select lnv.MaNv).SingleOrDefault();
                if (timnv == null)
                {
                    MessageBox.Show("Sửa nhân viên không thành công", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show("Sửa nhân viên thành công", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                */
                if (check() == true)
                {
                    Nhanvien nv = new Nhanvien();
                    nv.MaNv = txtMaNV.Text;
                    nv.HoTen = txtTenNV.Text;
                    nv.MatKhau = txtMK.Text;
                    nv.NgaySinh = (DateTime)txtNS.SelectedDate;
                    nv.ChucVu = txtChucVu.Text;
                    nv.DiaChi = txtDiaChi.Text;
                    nv.Sdt = txtSDT.Text;
                    nv.TruyCap = DateTime.Now;
                    if (rNam.IsChecked == true)
                    {
                        nv.GioiTinh = "Nam";
                    }
                    else
                    {
                        nv.GioiTinh = "Nữ";
                    }
                    var maph = (from pb in db.PhongBans
                                where pb.TenPhong == cbbPB.Text
                                select pb.MaPhong).Single();
                    nv.MaPhong = maph.ToString();
                    nv.Email = txtEmail.Text;
                    nv.Luong = Double.Parse(txtLuong.Text);
                    nv.Thuong = Double.Parse(txtThuong.Text);
                    nv.Phat = Double.Parse(txtPhat.Text);
                    nv.TangCa = Double.Parse(txtTangCa.Text);
                    nv.Nghi = Double.Parse(txtNghi.Text);
                    nv.KinhNghiem = Double.Parse(txtNamKN.Text);
                    nv.Loi = txtLoi.Text;
                    nv.HoTro = txtHoTro.Text;
                    nv.Email = txtEmail.Text;
                    nv.MaNguoiSua = DangNhap._admin;
                    var name = (from lad in db.NgQls
                                where lad.MaAdm == DangNhap._admin
                                select lad.TenAdmin).SingleOrDefault();
                    if (name != null)
                    {
                        nv.NguoiSua = name;
                    }
                    nv.NgaySua = DateTime.Now;
                    var manv = (from lnv in db.Nhanviens
                                where lnv.MaNv == nv.MaNv
                                select lnv.MaNv).SingleOrDefault();
                    if (manv == null)
                    {
                        QlqdcsContext ql = new QlqdcsContext();
                        ql.Nhanviens.Add(nv);
                        ql.SaveChanges();
                        foreach (CheckBox check in lbCS.Children)
                        {
                            if (check.IsChecked == true)
                            {
                                Nvc nvc = new Nvc();
                                nvc.MaNv = nv.MaNv;
                                nvc.MaChinhSach = (string)check.Tag;
                                ql.Nvcs.Add(nvc);
                                ql.SaveChanges();
                            }
                        }
                        var timnv = (from lnv in ql.Nhanviens
                                     where lnv.MaNv == nv.MaNv
                                     select lnv.MaNv).SingleOrDefault();
                        if (timnv == null)
                        {
                            MessageBox.Show("Sửa nhân viên không thành công", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Sửa nhân viên thành công", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã nhân viên đã tồn tại", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        txtMaNV.Focus();
                    }

                }

                showListNV();
                clear();

            }
            else
            {
                MessageBox.Show("Hãy chọn nhân viên", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void btnDeleteNv_Click(object sender, RoutedEventArgs e)
        {
            if (dtgListNV.SelectedItem != null)
            {
                var spXoa = db.Nhanviens.SingleOrDefault(lnv => lnv.MaNv == txtMaNV.Text);
                var lcsXoa = (from lnvcs in db.Nvcs
                              where lnvcs.MaNv == spXoa.MaNv
                              select lnvcs);
                foreach (var item in lcsXoa)
                {
                    db.Nvcs.Remove(item);
                }
                db.SaveChanges();
                db.Nhanviens.Remove(spXoa);
                db.SaveChanges();
                var timnv = (from lnv in db.Nhanviens
                             where lnv.MaNv == spXoa.MaNv
                             select lnv.MaNv).SingleOrDefault();
                if (timnv == null)
                {
                    MessageBox.Show("Xóa nhân viên thành công", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show("Xóa nhân viên không thành công", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                showListNV();
                clear();
            }
            else
            {
                MessageBox.Show("Hãy chọn nhân viên", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SearchNv_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearchNv.Text != null)
            {

                var timnv = (from lnv in db.Nhanviens
                             join lpb in db.PhongBans
                             on lnv.MaPhong equals lpb.MaPhong
                             where lnv.MaNv == txtSearchNv.Text
                             select new
                             {
                                 MA = lnv.MaNv,
                                 TEN = lnv.HoTen,
                                 MK = lnv.MatKhau,
                                 NS = lnv.NgaySinh,
                                 GT = lnv.GioiTinh,
                                 CV = lnv.ChucVu,
                                 DC = lnv.DiaChi,
                                 EM = lnv.Email,
                                 SDT = lnv.Sdt,
                                 PB = lpb.TenPhong,
                                 LCB = lnv.Luong,
                                 TH = lnv.Thuong,
                                 PH = lnv.Phat,
                                 TC = lnv.TangCa,
                                 NG = lnv.Nghi,
                                 KN = lnv.KinhNghiem,
                                 LOI = lnv.Loi,
                                 HT = lnv.HoTro

                             });
                // Nếu tìm thấy nhân viên
                if (timnv.Any())
                {
                    dtgListNV.ItemsSource = null;
                    // Gán dữ liệu tìm thấy cho DataGrid
                    dtgListNV.ItemsSource = timnv.ToList();
                }
                else
                {
                    // Thông báo không tìm thấy nhân viên
                    MessageBox.Show("Không có nhân viên có mã đã tìm", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);

                    // Hiển thị lại danh sách nhân viên
                    showListNV();
                }


            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên muốn tìm kiếm", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void txtSearchNv_GotFocus(object sender, RoutedEventArgs e)
        {
            clear();
        }
        public static string _url;
        public static string _NameUrl;
        private void UpFileButton_Click(object sender, RoutedEventArgs e)
        {
            chooseFile();
        }
        private void chooseFile()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Chọn file cần up";
            open.Filter = "Tất cả các file (*.*)|*.*";

            if (open.ShowDialog() == true)
            {

                string filePath = open.FileName;
                string fileName = System.IO.Path.GetFileName(filePath);


                fileUp.Text = filePath;
                TenFile.Text = fileName;
                _url = filePath;
                _NameUrl = fileName;

            }
        }
        private void fileUp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UpFileButton_Click(sender, e);
        }

        private void DeleteFileButton_Click(object sender, RoutedEventArgs e)
        {
            fileUp.Text = string.Empty;
            TenFile.Text = "Chưa có file nào được tải lên.";

            MessageBox.Show("File đã được xóa", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
        }


        private bool checkCS()
        {
            if (txtMaCS.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập mã chính sách", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtMaCS.Focus();
                return false;

            }
            if (txtMaCS.Text.Any(c => c == ' '))
            {
                MessageBox.Show("Mã chính sách phải không có khoảng trắng giữa các từ.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtMaCS.Focus();
                return false;
            }
            if (txtCS.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập tên chính sách", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtCS.Focus();
                return false;
            }


            if (txtNAPCS.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập ngày áp dụng chính sách", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNAPCS.Focus();
                return false;
            }
            if (txtNHHCS.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập ngày hết hạn chính sách", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNHHCS.Focus();
                return false;
            }
            if (txtNHHCS.SelectedDate.Value <= txtNAPCS.SelectedDate.Value)
            {
                MessageBox.Show("Ngày hết hạn phải có sau ngày áp dụng", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNHHCS.Focus();
                return false;
            }
            if (txtMoTaCS.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập mô tả chính sách", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtMoTaCS.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(fileUp.Text) || fileUp.Text == "Chưa có file nào được tải lên.")
            {
                MessageBox.Show("Bạn chưa tải file đính kèm", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                chooseFile();
                return false;
            }
            return true;
        }
        private void clearCS()
        {
            txtMaCS.Text = string.Empty;
            txtCS.Text = string.Empty;
            txtNAPCS.SelectedDate = DateTime.Parse("01/01/2000");
            txtNHHCS.SelectedDate = DateTime.Parse("01/01/2000");
            txtMoTaCS.Text = string.Empty;
            txtPB.Text = string.Empty;
            fileUp.Text = string.Empty;
            TenFile.Text = string.Empty;
        }
        private void btnAddCS_Click(object sender, RoutedEventArgs e)
        {
            if (checkCS() == true)
            {
                ChinhSach cs = new ChinhSach();
                cs.MaChinhSach = txtMaCS.Text;
                cs.ChinhSach1 = txtCS.Text;
                cs.NgayApDung = (DateTime)txtNAPCS.SelectedDate;
                cs.NgayHetHan = (DateTime)txtNHHCS.SelectedDate;
                cs.Mota = txtMoTaCS.Text;
                cs.DuongDan = fileUp.Text;
                cs.PhienBan = txtPB.Text;
                var querycs = (from lcs in db.ChinhSaches
                               where lcs.MaChinhSach == cs.MaChinhSach
                               select lcs.MaChinhSach).SingleOrDefault();
                if (querycs == null)
                {
                    db.ChinhSaches.Add(cs);
                    db.SaveChanges();
                    var Searchcs = (from lcs in db.ChinhSaches
                                    where lcs.MaChinhSach == cs.MaChinhSach
                                    select lcs.MaChinhSach).SingleOrDefault();
                    if (Searchcs != null)
                    {
                        MessageBox.Show("Thêm chính sách thành công", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Thêm chính sách không thành công", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Mã chính sách đã tồn tại", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                showListCS();
                clearCS();
            }
        }

        private void btnDeleteCS_Click(object sender, RoutedEventArgs e)
        {
            if (dtgListCS.SelectedItem != null)
            {
                var querycs = (from lcs in db.ChinhSaches
                               where lcs.MaChinhSach == txtMaCS.Text
                               select lcs).SingleOrDefault();
                if (querycs != null)
                {
                    db.ChinhSaches.Remove(querycs);
                    db.SaveChanges();
                }
                var cs = (from lcs in db.ChinhSaches
                          where lcs.MaChinhSach == txtMaCS.Text
                          select lcs.MaChinhSach).SingleOrDefault();
                if (cs == null)
                {
                    MessageBox.Show("Xóa chính sách thành công", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show("Xóa chính sách không thành công", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                showListCS();
                clearCS();
            }
            else
            {
                MessageBox.Show("Hãy chọn chính sách", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void dtgListCS_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dtgListCS.SelectedItem != null)
            {
                var item = dtgListCS.SelectedItem;
                var c = (from lcs in db.ChinhSaches
                         where lcs.MaChinhSach == (dtgListCS.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text
                         select lcs).Single();
                if (c != null)
                {
                    clear();
                    txtMaCS.Text = c.MaChinhSach.Trim();
                    txtCS.Text = c.ChinhSach1;
                    txtNAPCS.SelectedDate = c.NgayApDung;
                    txtNHHCS.SelectedDate = c.NgayHetHan;
                    txtMoTaCS.Text = c.Mota;
                    txtPB.Text = c.PhienBan;
                    fileUp.Text = c.DuongDan.Trim();
                    _url = fileUp.Text;
                    string filePath = c.DuongDan.ToString();
                    bool exists = File.Exists(filePath);
                    if (exists)
                    {
                        TenFile.Text = System.IO.Path.GetFileName(filePath);
                    }
                    else
                    {
                        fileUp.Text = string.Empty;
                        TenFile.Text = "Chưa có file nào được tải lên.";
                    }
                }
            }
        }

        private void btnEditCS_Click(object sender, RoutedEventArgs e)
        {
            if (dtgListCS.SelectedItem != null)
            {
                var cs = (from lcs in db.ChinhSaches
                          where lcs.MaChinhSach == txtMaCS.Text
                          select lcs).SingleOrDefault();
                if (checkCS() != null)
                {
                    cs.ChinhSach1 = txtCS.Text;
                    cs.NgayApDung = (DateTime)txtNAPCS.SelectedDate;
                    cs.NgayHetHan = (DateTime)txtNHHCS.SelectedDate;
                    cs.Mota = txtMoTaCS.Text;
                    cs.DuongDan = fileUp.Text;
                    cs.PhienBan = txtPB.Text;
                    db.SaveChanges();
                    var tcs = (from lcs in db.ChinhSaches
                               where lcs.MaChinhSach == txtMaCS.Text
                               select lcs.MaChinhSach).SingleOrDefault();
                    if (tcs != null)
                    {
                        MessageBox.Show("Sửa chính sách thành công", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Sửa chính sách không thành công", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                showListCS();
                clearCS();
            }
            else
            {
                MessageBox.Show("Hãy chọn chính sách", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }

        private void SearchCS_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearchCS.Text != null)
            {

                var timcs = (from lcs in db.ChinhSaches
                             where lcs.MaChinhSach == txtSearchCS.Text
                             select new
                             {
                                 MACS = lcs.MaChinhSach,
                                 TENCS = lcs.ChinhSach1,
                                 NAP = lcs.NgayApDung,
                                 NHH = lcs.NgayHetHan,
                                 MT = lcs.Mota

                             });
                // Nếu tìm thấy chính sách
                if (timcs.Any())
                {
                    dtgListNV.ItemsSource = null;
                    // Gán dữ liệu tìm thấy cho DataGrid
                    dtgListCS.ItemsSource = timcs.ToList();
                }
                else
                {
                    // Thông báo không tìm thấy chính sách
                    MessageBox.Show("Không có chính sách có mã đã tìm", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);

                    // Hiển thị lại danh sách chính sách
                    showListCS();
                }

            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã chính sách muốn tìm kiếm", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void txtSearchCS_GotFocus(object sender, RoutedEventArgs e)
        {
            clearCS();
        }

        private void ReadFileButton_Click(object sender, RoutedEventArgs e)
        {
            ReadFile read = new ReadFile();
            read.Show();
        }

        private void DangXuat_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn muốn đăng xuất?", "Thông báo", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                DangNhap dn = new DangNhap();
                dn.Show();
                this.Hide();
            }
        }

        private void CanBo_Click(object sender, RoutedEventArgs e)
        {
            var cs = (from lcs in db.ChinhSaches
                      where lcs.MaChinhSach == "CS06"
                      select lcs.MaChinhSach
                       ).SingleOrDefault();

            foreach (CheckBox check in lbCS.Children)
            {
                check.IsChecked = true;

                if (cs != null)
                {
                    if (cs == (string)check.Tag)
                    {
                        check.IsChecked = false;
                    }
                }

            }
        }

        private void NVCT_Click(object sender, RoutedEventArgs e)
        {
            var cs = (from lcs in db.ChinhSaches
                      where lcs.MaChinhSach == "CS10"
                      select lcs.MaChinhSach
                       ).SingleOrDefault();

            foreach (CheckBox check in lbCS.Children)
            {
                check.IsChecked = true;

                if (cs != null)
                {
                    if (cs == (string)check.Tag)
                    {
                        check.IsChecked = false;
                    }
                }

            }
            var cs2 = (from lcs in db.ChinhSaches
                       where lcs.MaChinhSach == "CS06"
                       select lcs.MaChinhSach
                       ).SingleOrDefault();

            foreach (CheckBox check in lbCS.Children)
            {

                if (cs != null)
                {
                    if (cs2 == (string)check.Tag)
                    {
                        check.IsChecked = false;
                    }
                }

            }
        }

        private void TTS_Click(object sender, RoutedEventArgs e)
        {
            var cs = (from lcs in db.ChinhSaches
                      where lcs.MaChinhSach == "CS10"
                      select lcs.MaChinhSach
                       ).SingleOrDefault();

            foreach (CheckBox check in lbCS.Children)
            {
                check.IsChecked = true;

                if (cs != null)
                {
                    if (cs == (string)check.Tag)
                    {
                        check.IsChecked = false;
                    }
                }

            }
            var cs2 = (from lcs in db.ChinhSaches
                       where lcs.MaChinhSach == "CS05"
                       select lcs.MaChinhSach
                       ).SingleOrDefault();

            foreach (CheckBox check in lbCS.Children)
            {

                if (cs != null)
                {
                    if (cs2 == (string)check.Tag)
                    {
                        check.IsChecked = false;
                    }
                }

            }
        }
        public static string _ctcs;
        private void btnCTCS_Click(object sender, RoutedEventArgs e)
        {
            if (dtgListCS.SelectedItem != null)
            {
                var item = dtgListCS.SelectedItem;
                var c = (from lcs in db.ChinhSaches
                         where lcs.MaChinhSach == (dtgListCS.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text
                         select lcs).Single();
                if (c != null)
                {
                    _ctcs = c.MaChinhSach;
                }
            }
            ChiTietCS ct = new ChiTietCS();
            ct.Show();
        }

        private void CSCanBo_Click(object sender, RoutedEventArgs e)
        {
            if (dtgListCS.SelectedItem != null)
            {
                var item = dtgListCS.SelectedItem;
                string ma = (dtgListCS.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text.Trim();
                var nv = from lnv in db.Nhanviens
                         where lnv.Luong >= 80000000
                         select lnv;
                List<Nhanvien> list = nv.ToList();
                foreach (var i in list)
                {
                    var ics = (from lnvcs in db.Nvcs
                               where lnvcs.MaNv == i.MaNv
                               where lnvcs.MaChinhSach == ma
                               select lnvcs).SingleOrDefault();
                    if (ics == null)
                    {
                        Nvc nvc = new Nvc();
                        nvc.MaChinhSach = ma;
                        nvc.MaNv = i.MaNv;
                        db.Nvcs.Add(nvc);
                        db.SaveChanges();
                        
                    }

                }
                MessageBox.Show("Thêm chính sách, quy định thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CSNVCT_Click(object sender, RoutedEventArgs e)
        {
            if (dtgListCS.SelectedItem != null)
            {
                var item = dtgListCS.SelectedItem;
                string ma = (dtgListCS.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                var nv = from lnv in db.Nhanviens
                         where lnv.Luong < 80000000
                         where lnv.Luong >= 8000000
                         select lnv;
                List<Nhanvien> list = nv.ToList();
                foreach (var i in list)
                {
                    var ics = (from lnvcs in db.Nvcs
                               where lnvcs.MaNv == i.MaNv
                               where lnvcs.MaChinhSach == ma
                               select lnvcs).SingleOrDefault();
                    if (ics == null)
                    {
                        Nvc nvc = new Nvc();
                        nvc.MaChinhSach = ma;
                        nvc.MaNv = i.MaNv;
                        db.Nvcs.Add(nvc);
                        db.SaveChanges();
                        
                    }

                }
                MessageBox.Show("Thêm chính sách, quy định thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CSTTS_Click(object sender, RoutedEventArgs e)
        {
            if (dtgListCS.SelectedItem != null)
            {
                var item = dtgListCS.SelectedItem;
                string ma = (dtgListCS.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                var nv = from lnv in db.Nhanviens
                         where lnv.Luong < 8000000
                         select lnv;
                List<Nhanvien> list = nv.ToList();
                foreach (var i in list)
                {
                    var ics = (from lnvcs in db.Nvcs
                               where lnvcs.MaNv == i.MaNv
                               where lnvcs.MaChinhSach == ma
                               select lnvcs).SingleOrDefault();
                    if (ics == null)
                    {
                        Nvc nvc = new Nvc();
                        nvc.MaChinhSach = ma;
                        nvc.MaNv = i.MaNv;
                        db.Nvcs.Add(nvc);
                        db.SaveChanges();
                        
                    }

                }
                MessageBox.Show("Thêm chính sách, quy định thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void CSAll_Click(object sender, RoutedEventArgs e)
        {
            if (dtgListCS.SelectedItem != null)
            {
                var item = dtgListCS.SelectedItem;
                string ma = (dtgListCS.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                var nv = from lnv in db.Nhanviens
                         select lnv;
                List<Nhanvien> list = nv.ToList();
                foreach (var i in list)
                {
                    var ics = (from lnvcs in db.Nvcs
                               where lnvcs.MaNv == i.MaNv
                               where lnvcs.MaChinhSach == ma
                               select lnvcs).SingleOrDefault();
                    if (ics == null)
                    {
                        Nvc nvc = new Nvc();
                        nvc.MaChinhSach = ma;
                        nvc.MaNv = i.MaNv;
                        db.Nvcs.Add(nvc);
                        db.SaveChanges();

                    }

                }
                MessageBox.Show("Thêm chính sách, quy định thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void XCanBo_Click(object sender, RoutedEventArgs e)
        {
            if (dtgListCS.SelectedItem != null)
            {
                var item = dtgListCS.SelectedItem;
                string ma = (dtgListCS.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text.Trim();
                var nv = from lnv in db.Nhanviens
                         where lnv.Luong >= 80000000
                         select lnv;
                List<Nhanvien> list = nv.ToList();
                foreach (var i in list)
                {
                    var ics = (from lnvcs in db.Nvcs
                               where lnvcs.MaNv == i.MaNv
                               where lnvcs.MaChinhSach == ma
                               select lnvcs).SingleOrDefault();
                    if (ics != null)
                    {
                        db.Nvcs.Remove(ics);
                        db.SaveChanges();

                    }

                }
                MessageBox.Show("Xóa chính sách, quy định thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void XNVCT_Click(object sender, RoutedEventArgs e)
        {
            if (dtgListCS.SelectedItem != null)
            {
                var item = dtgListCS.SelectedItem;
                string ma = (dtgListCS.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                var nv = from lnv in db.Nhanviens
                         where lnv.Luong < 80000000
                         where lnv.Luong >= 8000000
                         select lnv;
                List<Nhanvien> list = nv.ToList();
                foreach (var i in list)
                {
                    var ics = (from lnvcs in db.Nvcs
                               where lnvcs.MaNv == i.MaNv
                               where lnvcs.MaChinhSach == ma
                               select lnvcs).SingleOrDefault();
                    if (ics != null)
                    {
                        db.Nvcs.Remove(ics);
                        db.SaveChanges();

                    }

                }
                MessageBox.Show("Xoá chính sách, quy định thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void XTTS_Click(object sender, RoutedEventArgs e)
        {
            if (dtgListCS.SelectedItem != null)
            {
                var item = dtgListCS.SelectedItem;
                string ma = (dtgListCS.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                var nv = from lnv in db.Nhanviens
                         where lnv.Luong < 8000000
                         select lnv;
                List<Nhanvien> list = nv.ToList();
                foreach (var i in list)
                {
                    var ics = (from lnvcs in db.Nvcs
                               where lnvcs.MaNv == i.MaNv
                               where lnvcs.MaChinhSach == ma
                               select lnvcs).SingleOrDefault();
                    if (ics != null)
                    {
                        db.Nvcs.Remove(ics);
                        db.SaveChanges();

                    }

                }
                MessageBox.Show("Xóa chính sách, quy định thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void XAll_Click(object sender, RoutedEventArgs e)
        {
            if (dtgListCS.SelectedItem != null)
            {
                var item = dtgListCS.SelectedItem;
                string ma = (dtgListCS.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                var nv = from lnv in db.Nhanviens
                         select lnv;
                List<Nhanvien> list = nv.ToList();
                foreach (var i in list)
                {
                    var ics = (from lnvcs in db.Nvcs
                               where lnvcs.MaNv == i.MaNv
                               where lnvcs.MaChinhSach == ma
                               select lnvcs).SingleOrDefault();
                    if (ics != null)
                    {
                        db.Nvcs.Remove(ics);
                        db.SaveChanges();

                    }

                }
                MessageBox.Show("Xóa chính sách, quy định thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
