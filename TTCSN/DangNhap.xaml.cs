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
    /// Interaction logic for DangNhap.xaml
    /// </summary>
    public partial class DangNhap : Window
    {
        public static string _admin;
        public DangNhap()
        {
            InitializeComponent();
        }


        QlqdcsContext db= new QlqdcsContext();
        
private void DNADMIN_Click(object sender, RoutedEventArgs e)
{
   if (!checkAd())
   {
       return;
   }
   var user = db.NgQls.FirstOrDefault(ldn => ldn.TenDangNhap == txtTenDN.Text &&
   ldn.MatKhau == txtMK.Password);
   if (user != null)
   {
                _admin = user.MaAdm;
       MessageBox.Show("Đăng nhập thành công");
       this.Hide();
       MainWindow mainWindow = new MainWindow();
       mainWindow.ShowDialog();
       this.Close();
   }
   else
   {
       MessageBox.Show("Đăng nhập không thành công", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
   }
}
private bool checkAd()
{
   if (txtTenDN.Text.Trim() == "")
   {
       MessageBox.Show("Bạn chưa nhập tên đăng nhập", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
       txtTenDN.Focus();
       return false;
   }
   if (txtMK.Password.Trim() == "")
   {
       MessageBox.Show("Bạn chưa nhập mật khẩu", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
       txtMK.Focus();
       return false;
   }
   return true;

}

private void quenMK_Click(object sender, RoutedEventArgs e)
{
   QuenMK q =new QuenMK();
   q.Show();
   this.Hide();
}
        public static string _MaNV;
        private void DNND_Click(object sender, RoutedEventArgs e)
        {
            if (!checkND())
            {
                return;
            }
            var user = db.Nhanviens.FirstOrDefault(ldn => ldn.MaNv == txtTenDNND.Text &&
            ldn.MatKhau == txtMKND.Password);
            if (user != null)
            {
                _MaNV = user.MaNv;
                MessageBox.Show("Đăng nhập thành công");
                this.Hide();
                User u = new User();
                u.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void quenMKND_Click(object sender, RoutedEventArgs e)
        {
            QuenMKND q = new QuenMKND();
            q.Show();
            this.Hide();
        }
        private bool checkND()
        {
            if (txtTenDNND.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập tên đăng nhập", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTenDNND.Focus();
                return false;
            }
            if (txtMKND.Password.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtMKND.Focus();
                return false;
            }
            return true;
        }
    }
}
