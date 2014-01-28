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
using System.IO;
using Microsoft.Win32;

namespace FstFileEditor
{
    /// <summary>
    /// FstFileEditorEditPart.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        bool _isTextModified = false;
        enum Miku { Change, Error };

        //-------------------------------------------------------------------------------
        //新規作成
        private void MenuItem_Click_New(object sender, RoutedEventArgs e)
        {
            if (deleteAnnounce())
            {
                fst_textBox.Clear();
                _isTextModified = false;
                _filePath = null;
                this.Title = "FstFileEditor Ver2.00";
            }
        }

        private void FileOpen_Click(object sender, RoutedEventArgs e)//新しいファイルを開くメソッド
        {
            fileOpen();
        }
        private void fileOpen()
        {
            var ofd = new OpenFileDialog();
            if (deleteAnnounce())
            {
                try
                {
                    ofd.FileName = string.Empty;
                    ofd.Filter = "fstファイル|*.fst|テキストファイル|*.txt|すべてのファイル|*.*";
                    if (ofd.ShowDialog() == true)
                    {
                        _filePath = ofd.FileName;
                        readFstFile(ofd.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SuperScription_Click(object sender, RoutedEventArgs e)//上書き保存さん
        {
            if (_filePath != null)
            {
                try
                {
                    File.WriteAllText(_filePath, fst_textBox.Text, Encoding.Default);
                    titleDefaultUpdate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + mikuMessage(Miku.Error));
                }
            }
            else
            {
                NamedScription_Click(sender, e);
            }
        }

        private void NamedScription_Click(object sender, RoutedEventArgs e)//名前を付けて保存さん
        {
            NamedSave();
        }
        private void NamedSave()
        {
            var sdf = new SaveFileDialog();
            sdf.Filter = "fstファイル|*.fst|テキストファイル|*.txt|すべてのファイル|*.*";
            sdf.AddExtension = true;

            try
            {
                {
                    if (sdf.ShowDialog() == true)
                    {
                        _filePath = sdf.FileName;
                        File.WriteAllText(_filePath, fst_textBox.Text, Encoding.Default);
                        titleDefaultUpdate();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + mikuMessage(Miku.Error));
            }
        }



        private void fst_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isTextModified = true;
            if (_isTextModified)//テキスト変更されたら
            {
                if (!this.Title.Contains("*"))//すでに*がなかったら
                    this.Title += "*";
            }
        }

        public void filePath_Drop(object sender, DragEventArgs e)//ModelTabのパス設定
        {
            writeFilePath(sender, e);
        }
        public void filePath_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = e.Data.GetData(DataFormats.FileDrop) != null;
        }
        public void filePath_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = sender as TextBox;
            string[] exArray = { ".pmd", ".vmd", ".wav", ".mp3" };
            foreach (string ex in exArray)
                if (!t.Text.Contains(ex))
                    t.Foreground = Brushes.Red;
                else
                {
                    t.Foreground = Brushes.Black;
                    break;
                }
        }

        //-------------------------------------------------------------------------------
        //座標の部分の数字と,.チェック。aBcなどがあったら赤くする。



        /**************************↑以上がフォームの処理********************************↓以下がその処理の補助関数**************************/
        //-----------------------------------------------------------------------------------
        //TAB上のパスを入力させるテキストボックスに対してDropされたファイルのパスを入力する。
        private void writeFilePath(object sender, DragEventArgs e)
        {
            string currentPath = System.Windows.Forms.Application.StartupPath;
            string[] DDfilePath = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            String DDPath = DDfilePath[0];
            var s = new System.Windows.Forms.WebBrowser();
            try
            {
                var sr = new StreamReader(@currentPath + "\\mmdapath.ini");
                string str = sr.ReadLine();
                if (str != null)
                {
                    string ReadPlus = str + "\\";
                    if (DDPath.Contains(str))
                        DDPath = DDPath.Replace(ReadPlus, string.Empty);
                }
                sr.Close();
            }
            catch
            {
                //Error処理
            }
            ((TextBox)sender).Text = DDPath;
        }
        //-----------------------------------------------------------------------------------
        //タイトルをデフォルトにするためのメソッド
        private void titleDefaultUpdate()
        {
            _isTextModified = false;
            this.Title = this.Title.Replace("*", "");

        }
        //-----------------------------------------------------------------------------------
        //テキストボックスに変更を加えて終了するときに変更を保存するかどうかの判定
        private bool deleteAnnounce()
        {
            if (_isTextModified)
            {
                MessageBoxResult result;
                result = MessageBox.Show(mikuMessage(Miku.Change), "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                return (result == MessageBoxResult.Yes);//Yesなら破棄してもよいNoなら続行    
            }
            else
                return true;//修正されていない。
        }

        private string mikuMessage(Miku i) //こんな面倒なコードかいてごめんなさい
        {
            string msg = "";
            switch (i)
            {
                case Miku.Error:

                    msg = " 　    /＾>》, -―‐‐＜＾}" +
           Environment.NewLine + "     /:::::/,≠´:::::;::::::::ヽ." +
           Environment.NewLine + "    /:::::〃:::::::::／}::::丿ハ" +
           Environment.NewLine + "   /:::::::i{l|:::::／　ﾉ／ }::::" +
           Environment.NewLine + "  /::::::::::瓜イ＞　´＜ ,':::::ﾉ" +
           Environment.NewLine + " /:::::::::|ﾉﾍ.{､　( ﾌ _ノﾉイ::" + "＜エラーですよマスター！" +
           Environment.NewLine + "/::::::::::|　／}｀不´) ::::::";
                    break;
                case Miku.Change:
                    msg = " 　    /＾>》, -―‐‐＜＾}" +
     Environment.NewLine + "     /:::::/,≠´:::::;::::::::ヽ." +
     Environment.NewLine + "    /:::::〃:::::::::／}::::丿ハ" +
     Environment.NewLine + "   /:::::::i{l|:::::／　ﾉ／ }::::" +
     Environment.NewLine + "  /::::::::::瓜イ●　´● ,':::::ﾉ　　" + "／変更は保存されていません。" +
     Environment.NewLine + " /:::::::::|ﾉﾍ.{､　( ﾌ_ノﾉイ::" + "　　　＼破棄していいですかマスター？" +
     Environment.NewLine + "/::::::::::|　／}｀不´) ::::::";
                    break;
                default:
                    msg = "ERROR MESSAGE";
                    break;
            }
            return msg;
        }
    }
}
