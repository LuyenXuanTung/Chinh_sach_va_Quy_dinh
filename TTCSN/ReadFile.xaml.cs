using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using Microsoft.Office.Interop.Word ;
using TTCSN.Models;
using static System.Net.WebRequestMethods;

namespace TTCSN
{
    /// <summary>
    /// Interaction logic for ReadFile.xaml
    /// </summary>
    public partial class ReadFile : System.Windows.Window
    {
        QlqdcsContext db = new QlqdcsContext();
        public ReadFile()
        {
            InitializeComponent();
            showFile();
        }

        private void showFile()
        {
            if(MainWindow._url != null)
            {

                var tencs = (from lcs in db.ChinhSaches
                             where lcs.DuongDan == MainWindow._url
                             select lcs.ChinhSach1).SingleOrDefault();
                if(tencs != null)
                {
                    FileName.Text = tencs;
                }
                Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                object miss = System.Reflection.Missing.Value;
                object path = MainWindow._url;
                object readOnly = true;
                Document docs = word.Documents.Open(ref path, ref miss, ref readOnly, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
                string totalText = "";
                foreach (Microsoft.Office.Interop.Word.Paragraph p in docs.Paragraphs)
                {
                    totalText += p.Range.Text.ToString();
                }
                rickTextBox.Text = totalText;
                docs.Close();
                word.Quit();
            }
            else
            {
                MessageBox.Show("Chưa có file tải lên","Cảnh báo",MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
