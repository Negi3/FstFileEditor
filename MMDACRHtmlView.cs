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
    public partial class MMDACRHtmlView : UserControl
    {
        string currentPath;
        List<Refinfo> refInfoList = new List<Refinfo>();
        struct Refinfo
        {
            public string タブ名;
            public string URI;
            public Refinfo(string _tabname, string uri)
            {
                タブ名 = _tabname;
                URI = uri;
            }
        }

        public MMDACRHtmlView(String Path,String listSelectedName)
        {
            InitializeComponent();
            initList();
            presentHtml(Path, listSelectedName);
        }

        private void initList()
        {
            refInfoList.Add(new Refinfo("はじめに", "MMDAbase.html"));
            refInfoList.Add(new Refinfo("MODEL", "ModelTab.html"));
            refInfoList.Add(new Refinfo("MOTION", "Motiontab.html"));
            refInfoList.Add(new Refinfo("MOVE&ROTATE", "MoveandRotatetab.html"));
            refInfoList.Add(new Refinfo("SOUND", "Soundtab.html"));
            refInfoList.Add(new Refinfo("STAGE", "Stagetab.html"));
            refInfoList.Add(new Refinfo("LIGHT", "Lighttab.html"));
            refInfoList.Add(new Refinfo("CAMERA", "Cameratab.html"));
            refInfoList.Add(new Refinfo("SPEECH", "Speechtab.html"));
            refInfoList.Add(new Refinfo("VARIABLE", "Variabletab.html"));
            refInfoList.Add(new Refinfo("PLUGIN", "PluginTab.html"));
            refInfoList.Add(new Refinfo("OTHER(cmd)", "OtherCmd.html"));
            refInfoList.Add(new Refinfo("Free", "FreeTab.html"));
            refInfoList.Add(new Refinfo("(MMDAキー)", "MMDAkey.html"));
            refInfoList.Add(new Refinfo("(MMDAマウス)", "MMDAmouse.html"));
        }
        private void presentHtml(string currentPath,String selectedName) {
            foreach (Refinfo ri in refInfoList)
            {
                if (selectedName == ri.タブ名)
                {
                   webBrowser1.Navigate(currentPath + "\\HelpHtmls\\MMDACR\\" + ri.URI);
                }
            }
        
        }

    }
}
