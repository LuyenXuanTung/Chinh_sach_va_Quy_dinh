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
    /// Interaction logic for QuenMKND.xaml
    /// </summary>
    public partial class QuenMKND : Window
    {
        QlqdcsContext db = new QlqdcsContext();
        public QuenMKND()
        {
            InitializeComponent();
        }

        private void CapNhat_Click(object sender, RoutedEventArgs e)
        {
            if (!check())
            {
                return;
            }
            string email = txtEmail.Text;
            var user = db.Nhanviens.SingleOrDefault(ldn => ldn.MaNv == txtTenDN.Text &&
           ldn.Email == email);
            if (user != null)
            {

                user.MatKhau = txtnewMK.Text;
                db.SaveChanges();
                var usera = db.Nhanviens.SingleOrDefault(ldn => ldn.MaNv == txtTenDN.Text &&
                ldn.MatKhau == txtnewMK.Text);
                if (usera != null)
                {
                    MessageBox.Show("Đổi mật khẩu thành công");
                }
                else
                {
                    MessageBox.Show("Đổi mật khẩu không thành công");
                }

                DangNhap d = new DangNhap();
                d.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc email không đúng.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool check()
        {
            if (txtTenDN.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập tên đăng nhập", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTenDN.Focus();
                return false;
            }
            if (txtEmail.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập email", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmail.Focus();
                return false;
            }
            if (txtnewMK.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtnewMK.Focus();
                return false;
            }
            return true;

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            DangNhap dn = new DangNhap();
            dn.Show();
            this.Hide();
        }
    }
}
