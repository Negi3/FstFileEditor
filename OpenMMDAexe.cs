using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;

namespace FstFileEditor
{
    class OpenMMDAexe
    {
        private readonly string _halfPath;
        public OpenMMDAexe(string mmdaPath)
        {
            _halfPath = mmdaPath;
        }

        public void OpenMmda()
        {
            string fullPath = _halfPath + "\\MMDAgent.exe";
            try
            {
                Process.Start(fullPath);
            }
            catch (Exception)
            {
                MessageBox.Show(fullPath +
                                "で\nMMDAgent.exeが見つかりません。" +
                                "\n(その他)→(フォルダパスの登録)を正しく行えているか、" +
                                "\nMMDAgentの実行ファイル名がMMDAgent.exeかもう一度確かめてください。");
                return;
            }
        }
    }
}
