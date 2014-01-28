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

namespace FstFileEditor
{
    /// <summary>
    /// opacityChooser.xaml の相互作用ロジック
    /// </summary>
    public partial class opacityChooser : Window
    {
        public int opacityValue{
            get { return (int)slider1.Value; }
        }
        MainWindow mw;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
        public opacityChooser(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            label1.Content = "透明度:"+(int)slider1.Value;
            mw.window.Opacity = 1.0 - slider1.Value/100;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
