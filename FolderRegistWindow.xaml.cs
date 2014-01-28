using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using TextBox = System.Windows.Controls.TextBox;


namespace FstFileEditor
{
    /// <summary>
    /// SearchWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SearchWindow : Window
    {
        readonly string _currentPath = System.Windows.Forms.Application.StartupPath;
        public string MmdAgentFolderPath {
            get { return _temppath; }
        }
        private string _temppath;
        public SearchWindow(MainWindow mw)
        {
            InitializeComponent();
            InitFolderPathTextbox();
            Owner = mw;
        }

        public void InitFolderPathTextbox()
        {

            try
            {
                var sr = new StreamReader(_currentPath + "\\mmdapath.ini");
                _temppath = FolderPath_TextBox.Text = sr.ReadLine();
                sr.Close();
            }
            catch
            {
                FolderPath_TextBox.Text = "フォルダパスを登録してください。";
                FolderRegistOKButton.IsEnabled = false;
            }
        }
        //---------------------------------------------------------------
        //何もせずに閉じる
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //---------------------------------------------------------------
        //フォルダパスの登録
        private void FolderRegistOKButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var sw = new StreamWriter(_currentPath + "\\mmdapath.ini");
                sw.Write(FolderPath_TextBox.Text);
                sw.Close();
                System.Windows.MessageBox.Show("フォルダパスを登録しました。");
                this.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void FolderPath_TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            FolderPath_TextBox.Text = string.Empty;
        }

        private void FolderPath_TextBox_Drop(object sender, System.Windows.DragEventArgs e)
        {
            var DDfilePath = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop, false);
            ((TextBox) sender).Text = DDfilePath[0];
        }

        private void FolderPath_TextBox_PreviewDragOver(object sender, System.Windows.DragEventArgs e)
        {
            e.Handled = true;
        }

        private void FolderPath_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FolderRegistOKButton.IsEnabled = true;
        }
        

    }
}
