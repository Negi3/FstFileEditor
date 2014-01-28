using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Web;

namespace FstFileEditor
{
    /// <summary>
    /// HelpWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class HelpWindow : Window
    {
        readonly string _currentPath = System.Windows.Forms.Application.StartupPath;
        
        public HelpWindow(MainWindow mw)
        {
            InitializeComponent();
            HelpView();
            Owner = mw;
        }
        //------------------------------------------------------------------
        //タブ毎にすでにhtmlをロードしておく。
        private void HelpView() {


          //  UserControl1 uc = new UserControl1();
            var uc = new UserControl1(1,_currentPath);
            windowsFormsHost1.Child = uc;
            var uc2 = new UserControl1(2, _currentPath);
            windowsFormsHost2.Child = uc2;
            var uc3 = new UserControl1(3, _currentPath);
            windowsFormsHost3.Child = uc3;

             InitHelpHtmLs();
            
        }
        //-----------------------------------------------------------------
        //ヘルプHTML達のinit
        private void InitHelpHtmLs() {
            this.Focus();
        }

        //--------------------------------------------------------------
        //文字コードの変更
        private string ConvertEncording(string src, Encoding dstEnc) {
            byte[] srcTemp = Encoding.ASCII.GetBytes(src);
            byte[] dstTemp = Encoding.Convert(Encoding.ASCII, dstEnc, srcTemp);
            
            string ret = dstEnc.GetString(dstTemp);

            return ret;
        } 


        private void checkBox1_Click(object sender, RoutedEventArgs e)
        {
            this.Topmost = (bool)checkBox1.IsChecked;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }


    }
}
