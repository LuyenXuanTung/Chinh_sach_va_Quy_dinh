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
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;

namespace TTCSN
{
    /// <summary>
    /// Interaction logic for ReadFileUser.xaml
    /// </summary>
    public partial class ReadFileUser : System.Windows.Window
    {
        public ReadFileUser()
        {
            InitializeComponent();
            showFile();
        }

        private void showFile()
        {
            if(User._userURL != null)
            {
                FileName.Text = User._fileName;
                Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                object miss = System.Reflection.Missing.Value;
                object path = User._userURL;
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
                MessageBox.Show("Chưa có file tải lên", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            var save = new SaveFileDialog
            {
                Filter = "Word Document|*.docx|Text File|*.txt|All Files|*.*",
                Title = "Lưu file"
             };
            if (save.ShowDialog() == true)
            {
                if(System.IO.Path.GetExtension(save.FileName).ToLower() == ".docx")
                {
                    var wordApp = new Microsoft.Office.Interop.Word.Application();
                    var document = wordApp.Documents.Add();

                    document.Content.Text = rickTextBox.Text;
                    document.SaveAs2(save.FileName);
                    document.Close();

                    wordApp.Quit();
                    
                }
                if(System.IO.Path.GetExtension(save.FileName).ToLower() == ".txt")
                {
                    System.IO.File.WriteAllText(save.FileName, rickTextBox.Text);
                }
                MessageBox.Show("File đã được lưu");
            }
        

        }
    }
}
