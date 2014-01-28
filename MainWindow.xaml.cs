using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FstFileEditor
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        int _lastSelection = 0;//カーソルの位置変数
        string _filePath = null;//ファイルパス
        int _searchIndex;


        //各タブがもつControlをまとめるControl配列の初期化
        Control[] EVENTINPUT;
        Control[] MODELTAB, MOTIONTAB, MOVETAB, SOUNDTAB, STAGETAB, LIGHTTAB, CAMERATAB, SPEECHTAB, VARIABLETAB, PLUGINTAB, OTHERTAB, FREETAB;

        //コマンドバインディングのための初期化
        public readonly static RoutedCommand FocusTop = new RoutedCommand("FocusTop", typeof(MainWindow));//一番上にフォーカスする
        public readonly static RoutedCommand NewCommand = new RoutedCommand("NewCommand", typeof(MainWindow));//Ctrl+Nの新規コマンド
        public readonly static RoutedCommand FileOpenCommand = new RoutedCommand("FileOpenCommand", typeof(MainWindow));//Ctrl+Oのファイル開くコマンド
        public readonly static RoutedCommand Save = new RoutedCommand("Save", typeof(MainWindow));//Ctrl+sの上書き保存用コマンド
        public readonly static RoutedCommand NamedScriptionCommand = new RoutedCommand("NamedScriptionCommand", typeof(MainWindow));//Ctrl+Wの名前を付けて保存コマンド
        public readonly static RoutedCommand ShowSearchRegion = new RoutedCommand("ShowSearchRegion", typeof(MainWindow));//検索ウィンドウを似非表示する
        public readonly static RoutedCommand OpenMMDAexe = new RoutedCommand("OpenMMDAexe", typeof(MainWindow));


        public MainWindow()
        {
            InitializeComponent();
            initTabs();
            initCommandBindings();
            initSearchEngine();
        }
        private void initSearchEngine()
        {
            _searchIndex = fst_textBox.SelectionStart;
            allEnable_Disable4Search(false);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
        //-----------------------------------------------------------------------------------
        //大域変数にControl配列をとったので、それを初期化する。
        private void initTabs()
        {
            EVENTINPUT = new Control[] { textBoxE1, textBoxE2, comboBoxE1, textBoxE3 };
            MODELTAB = new Control[] { comboBox1, comboBox16, textBox2, textBox3, textBox4, checkBox1, textBox5, textBox6 };
            MOTIONTAB = new Control[] { comboBox2, textBox7, textBox8, textBox9, comboBox3, comboBox15, checkBox2, checkBox3 };
            MOVETAB = new Control[] { comboBox4, textBox10, textBox11, comboBox5, textBox12 };
            SOUNDTAB = new Control[] { comboBox6, textBox13, textBox14 };
            STAGETAB = new Control[] { comboBox7, textBox15, textBox16 };
            LIGHTTAB = new Control[] { comboBox8, textBox17, textBox18 };
            CAMERATAB = new Control[] { comboBox9, textBox19, textBox20, textBox21, textBox22 };
            SPEECHTAB = new Control[] { comboBox10, textBox23, textBox24, textBox25, textBox26 };
            VARIABLETAB = new Control[] { comboBox11, textBox27, textBox28, textBox29, textBox30, comboBox12, textBox31, textBox32 };
            PLUGINTAB = new Control[] { comboBox13, textBox33 };
            OTHERTAB = new Control[] { comboBox14, textBox34, textBox35, textBox36, checkBox4, checkBox5, checkBox6 };
            FREETAB = new Control[] { textBox37, textBox38, textBox39 };
        }
        //-----------------------------------------------------------------------------------
        //コマンドバインディングの内容初期化
        private void initCommandBindings()
        {
            this.CommandBindings.Add(new CommandBinding(FocusTop, FocusTopbutton_Click, (s, e) => e.CanExecute = true));
            this.CommandBindings.Add(new CommandBinding(NewCommand, MenuItem_Click_New, (s, e) => e.CanExecute = true));
            this.CommandBindings.Add(new CommandBinding(FileOpenCommand, FileOpen_Click, (s, e) => e.CanExecute = true));
            this.CommandBindings.Add(new CommandBinding(Save, SuperScription_Click, (s, e) => e.CanExecute = true));
            this.CommandBindings.Add(new CommandBinding(NamedScriptionCommand, NamedScription_Click, (s, e) => e.CanExecute = true));
            this.CommandBindings.Add(new CommandBinding(ShowSearchRegion, searchbutton_Click, (s, e) => e.CanExecute = true));
            this.CommandBindings.Add(new CommandBinding(OpenMMDAexe, executeMMDAgentButton_Click, (s, e) => e.CanExecute = true));
        }
        //FocusTopbutton_Click(s, e);
        //-----------------------------------------------------------------------------------
        //常に最前に表示のためのメソッド
        private void Menu_topmost_Click(object sender, RoutedEventArgs e)
        {
            this.Topmost = Menu_topmost.IsChecked;

        }
        //-----------------------------------------------------------------------------------
        //テキストを右で折り返す　のためのメソッド
        private void Menu_returnRight_Click(object sender, RoutedEventArgs e)
        {
            if (Menu_returnRight.IsChecked)
                fst_textBox.TextWrapping = TextWrapping.Wrap;
            else
            {
                fst_textBox.TextWrapping = TextWrapping.NoWrap;
            }
        }
        //-----------------------------------------------------------------------------------
        //EVENTTABのコンボボックスをEnterキーで開く
        private void comboBoxE1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                comboBoxE1.IsDropDownOpen = true;
        }
        //-----------------------------------------------------------------------------------
        //FsttextBoxの書き込み用関数フォーカスとカーソルの移動をする。
        private void focus_and_write(string s)
        {
            _lastSelection = fst_textBox.SelectionStart;
            fst_textBox.Text = fst_textBox.Text.Substring(0, fst_textBox.SelectionStart) + s + fst_textBox.Text.Substring(fst_textBox.SelectionStart);//カーソルの位置に書きこむ
            fst_textBox.SelectionStart = _lastSelection + s.Length;//カーソルの位置を調整する
            fst_textBox.Focus();//書き込んだ後テキストボックスをアクティブにする
            // fst_textBox.ScrollToLine();//カーソルを書き込んだ位置にもっていく
        }
        //-----------------------------------------------------------------------------------
        //TabControl上のコントロールからテキストを生成する。
        private string makeString(IEnumerable<Control> ctls)
        {
            string strtemp = "";
            if (ctls.Equals(FREETAB) && (bool)isCommentCheckBox.IsChecked)
                strtemp += "#";
            strtemp += makeEventString();
            bool isFirstString = true;
            foreach (Control t1 in ctls)
            {
                if (t1.IsEnabled)
                {
                    var t = t1 as TextBox;
                    if (t == null)
                    {
                        var c = t1 as ComboBox;
                        if (c == null)
                        {
                            var ch = t1 as CheckBox;
                            strtemp += ((bool)ch.IsChecked) ? "|ON" : "|OFF";
                        }
                        else
                        {
                            if (c.Text != string.Empty)
                            {
                                strtemp += (isFirstString) ? c.Text : "|" + c.Text;
                                isFirstString = false;
                            }
                        }
                    }
                    else
                    {
                        if (t.Text != string.Empty)
                        {
                            strtemp += (isFirstString) ? t.Text : "|" + t.Text;
                            isFirstString = false;
                        }
                    }
                }
            }
            return strtemp + "\n";
        }
        //-----------------------------------------------------------------------------------
        //書き込み後のコントロールの初期化（チェックボックスは初期化していないが…）
        private void initTabContents(IEnumerable<Control> ctls)
        {
            Control[] seq = EVENTINPUT.Concat(ctls).ToArray();
            foreach (Control ctl in seq)
            {
                var t = ctl as TextBox;
                if (t == null)
                {
                    var c = ctl as ComboBox;
                    if (c == null)
                    {
                        var ch = ctl as CheckBox;
                        ch.IsChecked = false;
                    }
                    else
                    {
                        c.Text = string.Empty;
                    }
                }
                else
                {
                    t.Text = string.Empty;
                }
                ctl.IsEnabled = true;
            }

        }
        //-----------------------------------------------------------------------------------
        //EVENTTABと引数で与えたControl配列を組み合わせる。(Write処理の時に一気に入力内容を書き込むために用いる)
        public void arrayPlusEvent(Control[] ctls)
        {
            Control[] seq = EVENTINPUT.Concat(ctls).ToArray();
        }
        //-----------------------------------------------------------------------------------
        //EVENTTABの入力した内容を文字列にして返す。
        //（EVENTINPUT以外に使わないので、For文で走査するよりはそのまま記述したほうが早い） 
        public string makeEventString()
        {
            string eventString = string.Empty;
            eventString += textBoxE1.Text == string.Empty ? "" : textBoxE1.Text + " ";
            eventString += textBoxE2.Text == string.Empty ? "" : textBoxE2.Text + " ";
            eventString += comboBoxE1.Text == string.Empty ? "" : comboBoxE1.Text;
            eventString += textBoxE3.Text == string.Empty ? "" : "|" + textBoxE3.Text;
            //if (textBoxE3.Text == string.Empty) eventString.Replace("|", "");
            eventString += (eventString == string.Empty) ? "" : "     ";
            return eventString;
        }

        #region MODELTAB
        private void comboBox1_KeyDown(object sender, KeyEventArgs e)//MODEL_Cmd
        {
            if (e.Key == Key.Enter)
                comboBox1.IsDropDownOpen = true;
        }
        private void checkBox1_KeyDown(object sender, KeyEventArgs e)//MODEL_cartoon
        {
            if (e.Key == Key.Enter)
            {
                checkBox1.IsChecked = !checkBox1.IsChecked;
            }
        }
        private void button1_Click(object sender, RoutedEventArgs e) //MODEL_OK
        {
            focus_and_write(makeString(MODELTAB));
            initTabContents(MODELTAB);
        }


        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox1.SelectedItem == null) return;

            foreach (Control t in MODELTAB)
            {
                t.IsEnabled = true;
            }

            string s = comboBox1.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", string.Empty);
            int rist_index = MODELTAB.Length;
            switch (s)
            {
                case "MODEL_ADD":
                    break;
                case "MODEL_CHANGE":
                    rist_index = 2;
                    break;
                default:
                    rist_index = 1;
                    break;

            }
            for (int i = 0; i < MODELTAB.Length; i++)
            {
                if (i > rist_index)
                    MODELTAB[i].IsEnabled = false;
            }
        }
        #endregion
        #region MOTIONTAB
        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox2.SelectedItem == null) return;
            foreach (Control t in MOTIONTAB)
            {
                t.IsEnabled = true;
            }
            string s = comboBox2.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", string.Empty);
            int ristIndex = MOTIONTAB.Length;
            switch (s)
            {
                case "MOTION_ADD":
                    break;
                case "MOTION_CHANGE":
                    ristIndex = 3;
                    break;
                default:
                    ristIndex = 2;
                    break;

            }
            for (int i = 0; i < MOTIONTAB.Length; i++)
            {
                if (i > ristIndex)
                    MOTIONTAB[i].IsEnabled = false;
            }
        }
        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                comboBox2.IsDropDownOpen = true;
        }
        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                comboBox3.IsDropDownOpen = true;
        }
        private void comboBox15_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                comboBox15.IsDropDownOpen = true;
        }
        private void checkBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                checkBox2.IsChecked = !checkBox2.IsChecked;
        }
        private void checkBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                checkBox3.IsChecked = !checkBox3.IsChecked;

        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            focus_and_write(makeString(MOTIONTAB));
            initTabContents(MOTIONTAB);
        }
        #endregion
        #region MOVETAB
        private void comboBox4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox4.SelectedItem == null) return;

            foreach (Control t in MOVETAB)
            {
                t.IsEnabled = true;
            }
            string s = comboBox4.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", string.Empty);
            int ristIndex = MOVETAB.Length;
            switch (s)
            {
                case "MOVE_START":
                case "TURN_START":
                case "ROTATE_START":
                    break;
                default:
                    ristIndex = 1;
                    break;

            }
            for (int i = 0; i < MOVETAB.Length; i++)
            {
                if (i > ristIndex)
                    MOVETAB[i].IsEnabled = false;
            }
        }

        private void comboBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                comboBox4.IsDropDownOpen = true;
        }
        private void comboBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                comboBox5.IsDropDownOpen = true;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            focus_and_write(makeString(MOVETAB));
            initTabContents(MOVETAB);
        }
        #endregion
        #region SOUNDTAB
        private void button6_Click(object sender, RoutedEventArgs e)
        {
            focus_and_write(makeString(SOUNDTAB));
            initTabContents(SOUNDTAB);
        }

        private void comboBox6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox6.SelectedItem == null) return;

            foreach (Control t in SOUNDTAB)
            {
                t.IsEnabled = true;
            }
            string s = comboBox6.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", string.Empty);
            int ristIndex = SOUNDTAB.Length;
            switch (s)
            {
                case "SOUND_START":
                    break;
                default:
                    ristIndex = 1;
                    break;

            }
            for (int i = 0; i < SOUNDTAB.Length; i++)
            {
                if (i > ristIndex)
                    SOUNDTAB[i].IsEnabled = false;
            }
        }
        private void comboBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                comboBox6.IsDropDownOpen = true;
        }
        #endregion
        #region STAGETAB

        private void comboBox7_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                comboBox7.IsDropDownOpen = true;
        }
        private void button7_Click(object sender, RoutedEventArgs e)
        {
            focus_and_write(makeString(STAGETAB));
            initTabContents(STAGETAB);
        }
        #endregion
        #region LIGHTTAB
        private void button8_Click(object sender, RoutedEventArgs e)
        {
            focus_and_write(makeString(LIGHTTAB));
            initTabContents(LIGHTTAB);
        }

        private void comboBox8Changed(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox8.SelectedItem == null) return;

            foreach (Control t in LIGHTTAB)
            {
                t.IsEnabled = true;
            }
            string s = comboBox8.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", string.Empty);
            int ristIndex = LIGHTTAB.Length;
            switch (s)
            {

                case "LIGHTCOLOR":
                    LIGHTTAB[2].IsEnabled = false;
                    break;
                case "LIGHTDIRECTION":
                    LIGHTTAB[1].IsEnabled = false;
                    break;
            }
        }

        private void comboBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                comboBox8.IsDropDownOpen = true;
            }
        }
        #endregion
        #region CAMERATAB
        private void comboBox9_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox9.SelectedItem == null) return;

            foreach (Control t in CAMERATAB)
            {
                t.IsEnabled = true;
            }
            string s = comboBox9.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", string.Empty);
            int rist_index = CAMERATAB.Length;
            switch (s)
            {
                case "CAMERA":
                    break;
            }
            for (int i = 0; i < CAMERATAB.Length; i++)
            {
                if (i > rist_index)
                    CAMERATAB[i].IsEnabled = false;
            }
        }

        private void comboBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                comboBox9.IsDropDownOpen = true;
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            focus_and_write(makeString(CAMERATAB));
            initTabContents(CAMERATAB);
        }

        #endregion
        #region SPEECHTAB
        private void comboBox10_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox10.SelectedItem == null) return;
            foreach (Control t in SPEECHTAB)
            {
                t.IsEnabled = true;
            }
            string s = comboBox10.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", string.Empty);
            int ristIndex = SPEECHTAB.Length;
            switch (s)
            {
                case "SYNTH_START":
                    ristIndex = 3;
                    break;
                case "LIPSYNC_START":
                    for (int o = 1; o <= 3; o++) SPEECHTAB[o].IsEnabled = false;
                    break;
                default:
                    ristIndex = 1;
                    break;

            }
            for (int i = 0; i < SPEECHTAB.Length; i++)
            {
                if (i > ristIndex)
                    SPEECHTAB[i].IsEnabled = false;
            }
        }

        private void comboBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                comboBox10.IsDropDownOpen = true;
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            focus_and_write(makeString(SPEECHTAB));
            initTabContents(SPEECHTAB);
        }
        #endregion
        #region VARIABLE
        private void button5_Click(object sender, RoutedEventArgs e)
        {
            focus_and_write(makeString(VARIABLETAB));
            initTabContents(VARIABLETAB);
        }

        private void comboBox11_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox11.SelectedItem == null) return;
            foreach (Control t in VARIABLETAB)
            {
                t.IsEnabled = true;
            }
            string s = comboBox11.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", string.Empty);
            int ristIndex = VARIABLETAB.Length;
            switch (s)
            {
                case "VALUE_SET":
                    ristIndex = 4;
                    break;
                case "VALUE_GET":
                    ristIndex = 1;
                    break;
                case "VALUE_EVAL":
                    for (int o = 2; o <= 4; o++) VARIABLETAB[o].IsEnabled = false; VARIABLETAB[6].IsEnabled = false;
                    break;
                case "TIMER_START":
                case "TIMER_STOP":
                    for (int o = 1; o <= 5; o++) VARIABLETAB[o].IsEnabled = false;
                    break;
                default:
                    ristIndex = 1;
                    break;

            }
            for (int i = 0; i < VARIABLETAB.Length; i++)
            {
                if (i > ristIndex)
                    VARIABLETAB[i].IsEnabled = false;
            }
        }

        private void comboBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                comboBox11.IsDropDownOpen = true;
            }
        }

        private void comboBox12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                comboBox12.IsDropDownOpen = true;
            }
        }


        #endregion
        #region PLUGINTAB
        private void button10_Click(object sender, RoutedEventArgs e)
        {
            focus_and_write(makeString(PLUGINTAB));
            initTabContents(PLUGINTAB);
        }

        private void comboBox13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                comboBox13.IsDropDownOpen = true;
        }
        #endregion
        #region OTHERTAB
        private void button11_Click(object sender, RoutedEventArgs e)
        {
            focus_and_write(makeString(OTHERTAB));
            initTabContents(OTHERTAB);
        }
        private void checkBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                checkBox4.IsChecked = !checkBox1.IsChecked;
            }
        }
        private void checkBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                checkBox5.IsChecked = !checkBox1.IsChecked;
            }
        }
        private void checkBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                checkBox6.IsChecked = !checkBox1.IsChecked;
            }
        }
        private void comboBox14_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                comboBox14.IsDropDownOpen = true;
        }
        private void comboBox14_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox14.SelectedItem == null) return;

            foreach (Control t in OTHERTAB)
            {
                t.IsEnabled = true;
            }
            string s = comboBox14.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", string.Empty);
            int ristIndex = OTHERTAB.Length;
            switch (s)
            {
                case "EXECUTE":
                    ristIndex = 1;
                    break;
                default:
                    OTHERTAB[1].IsEnabled = false;
                    break;

            }
            for (int i = 0; i < OTHERTAB.Length; i++)
            {
                if (i > ristIndex)
                    OTHERTAB[i].IsEnabled = false;
            }
        }
        #endregion
        #region FREETAB
        private void button12_Click(object sender, RoutedEventArgs e)
        {
            focus_and_write(makeString(FREETAB));
            initTabContents(FREETAB);
        }
        #endregion
        //-----------------------------------------------------------------------------------
        //ドラッグが入ってきた時
        private void fst_textBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.All : DragDropEffects.Move;
        }

        //-----------------------------------------------------------------------------------
        //読み込む前の前処理
        private void fst_textBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = e.Data.GetData(DataFormats.FileDrop) != null;
        }
        //-----------------------------------------------------------------------------------
        //Dropしたらfstファイルを読み込む。
        private void fst_textBox_Drop(object sender, DragEventArgs e)//D&Dしたファイルが指定された拡張子かどうかの判定
        {
            readFstFile(e);
        }
        //-----------------------------------------------------------------------------------
        //Fstファイルを読み込むメソッド(別にFstファイルだけではないが)
        private void readFstFile(DragEventArgs e)
        {
            string[] DDfilePaths = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            string DDfilePath = DDfilePaths[0];
            if (CheckExtension(DDfilePath))
            {
                _filePath = DDfilePath;
                this.Title = System.IO.Path.GetFileName(DDfilePath) + " - FstFileEditor Ver2.00";
                var sr = new StreamReader(new FileStream(DDfilePath, FileMode.Open), Encoding.GetEncoding("SJIS"));
                fst_textBox.Text = sr.ReadToEnd();
                titleDefaultUpdate();
                sr.Close();
            }
        }

        //-----------------------------------------------------------------------------------
        //Fstファイルを読み込むメソッド(別にFstファイルだけではないが)これはファイルを開くから選んだ場合のオーバーロード
        private void readFstFile(string Path)
        {
            string DDfilePath = Path;
            if (CheckExtension(DDfilePath))
            {
                _filePath = DDfilePath;
                this.Title = System.IO.Path.GetFileName(DDfilePath) + " - FstFileEditor Ver2.00";
                var sr = new StreamReader(new FileStream(DDfilePath, FileMode.Open), Encoding.GetEncoding("SJIS"));
                fst_textBox.Text = sr.ReadToEnd();
                titleDefaultUpdate();
                sr.Close();
            }
        }
        //-----------------------------------------------------------------------------------
        //D&Dの際にexArrayに登録された拡張子以外は弾くメソッド
        private bool CheckExtension(string DDfilePath)
        {
            string ex = System.IO.Path.GetExtension(DDfilePath);
            string[] exArray = { ".fst", ".txt", ".mdf", ".ojt", "dic", ".xml" };
            return exArray.Any(extension => extension == ex);
        }
        //-----------------------------------------------------------------------------------
        //ヘルプリファレンスウィンドウの表示
        private void Menu_help_Click(object sender, RoutedEventArgs e)
        {

        }
        //-----------------------------------------------------------------------------------
        //フォントダイアログの表示
        private void Menu_font_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new emanual.Wpf.Controls.FontDialogEx
                {
                    Left = this.Left + 50,
                    Top = this.Top + 50,
                    DlgFontFamily = new FontFamily("メイリオ"),
                    DlgFontSize = 15,
                    DlgLanguage = "ja-jp",
                    DlgSampleText = "テスト Test"
                };

            //dlg.DlgFontWeight = FontWeights.Bold;

            if (dlg.ShowDialog() == true)
            {
                fst_textBox.FontFamily = dlg.DlgFontFamily;
                fst_textBox.FontSize = dlg.DlgFontSize;
                fst_textBox.FontWeight = dlg.DlgFontWeight;
                fst_textBox.FontStyle = dlg.DlgFontStyle;
            }
        }
        //-----------------------------------------------------------------------------------
        //文字色設定ダイアログの表示
        private void Menu_forecolorChoose_Click(object sender, RoutedEventArgs e)
        {
            var cd = new System.Windows.Forms.ColorDialog();
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Color color = cd.Color;
                System.Windows.Media.Color clr = System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
                fst_textBox.Foreground = new SolidColorBrush(clr);
            }
        }
        //-----------------------------------------------------------------------------------
        //背景色設定ダイアログの表示
        private void Menu_backcolorChoose_Click(object sender, RoutedEventArgs e)
        {
            var cd = new System.Windows.Forms.ColorDialog();
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Color color = cd.Color;
                var clr = System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
                fst_textBox.Background = new SolidColorBrush(clr);
            }
        }
        //-----------------------------------------------------------------------------------
        //終了ボタン
        private void TheEnd_Click(object sender, RoutedEventArgs e)
        {
            if (deleteAnnounce())
                this.Close();
        }
        //-----------------------------------------------------------------------------------
        //Topへフォーカスするためのショートカットのためのダミーボタン
        private void FocusTopbutton_Click(object sender, RoutedEventArgs e)
        {
            textBoxE1.Focus();
        }
        //-----------------------------------------------------------------------------------
        //STAGEタブのbmp,bmpへの対応
        private void textBox16_Drop(object sender, DragEventArgs e)
        {
            string[] DDfilePath = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (textBox16.Text.Contains("\\") && !textBox16.Text.Contains(","))
            {
                textBox16.Text += "," + DDfilePath[0];
            }
            else
                textBox16.Text = DDfilePath[0];
        }
        private void textBox16_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = e.Data.GetData(DataFormats.FileDrop) != null;
        }
        //-----------------------------------------------------------------------------------
        //ヘルプ->FstFileEditorヘルプの表示
        private void FFEHELP_Click(object sender, RoutedEventArgs e)
        {
            var hw = new HelpWindow(this);
            hw.Show();

        }
        //-----------------------------------------------------------------------------------
        //ヘルプ->作者について
        private void Myself_Click(object sender, RoutedEventArgs e)
        {
            var im = new IntroduceMyself(this);
            im.Show();
        }
        //-----------------------------------------------------------------------------------
        //MMDAコマンドリファレンスの表示
        private void MMDACR_Click(object sender, RoutedEventArgs e)
        {
            var mcrw = new MMDAgentCommandReferenceWindow(this);
            mcrw.Show();
        }

        private void Menu_Search_Click(object sender, RoutedEventArgs e)
        {
            searchbutton_Click(sender, e);
        }

        private void fst_textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }
        //-------------------------------------------------------------------------------------------
        private void searchbutton_Click(object sender, RoutedEventArgs e)
        {
            allEnable_Disable4Search(true);
            HideSearchRect.Width = 0;
            searchStringTextBox.Focus();
        }
        //--------------------------------------------------------------------------------------------
        //検索ボタン
        private void StringSearchButton_Click(object sender, RoutedEventArgs e)
        {

            fst_textBox.Focus();
            searchText(searchStringTextBox.Text);
            StringSearchButton.Focus();
        }
        //--------------------------------------------------------------------------------------------
        //文字列検索用メソッド
        private void searchText(string str)
        {
            int findTextIndex;
            if ((bool)isIgnoreCheckBox.IsChecked)
            {
                if ((bool)isDown.IsChecked)
                    findTextIndex = fst_textBox.Text.IndexOf(str, _searchIndex);
                else
                    findTextIndex = fst_textBox.Text.LastIndexOf(str, _searchIndex);
            }
            else
            {
                if ((bool)isDown.IsChecked)
                    findTextIndex = fst_textBox.Text.IndexOf(str, _searchIndex,
                        StringComparison.CurrentCultureIgnoreCase);
                else
                    findTextIndex = fst_textBox.Text.LastIndexOf(str, _searchIndex,
                        StringComparison.CurrentCultureIgnoreCase);
            }

            if (findTextIndex == -1)
            {
                MessageBox.Show("指定した文字列が見つかりませんでした。", "FFE - Search", MessageBoxButton.OK);
                return;
            }

            fst_textBox.Select(findTextIndex, str.Length);
            fst_textBox.ScrollToLine(fst_textBox.GetLineIndexFromCharacterIndex(findTextIndex));

            if ((bool)isUp.IsChecked)
                _searchIndex = findTextIndex - 1;
            else
                _searchIndex = findTextIndex + str.Length;
        }
        //-----------------------------------------------------------------
        //検索文字列が空文字の時は検索しない。
        private void searchStringTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringSearchButton.IsEnabled = searchStringTextBox.Text != "";
        }

        //-----------------------------------------------------------------
        //検索枠のコントロールを全enableOR全disableする。
        private void allEnable_Disable4Search(bool isAllEnable)
        {
            var sc = new Control[] { searchStringTextBox, isUp, isDown, isIgnoreCheckBox, /*StringSearchButton,*/ SearchCancelButton };
            foreach (Control t in sc)
                t.IsEnabled = isAllEnable;
        }

        private void SearchCancelButton_Click(object sender, RoutedEventArgs e)
        {
            // fst_textBox.Focus();
            allEnable_Disable4Search(false);
            HideSearchRect.Width = 378;
        }
        //-------------------------------------------------------------------------------
        //終了イベントハンドラ（終了して良いか判断する）
        private void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!deleteAnnounce())
            {
                e.Cancel = true;
            }
            if (!e.Cancel)
            {
                //セッティングセーブメソッドがあればここに書く。
            }
        }
        //-------------------------------------------------------------------------------
        //MMDAgentのパスを相対パスのために登録する
        private void Menu_folderRegist_Click(object sender, RoutedEventArgs e)
        {
            var sw = new SearchWindow(this);
            sw.Show();
        }
        //-------------------------------------------------------------------------------
        //正規表現で座標が正しいか確認する。
        private void Coodinate_CheckEvent(object sender, TextChangedEventArgs e)
        {
            var t = sender as TextBox;
            Brush br;
            if (System.Text.RegularExpressions.Regex.IsMatch(t.Text, @"[-]?\d,[-]?\d,[-]?\d") &&
                !System.Text.RegularExpressions.Regex.IsMatch(t.Text, "[a-z]{1,}|[A-Z]{1,}") &&
                CountChar(t.Text, ',') == 2
                )
            {
                br = Brushes.Black;
            }
            else
                br = Brushes.Red;

            t.Foreground = br;
        }
        //-------------------------------------------------------------------------------
        //文字列中の特定の文字の出現回数を調べる。
        private int CountChar(string text, char ch)
        {
            return text.Length - text.Replace(ch.ToString(), "").Length;
        }

        //-------------------------------------------------------------------------------
        //MMDAgentを起動するためのクリックイベント
        private void executeMMDAgentButton_Click(object sender, RoutedEventArgs e)
        {
            SuperScription_Click(sender, e);
            var fp = new SearchWindow(this);
            var oma = new OpenMMDAexe(fp.MmdAgentFolderPath);
            oma.OpenMmda();
        }

        private void searchStringTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                StringSearchButton_Click(sender, e);
            }
        }

    }
}
