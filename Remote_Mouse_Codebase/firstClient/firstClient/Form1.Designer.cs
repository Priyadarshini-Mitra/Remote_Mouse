namespace firstClient
{
    partial class Client
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client));
            this.txtb_clientDisplay = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.txtb_clientView = new System.Windows.Forms.TextBox();
            this.txtb_clientChat = new System.Windows.Forms.TextBox();
            this.cmb_clientList = new System.Windows.Forms.ComboBox();
            this.lbl_sendTo = new System.Windows.Forms.Label();
            this.lbl_computerName = new System.Windows.Forms.Label();
            this.txtb_name = new System.Windows.Forms.TextBox();
            this.btn_setName = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_attachFile = new System.Windows.Forms.Button();
            this.btn_clearChat = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtb_clientDisplay
            // 
            this.txtb_clientDisplay.BackColor = System.Drawing.Color.White;
            this.txtb_clientDisplay.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtb_clientDisplay.Location = new System.Drawing.Point(447, 541);
            this.txtb_clientDisplay.Multiline = true;
            this.txtb_clientDisplay.Name = "txtb_clientDisplay";
            this.txtb_clientDisplay.ReadOnly = true;
            this.txtb_clientDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtb_clientDisplay.Size = new System.Drawing.Size(191, 63);
            this.txtb_clientDisplay.TabIndex = 0;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(320, 541);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(119, 53);
            this.btn_send.TabIndex = 1;
            this.btn_send.Text = "Send IM";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // txtb_clientView
            // 
            this.txtb_clientView.BackColor = System.Drawing.Color.White;
            this.txtb_clientView.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtb_clientView.Location = new System.Drawing.Point(12, 62);
            this.txtb_clientView.Multiline = true;
            this.txtb_clientView.Name = "txtb_clientView";
            this.txtb_clientView.ReadOnly = true;
            this.txtb_clientView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtb_clientView.Size = new System.Drawing.Size(626, 412);
            this.txtb_clientView.TabIndex = 2;
            // 
            // txtb_clientChat
            // 
            this.txtb_clientChat.BackColor = System.Drawing.Color.White;
            this.txtb_clientChat.Location = new System.Drawing.Point(12, 480);
            this.txtb_clientChat.Multiline = true;
            this.txtb_clientChat.Name = "txtb_clientChat";
            this.txtb_clientChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtb_clientChat.Size = new System.Drawing.Size(626, 55);
            this.txtb_clientChat.TabIndex = 3;
            // 
            // cmb_clientList
            // 
            this.cmb_clientList.FormattingEnabled = true;
            this.cmb_clientList.Location = new System.Drawing.Point(57, 543);
            this.cmb_clientList.Name = "cmb_clientList";
            this.cmb_clientList.Size = new System.Drawing.Size(257, 21);
            this.cmb_clientList.TabIndex = 5;
            this.cmb_clientList.Click += new System.EventHandler(this.comboBox1_Click);
            // 
            // lbl_sendTo
            // 
            this.lbl_sendTo.AutoSize = true;
            this.lbl_sendTo.Location = new System.Drawing.Point(9, 546);
            this.lbl_sendTo.Name = "lbl_sendTo";
            this.lbl_sendTo.Size = new System.Drawing.Size(47, 13);
            this.lbl_sendTo.TabIndex = 6;
            this.lbl_sendTo.Text = "Send to:";
            // 
            // lbl_computerName
            // 
            this.lbl_computerName.AutoSize = true;
            this.lbl_computerName.Location = new System.Drawing.Point(12, 33);
            this.lbl_computerName.Name = "lbl_computerName";
            this.lbl_computerName.Size = new System.Drawing.Size(84, 13);
            this.lbl_computerName.TabIndex = 7;
            this.lbl_computerName.Text = "Computer name:";
            // 
            // txtb_name
            // 
            this.txtb_name.Location = new System.Drawing.Point(103, 30);
            this.txtb_name.Name = "txtb_name";
            this.txtb_name.Size = new System.Drawing.Size(336, 20);
            this.txtb_name.TabIndex = 8;
            // 
            // btn_setName
            // 
            this.btn_setName.Location = new System.Drawing.Point(464, 28);
            this.btn_setName.Name = "btn_setName";
            this.btn_setName.Size = new System.Drawing.Size(75, 23);
            this.btn_setName.TabIndex = 9;
            this.btn_setName.Text = "Set Name";
            this.btn_setName.UseVisualStyleBackColor = true;
            this.btn_setName.Click += new System.EventHandler(this.btn_setName_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(545, 28);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(75, 23);
            this.btn_reset.TabIndex = 10;
            this.btn_reset.Text = "Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(650, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToToolStripMenuItem,
            this.disconnectToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // connectToToolStripMenuItem
            // 
            this.connectToToolStripMenuItem.Name = "connectToToolStripMenuItem";
            this.connectToToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.connectToToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.connectToToolStripMenuItem.Text = "&Connect to..";
            this.connectToToolStripMenuItem.Click += new System.EventHandler(this.connectToToolStripMenuItem_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D)));
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.disconnectToolStripMenuItem.Text = "&Disconnect";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // btn_attachFile
            // 
            this.btn_attachFile.Location = new System.Drawing.Point(57, 570);
            this.btn_attachFile.Name = "btn_attachFile";
            this.btn_attachFile.Size = new System.Drawing.Size(106, 23);
            this.btn_attachFile.TabIndex = 12;
            this.btn_attachFile.Text = "Attach FIle";
            this.btn_attachFile.UseVisualStyleBackColor = true;
            this.btn_attachFile.Click += new System.EventHandler(this.btn_attachFile_Click);
            // 
            // btn_clearChat
            // 
            this.btn_clearChat.Location = new System.Drawing.Point(170, 571);
            this.btn_clearChat.Name = "btn_clearChat";
            this.btn_clearChat.Size = new System.Drawing.Size(75, 23);
            this.btn_clearChat.TabIndex = 13;
            this.btn_clearChat.Text = "Clear chat";
            this.btn_clearChat.UseVisualStyleBackColor = true;
            this.btn_clearChat.Click += new System.EventHandler(this.btn_clearLog_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 616);
            this.Controls.Add(this.btn_clearChat);
            this.Controls.Add(this.btn_attachFile);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_setName);
            this.Controls.Add(this.txtb_name);
            this.Controls.Add(this.lbl_computerName);
            this.Controls.Add(this.lbl_sendTo);
            this.Controls.Add(this.cmb_clientList);
            this.Controls.Add(this.txtb_clientChat);
            this.Controls.Add(this.txtb_clientView);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.txtb_clientDisplay);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Client";
            this.Text = "Cerebro Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Client_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtb_clientDisplay;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.TextBox txtb_clientView;
        private System.Windows.Forms.TextBox txtb_clientChat;
        private System.Windows.Forms.ComboBox cmb_clientList;
        private System.Windows.Forms.Label lbl_sendTo;
        private System.Windows.Forms.Label lbl_computerName;
        private System.Windows.Forms.TextBox txtb_name;
        private System.Windows.Forms.Button btn_setName;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button btn_attachFile;
        private System.Windows.Forms.Button btn_clearChat;
    }
}

