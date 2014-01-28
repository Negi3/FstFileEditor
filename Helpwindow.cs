using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class Helpwindow : Form
    {
        enum Nodelist {Readme };

        public Helpwindow()
        {
            InitializeComponent();
        }

        private void Helpwindow_Load(object sender, EventArgs e)
        {
            
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)//クリックしたNodeの名前を取得して下のhtml_reference関数を呼び出す
        {
            TreeViewHitTestInfo ht = treeView1.HitTest(e.Location);
            if (ht.Location == TreeViewHitTestLocations.Label) {
                ht.Node.Checked = !ht.Node.Checked;
                html_Reference(ht.Node.Text);
            }
        }

        private void html_Reference(string TVNodeText) //クリックしたNodeのテキストを参照し、表示
        {
            string _currentfilepath = Application.StartupPath;
            
            switch (TVNodeText) { 
                case "Readme":
                    helpHtmlView.Navigate(_currentfilepath + "\\HelpHtmls\\FFEヘルプ\\FstFileEditorhelp.html");
                  break;
                case "MMDAgentの操作方法(key)":
                  helpHtmlView.Navigate(_currentfilepath+"\\helps\\operateMMDAkey.htm");
                    break;

                case "MMDAgentの操作方法(mouse)":
                    helpHtmlView.Navigate(_currentfilepath + "\\helps\\operateMMDAmouse.htm");
                    break;
                case "FstFileEditor基本操作(メニュー編)":
                    helpHtmlView.Navigate(_currentfilepath + "\\helps\\basishelp_menu.htm");
                    break;
                case "FstFileEditor基本操作(入力編)":
                    helpHtmlView.Navigate(_currentfilepath + "\\helps\\basishelp_input.htm");
                    break;
                case "モデルタブ":
                    helpHtmlView.Navigate(_currentfilepath+"\\helps\\modeltab.htm");
                    break;
                case "モーションタブ":
                    helpHtmlView.Navigate(_currentfilepath+"\\helps\\motiontab.htm");
                    Console.WriteLine(_currentfilepath);
                    break;
                case "移動･回転タブ":
                    helpHtmlView.Navigate(_currentfilepath + "\\helps\\move_rotationtab.htm");
                    Console.WriteLine(_currentfilepath);
                    break;
                case "音楽･画像タブ":
                    helpHtmlView.Navigate(_currentfilepath + "\\helps\\music_picture.htm");
                    Console.WriteLine(_currentfilepath);
                    break;
                case "カメラ･照明タブ":
                    helpHtmlView.Navigate(_currentfilepath + "\\helps\\camera_lightning.htm");
                    Console.WriteLine(_currentfilepath);
                    break;
                case "音声認識･合成タブ":
                    helpHtmlView.Navigate(_currentfilepath + "\\helps\\synth_compose.htm");
                    Console.WriteLine(_currentfilepath);
                    break;
                case "その他タブ":
                    helpHtmlView.Navigate(_currentfilepath + "\\helps\\othertab.htm");
                    Console.WriteLine(_currentfilepath);
                    break;

                    

            
            
            
            }
        }

        


        private void helpHtmlView_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
        }





    }
}
