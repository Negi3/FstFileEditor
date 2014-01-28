using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FstFileEditor
{
    public partial class UserControl1 : UserControl
    {
        
        public UserControl1(int htmlIndex,string currentPath)
        {
            InitializeComponent();
            presentHtmls_at_webBrowser1(htmlIndex,currentPath);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void presentHtmls_at_webBrowser1(int Index,String currentPath) {
            switch (Index)
            {
                case 1:
                    webBrowser1.Navigate(currentPath + "\\HelpHtmls\\FFEHelp\\FstFileEditorhelp.html");
                    break;
                case 2:
                    webBrowser1.Navigate(currentPath + "\\HelpHtmls\\FFEHelp\\FstFileEditorhelp2.html");
                    break;
                case 3:
                    webBrowser1.Navigate(currentPath + "\\HelpHtmls\\FFEHelp\\FstFileEditorhelp3.html");
                    break;
                default:
                    break;

            }
        
        }
    }
}
