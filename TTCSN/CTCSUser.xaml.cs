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
    /// Interaction logic for CTCSUser.xaml
    /// </summary>
    public partial class CTCSUser : Window
    {
        QlqdcsContext db = new QlqdcsContext();
        public CTCSUser()
        {
            InitializeComponent();
            showCTCS();
        }
        private void showCTCS()
        {
            if (User._nameCS != null)
            {
                var name = (from lcs in db.ChinhSaches
                            where lcs.MaChinhSach == User._nameCS
                            select lcs.ChinhSach1).SingleOrDefault();
                if (name != null)
                {
                    TenCS.Text = name;
                }
                var cs = from lcs in db.Ctcs
                         where lcs.MaCs == User._nameCS
                         select new
                         {
                             DK = lcs.DoiTuong,
                             KL = lcs.KetLuan
                         };
                dtgCTCS.ItemsSource = cs.ToList();
            }
            else
            {
                MessageBox.Show("Không tìm thấy","Cảnh báo",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
