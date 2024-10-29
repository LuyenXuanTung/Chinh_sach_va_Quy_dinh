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
    /// Interaction logic for ChiTietCS.xaml
    /// </summary>
    public partial class ChiTietCS : Window
    {
        string macs = MainWindow._ctcs;
        QlqdcsContext db = new QlqdcsContext();
        public ChiTietCS()
        {
            InitializeComponent();
            showCTCS();
        }
        private void showCTCS()
        {
            dtgCTCS.ItemsSource = null;
            clear();
            if (macs!=null)
            {
                var name = (from lcs in db.ChinhSaches
                           where lcs.MaChinhSach == macs
                           select lcs.ChinhSach1).SingleOrDefault();
                if(name!=null )
                {
                    TenCS.Text = name;
                }
                var ctcs = from lct in db.Ctcs
                           where lct.MaCs == macs
                           select new
                           {
                               DK = lct.DoiTuong,
                               KL = lct.KetLuan
                           };
                dtgCTCS.ItemsSource = ctcs.ToList();
            }
        }

        private void dtgCTCS_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if(dtgCTCS.SelectedItem != null)
            {
                var item = dtgCTCS.SelectedItem;
                txtDK.Text = (dtgCTCS.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                txtKL.Text = (dtgCTCS.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
            }
        }
        private void clear()
        {
            txtDK.Text = string.Empty;
            txtKL.Text = string.Empty;
        }
        private void CLear_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }
        private bool check()
        {
            if(txtDK.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập điều kiện", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtDK.Focus();
                return false;
            }
            if (txtKL.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập kết luận", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtKL.Focus();
                return false;
            }
            return true;
        }
        private void AddCT_Click(object sender, RoutedEventArgs e)
        {
            if(check()==true) { 
                 Ctc newct = new Ctc();
                newct.MaCs = macs;
                newct.DoiTuong =txtDK.Text;
                newct.KetLuan =txtKL.Text;
                db.Ctcs.Add(newct);
                db.SaveChanges();
                MessageBox.Show("Thêm thành công", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                showCTCS();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if(dtgCTCS.SelectedIndex != null)
            {
                var item = dtgCTCS.SelectedItem;
                string dk = (dtgCTCS.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                string kl = (dtgCTCS.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                if (check() == true)
                {
                    var ct = (from lct in db.Ctcs
                              where lct.MaCs == macs && lct.DoiTuong == dk && lct.KetLuan == kl
                              select lct).SingleOrDefault();
                   if(ct != null )
                    {
                        ct.MaCs = macs;
                        ct.DoiTuong = txtDK.Text;
                        ct.KetLuan = txtKL.Text;
                        db.SaveChanges();
                        MessageBox.Show("Sửa thành công", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        
                        showCTCS();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dtgCTCS.SelectedIndex != null)
            {
                var item = dtgCTCS.SelectedItem;
                string dk = (dtgCTCS.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                string kl = (dtgCTCS.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                
                
                var ct = (from lct in db.Ctcs
                           where lct.MaCs == macs && lct.DoiTuong == dk && lct.KetLuan == kl
                           select lct).SingleOrDefault();
                db.Ctcs.Remove(ct);
                db.SaveChanges();
                MessageBox.Show("Xóa thành công", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                showCTCS();
            }
        }
    }
}
