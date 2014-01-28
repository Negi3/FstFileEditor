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
namespace FstFileEditor
{
    /// <summary>
    /// MMDAgentCommandReferenceWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MMDAgentCommandReferenceWindow : Window
    {

        String currentPath = System.Windows.Forms.Application.StartupPath;
        public MMDAgentCommandReferenceWindow(MainWindow mw)
        {
            InitializeComponent();
            Owner = mw;
        }

        private void checkBox1_Click(object sender, RoutedEventArgs e)
        {
            this.Topmost = (bool)checkBox1.IsChecked;
        }
        //-----------------------------------------------------------------
        //


        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string t = listBox1.SelectedItem.ToString().Replace("System.Windows.Controls.ListBoxItem: ",string.Empty);
            MMDACRHtmlView mmdacr = new MMDACRHtmlView(currentPath, t);
            windowsFormsHost1.Child = mmdacr;
        }

        private void MCRhelpViewer_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
    }
}
