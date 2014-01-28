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
    /// IntroduceMyself.xaml の相互作用ロジック
    /// </summary>
    public partial class IntroduceMyself : Window
    {
        public IntroduceMyself(MainWindow mw)
        {
            InitializeComponent();
            Owner = mw;
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
