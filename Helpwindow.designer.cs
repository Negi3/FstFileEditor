namespace WindowsFormsApplication5
{
    partial class Helpwindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Readme");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("MMDAgentの操作方法(key)");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("MMDAgentの操作方法(mouse)");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("FstFileEditor基本操作(メニュー編)");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("FstFileEditor基本操作(入力編)");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("-------------------------------------");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("モデルタブ");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("モーションタブ");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("移動･回転タブ");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("音楽･画像タブ");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("カメラ･照明タブ");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("音声認識･合成タブ");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("その他タブ");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Helpwindow));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.helpHtmlView = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.HideSelection = false;
            this.treeView1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "ノード0";
            treeNode1.Text = "Readme";
            treeNode2.Name = "ノード0";
            treeNode2.Text = "MMDAgentの操作方法(key)";
            treeNode3.Name = "ノード0";
            treeNode3.Text = "MMDAgentの操作方法(mouse)";
            treeNode4.Name = "ノード8";
            treeNode4.Text = "FstFileEditor基本操作(メニュー編)";
            treeNode5.Name = "ノード0";
            treeNode5.Text = "FstFileEditor基本操作(入力編)";
            treeNode6.Name = "ノード1";
            treeNode6.Text = "-------------------------------------";
            treeNode7.Name = "ノード2";
            treeNode7.Text = "モデルタブ";
            treeNode8.Name = "ノード3";
            treeNode8.Text = "モーションタブ";
            treeNode9.Name = "ノード4";
            treeNode9.Text = "移動･回転タブ";
            treeNode10.Name = "ノード5";
            treeNode10.Text = "音楽･画像タブ";
            treeNode11.Name = "ノード6";
            treeNode11.Text = "カメラ･照明タブ";
            treeNode12.Name = "ノード7";
            treeNode12.Text = "音声認識･合成タブ";
            treeNode13.Name = "ノード9";
            treeNode13.Text = "その他タブ";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13});
            this.treeView1.ShowPlusMinus = false;
            this.treeView1.Size = new System.Drawing.Size(180, 562);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // helpHtmlView
            // 
            this.helpHtmlView.AllowWebBrowserDrop = false;
            this.helpHtmlView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.helpHtmlView.Location = new System.Drawing.Point(180, 0);
            this.helpHtmlView.MinimumSize = new System.Drawing.Size(20, 20);
            this.helpHtmlView.Name = "helpHtmlView";
            this.helpHtmlView.Size = new System.Drawing.Size(604, 562);
            this.helpHtmlView.TabIndex = 1;
            this.helpHtmlView.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.helpHtmlView_DocumentCompleted);
            // 
            // Helpwindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.helpHtmlView);
            this.Controls.Add(this.treeView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Helpwindow";
            this.Text = "FstFileEditor Help";
            this.Load += new System.EventHandler(this.Helpwindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.WebBrowser helpHtmlView;

    }
}