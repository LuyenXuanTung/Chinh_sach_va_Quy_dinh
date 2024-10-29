using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TTCSN.Models;

namespace TTCSN
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : Window
    {
        public static string _userURL;
        public static string _fileName;
        QlqdcsContext db = new QlqdcsContext();
        string manv = DangNhap._MaNV;
        
        public User()
        {
            InitializeComponent();
            showTTNV();
            showPB();
            showCS();
        }
        private void showTTNV()
        {
            clear();
            var nv = db.Nhanviens.FirstOrDefault(lnv => lnv.MaNv == manv);
            
            if(nv != null )
            {
                txtTen.Text = nv.HoTen;
                txtNS.SelectedDate = nv.NgaySinh;
                txtSDT.Text = nv.Sdt;
                if (nv.GioiTinh == "Nam")
                {
                    rNam.IsChecked = true;
                }
                else
                {
                    rNu.IsChecked = true;
                }

                txtDiaChi.Text = nv.DiaChi;
                txtEmail.Text = nv.Email;
                txtMaNV.Text = nv.MaNv;
                txtMK.Text = nv.MatKhau;
                txtCV.Text = nv.ChucVu;
                txtLuong.Text = nv.Luong.ToString();
                txtThuong.Text = nv.Thuong.ToString();
                txtPhat.Text = nv.Phat.ToString();
                txtTangCa.Text = nv.TangCa.ToString();
                txtNghi.Text = nv.Nghi.ToString();
                txtNamKN.Text = nv.KinhNghiem.ToString();
                txtLoi.Text = nv.Loi;
                txtHoTro.Text = nv.HoTro;

            }
            else
            {
                MessageBox.Show("Không truy cập được thông tin nhân viên", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
        private void showPB()
        {
           // var queryPB = from lpb in db.PhongBans
            //              select lpb.TenPhong;
            //cbbPB.ItemsSource = queryPB.ToList();
            string manv = DangNhap._MaNV;
            var pb = (from lnv in db.Nhanviens
                      join lpb in db.PhongBans on lnv.MaPhong equals lpb.MaPhong
                      where lnv.MaNv == manv
                      select lpb.TenPhong).FirstOrDefault();
            if(pb != null)
            {
                cbbPB.Text = pb;
            }
            

        }
        private void showCS()
        {
            string manv = DangNhap._MaNV;
            var cs = from lnv in db.Nhanviens
                     join lnvcs in db.Nvcs on lnv.MaNv equals lnvcs.MaNv
                     join lcs in db.ChinhSaches on lnvcs.MaChinhSach equals lcs.MaChinhSach
                     where lnv.MaNv == manv
                     select new
                     {
                         TCS = lcs.ChinhSach1,
                         PB = lcs.PhienBan
                     };
            dtgCS.ItemsSource = cs.ToList(); 
        }

        private void dtgCS_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void readFile_Click(object sender, RoutedEventArgs e)
        {
            if(dtgCS.SelectedItem != null)
            {
                var item = dtgCS.SelectedItem;
                var cs = (from lcs in db.ChinhSaches
                         where lcs.ChinhSach1 == (dtgCS.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text
                         select lcs.DuongDan).SingleOrDefault();
                if (cs != null)
                {
                    _userURL = cs;
                    _fileName = System.IO.Path.GetFileName(cs);
                    ReadFileUser rfu = new ReadFileUser();
                    rfu.Show();
                }
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (check() == true)
            {
                var nv = db.Nhanviens.FirstOrDefault(lnv => lnv.MaNv == manv);
                if (nv != null)
                {
                    nv.HoTen = txtTen.Text;
                    nv.NgaySinh = (DateTime)txtNS.SelectedDate;
                    nv.Sdt = txtSDT.Text;
                    if (rNam.IsChecked == true)
                    {
                        nv.GioiTinh = "Nam";
                    }
                    else
                    {
                        nv.GioiTinh = "Nữ";
                    }
                    nv.DiaChi = txtDiaChi.Text;
                    nv.Email = txtEmail.Text;
                    nv.MatKhau = txtMK.Text;
                    db.SaveChanges();
                    showTTNV();
                    MessageBox.Show("Sửa thông tin thành công", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        private bool check()
        {
            if(txtTen.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập họ tên", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTen.Focus();
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
            if (txtMK.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtMK.Focus();
                return false;
            }

            return true;

        }
        private void clear()
        {
            txtTen.Text = string.Empty;
            txtNS.SelectedDate = DateTime.Parse("01/01/2000");
            txtSDT.Text = string.Empty;
            rNam.IsChecked = true;
            txtDiaChi.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtMaNV.Text = string.Empty;
            txtMK.Text = string.Empty;
            txtCV.Text = string.Empty;
            txtLuong.Text = string.Empty;
            txtThuong.Text = string.Empty;
            txtPhat.Text = string.Empty;
            txtTangCa.Text = string.Empty;
            txtNghi.Text = string.Empty;
            //NAM KN
            txtLoi.Text = string.Empty;
            txtHoTro.Text = string.Empty;
        }
        public static string _nameCS;
        private void ChiTiet_Click(object sender, RoutedEventArgs e)
        {
            if(dtgCS.SelectedItem != null)
            {
                var item = dtgCS.SelectedItem;
                string a = (dtgCS.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
               var macs = (from lcs in db.ChinhSaches
                          where lcs.ChinhSach1 == a
                          select lcs.MaChinhSach).SingleOrDefault();
                _nameCS = macs;
                CTCSUser ctu = new CTCSUser();
                ctu.Show();
            }
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
    }
}
